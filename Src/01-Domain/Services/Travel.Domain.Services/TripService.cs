using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Contracts.Services;
using Travel.Domain.Core.Entities;

namespace Travel.Domain.Service;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;
    private readonly IUserService _userService;

    public TripService(ITripRepository tripRepository, IUserService userService)
    {
        _tripRepository = tripRepository;
        _userService = userService;
    }


    public async Task<Result> AddTrip(Trip trip, CancellationToken cancellationToken)
    {
        var result = await _userService.CheckUserExistById(trip.UserId, cancellationToken);
        if (!result.Flag)
            return result;

        var typeResult = await _tripRepository.CheckTripTypeExist(trip.TripType, cancellationToken);
        if(!typeResult)
            return new Result(false, "Trip type does not exist.");

        var addResult = await _tripRepository.AddTrip(trip, cancellationToken);
        return new Result(true, "Trip added successfully");
    }

    public Task<List<Trip>> GetUsersTripsById(int userId, CancellationToken cancellationToken)
        => _tripRepository.GetUsersTripsById(userId, cancellationToken);
}
