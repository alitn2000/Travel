﻿using System;
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

    public TripService(ITripRepository tripRepository, IUserService userService, ICheckListService checkListService)
    {
        _tripRepository = tripRepository;
        _userService = userService;
        _checkListService = checkListService;
    }


    public async Task<Result> AddTrip(Trip trip, CancellationToken cancellationToken)
    {
        var result = await _userService.CheckUserExistById(trip.UserId, cancellationToken);
        if (!result.Flag)
            return result;

        var typeResult =  _tripRepository.CheckTripTypeExist(trip.TripType);
        if(!typeResult)
            return new Result(false, "Trip type does not exist.");

        var userTrips = await _tripRepository.GetUsersTripsById(trip.UserId, cancellationToken);

        foreach (var t in userTrips)
        {
            
            if (trip.Start <= t.End && trip.End >= t.Start)
                return new Result(false, "You already have a trip at this time.");
        }

        var addResult = await _tripRepository.AddTrip(trip, cancellationToken);
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
}
