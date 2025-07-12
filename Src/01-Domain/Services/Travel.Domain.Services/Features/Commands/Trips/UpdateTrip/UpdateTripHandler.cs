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
            return new Result(false, "Only the trip owner can update the trip.");
        var userResult = await _mediator.Send(new CheckUserExistByIdQuery(userId), cancellationToken);

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

        var result = await _tripRepository.UpdateTrip(dto,userId, cancellationToken);

        return result;
    }
}
