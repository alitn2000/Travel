using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Jobs;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Contracts.Services;
using Travel.Domain.Core.Entities;
using Travel.Domain.Core.Entities.TripManagement;
using Travel.Domain.Core.Entities.UserManagement;
using Travel.Domain.Core.Enums;
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

        var overLapResult = await _tripRepository.CheckUserTripDateConflict(userId, dto.Start, dto.End, cancellationToken);
        if (!overLapResult.Flag)
            return overLapResult;

        if (dto.Start < DateTime.UtcNow)
            return new Result(false, "Start date cannot be in the past.");     //performance ya khanayi?

        if (dto.Start >= dto.End)
            return new Result(false, "Start date must be before end date.");

        if (!Enum.IsDefined(typeof(TripEnums), dto.TripType))
            return new Result(false, "Invalid trip type.");

        var trip = new Trip(dto.Destination, dto.Start, dto.End, dto.TripType);
        


        var result = await _mediator.Send(new CheckUserExistByIdQuery(userId), cancellationToken);
        if (!result.Flag)
            return result;

        await _tripRepository.AddTrip(trip, userId, cancellationToken);

        //var userTrips = await _tripRepository.GetUsersTripsById(userId, cancellationToken); // domain event!!!
        //if (userTrips.Any(t => trip.Start <= t.End && trip.End >= t.Start))
        //    return new Result(false, "You already have a trip at this time.");



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
