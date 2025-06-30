using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Jobs;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Contracts.Services;
using Travel.Domain.Core.DTOs.TripDtos;
using Travel.Domain.Core.Entities;

namespace Travel.Domain.Service.Trips;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;
    private readonly IUserService _userService;
    private readonly ICheckListService _checkListService;

    private readonly IUserTripRepository _userTripRepository;
    private readonly ITripJobScheduler _tripJobScheduler;

    public TripService(ITripRepository tripRepository,
        IUserService userService,
        ICheckListService checkListService,
        IUserTripRepository userTripRepository, ITripJobScheduler tripJobScheduler)
    {
        _tripRepository = tripRepository;
        _userService = userService;
        _checkListService = checkListService;
        _userTripRepository = userTripRepository;
        _tripJobScheduler = tripJobScheduler;
    }


    public async Task<Result> AddTrip(AddTripDto dto,int userId, CancellationToken cancellationToken)
    {
        
        var trip = new Trip()
        {
            Destination = dto.Destination,
            End = dto.End,
            Start = dto.Start,
            TripType = dto.TripType,


        };


        var result = await _userService.CheckUserExistById(userId, cancellationToken);
        if (!result.Flag)
            return result;

        var typeResult = _tripRepository.CheckTripTypeExist(trip.TripType);
        if (!typeResult)
            return new Result(false, "Trip type does not exist.");

        var userTrips = await _tripRepository.GetUsersTripsById(userId, cancellationToken);

        if (userTrips.Any(t => trip.Start <= t.End && trip.End >= t.Start))
        {
            return new Result(false, "You already have a trip at this time.");
        }

        var addResult = await _tripRepository.AddTrip(trip, cancellationToken);
        var userTrip = new UserTrip()
        {
            UserId = userId,
            TripId = trip.Id,
            IsOwner = true
        };
        var userTripResult = await _userTripRepository.AddUserTrip(userTrip, cancellationToken);
        if (!userTripResult)
            return new Result(false, "trip not addded!!!");

        await _tripJobScheduler.ScheduleTripJobsAsync(trip.Id, trip.Start, trip.End);

        return new Result(true, "Trip added successfully");
    } //done

    public async Task<bool> CheckTripExist(int tripId, CancellationToken cancellationToken)
        => await _tripRepository.CheckTripExist(tripId, cancellationToken); //done

    public async Task<bool> CheckUsersHaveTripById(int userId, int tripId, CancellationToken cancellationToken)
        => await _tripRepository.CheckUsersHaveTripById(userId, tripId, cancellationToken); //done

    public async Task<List<GetUsersTripDto>> GetUsersTripsById(int userId, CancellationToken cancellationToken) //done
        => await _tripRepository.GetUsersTripsById(userId, cancellationToken);

    public async Task<Result> UpdateTrip(UpdateTripDto dto,int userId, CancellationToken cancellationToken)  
    {
        var isOwner = await _userTripRepository.CheckUserIsOwner(userId, dto.Id, cancellationToken);

        if(!isOwner)
            return new Result(false, "Only the trip owner can update the trip.");
        var userResult = await _userService.CheckUserExistById(dto.UserId, cancellationToken);

        if (!userResult.Flag)
            return userResult;

        var typeResult = _tripRepository.CheckTripTypeExist(dto.TripType);

        if (!typeResult)
            return new Result(false, "Trip type does not exist.");

        var userCheckResult = await _tripRepository.CheckUsersHaveTripById(dto.UserId, dto.Id, cancellationToken);

        if (!userCheckResult)
            return new Result(false, "User does not have this trip.");

        if (dto.Start < DateTime.UtcNow)
            return new Result(false, "start date is in the past!!!");

        if (dto.Start >= dto.End)
            return new Result(false, "start date most be befor end date!!!");

        var result = await _tripRepository.UpdateTrip(dto, cancellationToken);

        return result;
    } //done

    public async Task<Result> AddUsersToTrip(AddUsersToTripDto dto,int userId, CancellationToken cancellationToken)
    {

        if (dto.UsersId == null || !dto.UsersId.Any())
            return new Result(false, "No users to add.");

       


        var isOwner = await _userTripRepository.CheckUserIsOwner(userId, dto.TripId, cancellationToken);
        if (!isOwner)
            return new Result(false, "Only the trip owner can add users.");

        var trip = await _tripRepository.GetTripById(dto.TripId, cancellationToken);
        if (trip == null)
            return new Result(false, "Trip not found.");

        var addedUsers = new List<UserTrip>();

        foreach (var Id in dto.UsersId.Distinct())
        {

            var userExists = await _userService.CheckUserExistById(userId, cancellationToken);
            if (!userExists.Flag)
                continue;


            var userTrips = await _tripRepository.GetUsersTripsById(userId, cancellationToken);

            bool overlap = userTrips.Any(t => trip.Start <= t.End && trip.End >= t.Start);

            if (overlap)
                continue;

            bool alreadyInTrip = await _tripRepository.CheckUsersHaveTripById(userId, trip.Id, cancellationToken);
            if (alreadyInTrip)
                continue;

            addedUsers.Add(new UserTrip
            {
                UserId = userId,
                TripId = trip.Id,
                IsOwner = false
            });
        }

        if (!addedUsers.Any())
            return new Result(false, "none of the users can be added beacause of validations.");

        var addResult = await _userTripRepository.AddUserTrips(addedUsers, cancellationToken);

        if (!addResult)
            return new Result(false, "Error while adding users to trip.");

        return new Result(true, $"{addedUsers.Count} users added successfully to trip.");



    } //done

}
