using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Contracts.Services;
using Travel.Domain.Core.Entities;
using Travel.Domain.Service.Exceptions;
using Travel.Domain.Service.Features.Queries.Users.CheckUserExistById;

namespace Travel.Domain.Service.Features.Commands.Trips.UpdateTrip;

public class UpdateTripHandler : IRequestHandler<UpdateTripCommand, Result>
{
    private readonly ITripRepository _tripRepository;
    private readonly IUserTripRepository _userTripRepository;
    private readonly IMediator _mediator;
    public UpdateTripHandler(ITripRepository tripRepository, IUserTripRepository userTripRepository, IMediator mediator)
    {
        _tripRepository = tripRepository;
        _userTripRepository = userTripRepository;
        _mediator = mediator;
    }

    public async Task<Result> Handle(UpdateTripCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var userId = request.UserId;



        var isOwner = await _userTripRepository.CheckUserIsOwner(userId, dto.Id, cancellationToken);

        if (!isOwner)
            throw new CommandValidationException("Only the trip owner can update the trip.");
        var userResult = await _mediator.Send(new CheckUserExistByIdQuery(userId), cancellationToken);

        if (!userResult.Flag)
            return userResult;

        var userCheckResult = await _tripRepository.CheckUsersHaveTripById(dto.UserId, dto.Id, cancellationToken);

        if (!userCheckResult)
            throw new CommandValidationException("User does not have this trip.");

        var trip = await _tripRepository.GetTripById(dto.Id, cancellationToken);
        if (trip == null)
            throw new CommandValidationException("Trip not found.");

        var updateResult = trip.UpdateTrip(dto.Destination, dto.Start, dto.End, dto.TripType);

        if (!updateResult.Flag)
            return updateResult;

        var result = await _tripRepository.UpdateTrip(trip, userId, cancellationToken);

        return result;
    }
}
