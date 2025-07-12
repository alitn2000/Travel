using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Jobs;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Contracts.Services;
using Travel.Domain.Core.Entities;
using Travel.Domain.Service.Features.Queries.Users.CheckUserExistById;

namespace Travel.Domain.Service.Features.Commands.Trips.AddTrip;

public class AddTripHandler : IRequestHandler<AddTripCommand, Result>
{


    private readonly ITripRepository _tripRepository;
   // private readonly IUserService _userService;
    private readonly IUserTripRepository _userTripRepository;
    private readonly ITripJobScheduler _tripJobScheduler;
    private readonly IMediator _mediator;

    public AddTripHandler(ITripRepository tripRepository,
                                 IUserTripRepository userTripRepository,
                                 ITripJobScheduler tripJobScheduler,
                                 IMediator mediator)
    {
        _tripRepository = tripRepository;
        // _userService = userService;
        _userTripRepository = userTripRepository;
        _tripJobScheduler = tripJobScheduler;
        _mediator = mediator;
    }

    public async Task<Result> Handle(AddTripCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var userId = request.UserId;

        var trip = new Trip
        {
            Destination = dto.Destination,
            End = dto.End,
            Start = dto.Start,
            TripType = dto.TripType,
        };

        var result = await _mediator.Send(new CheckUserExistByIdQuery(userId), cancellationToken);
        if (!result.Flag)
            return result;

        if (!_tripRepository.CheckTripTypeExist(trip.TripType))
            return new Result(false, "Trip type does not exist.");

        var userTrips = await _tripRepository.GetUsersTripsById(userId, cancellationToken);
        if (userTrips.Any(t => trip.Start <= t.End && trip.End >= t.Start))
            return new Result(false, "You already have a trip at this time.");

        await _tripRepository.AddTrip(trip,userId, cancellationToken);

        var userTrip = new UserTrip
        {
            UserId = userId,
            TripId = trip.Id,
            IsOwner = true
        };

        var userTripResult = await _userTripRepository.AddUserTrip(userTrip, cancellationToken);
        if (!userTripResult)
            return new Result(false, "Trip not added!!!");

        await _tripJobScheduler.ScheduleTripJobsAsync(trip.Id, trip.Start, trip.End);

        return new Result(true, "Trip added successfully");
    }
}
