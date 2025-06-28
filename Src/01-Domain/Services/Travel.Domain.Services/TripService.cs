using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Contracts.Services;
using Travel.Domain.Core.DTOs.TripDtos;
using Travel.Domain.Core.Entities;

namespace Travel.Domain.Service;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;
    private readonly IUserService _userService;
    private readonly ICheckListService _checkListService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserTripRepository _userTripRepository;

    public TripService(ITripRepository tripRepository,
        IUserService userService,
        ICheckListService checkListService,
        IHttpContextAccessor httpContextAccessor,
        IUserTripRepository userTripRepository)
    {
        _tripRepository = tripRepository;
        _userService = userService;
        _checkListService = checkListService;
        _httpContextAccessor = httpContextAccessor;
        _userTripRepository = userTripRepository;
    }


    public async Task<Result> AddTrip(AddTripDto dto, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext.User;
        var userId = int.Parse(user.FindFirst("Id").Value);

        if (user == null)
            return new Result(false, "user not logged in!!!");
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

        foreach (var t in userTrips)
        {

            if (trip.Start <= t.End && trip.End >= t.Start)
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

        return new Result(true, "Trip added successfully");
    }

    public async Task<bool> CheckTripExist(int tripId, CancellationToken cancellationToken)
        => await _tripRepository.CheckTripExist(tripId, cancellationToken);

    public Task<bool> CheckUsersHaveTripById(int userId, int tripId, CancellationToken cancellationToken)
        => _tripRepository.CheckUsersHaveTripById(userId, tripId, cancellationToken);

    public async Task<List<GetUsersTripDto>> GetUsersTripsById(int userId, CancellationToken cancellationToken)
        => await _tripRepository.GetUsersTripsById(userId, cancellationToken);

    public async Task<Result> UpdateTrip(UpdateTripDto dto, CancellationToken cancellationToken)
    {
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
    }

    public async Task<Result> AddUsersToTrip(AddUsersToTripDto dto, CancellationToken cancellationToken)
    {

        if(!dto.UsersId.Any())
            return new Result(false, "No new users to add.");

        var user = _httpContextAccessor.HttpContext.User;
        if (user == null)
            return new Result(false, "user not logged in!!!");

        var userId = int.Parse(user.FindFirst("Id").Value);

        if (!await _userTripRepository.CheckUserIsOwner(userId, dto.TripId, cancellationToken))
            return new Result(false, "only the owner of trip can add users");

        var trip = await _tripRepository.GetTripById(dto.TripId, cancellationToken);
        if (trip == null)
            return new Result(false, "trip not found !!!");

        foreach (var trips in dto)
        {
            _tripRepository.AddTrip()
        }


    }

}
