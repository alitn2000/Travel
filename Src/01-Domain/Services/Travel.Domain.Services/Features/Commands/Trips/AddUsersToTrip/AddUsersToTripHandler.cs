using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Contracts.Services;
using Travel.Domain.Core.Entities.UserManagement;
using Travel.Domain.Service.Features.Queries.Users.CheckUserExistById;

namespace Travel.Domain.Service.Features.Commands.Trips.AddUsersToTrip;

public class AddUsersToTripHandler : IRequestHandler<AddUsersToTripCommand, Result>
{
    private readonly IUserTripRepository _userTripRepository;
    private readonly ITripRepository _tripRepository;
    private readonly IMediator _mediator;

    public AddUsersToTripHandler(IUserTripRepository userTripRepository, ITripRepository tripRepository, IMediator mediator)
    {
        _userTripRepository = userTripRepository;
        _tripRepository = tripRepository;
        _mediator = mediator;
    }

    public async Task<Result> Handle(AddUsersToTripCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var userId = request.UserId;
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

            var userExists = await _mediator.Send(new CheckUserExistByIdQuery(userId), cancellationToken);
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

        var addResult = await _userTripRepository.AddUserTrips(addedUsers,userId, cancellationToken);

        if (!addResult)
            return new Result(false, "Error while adding users to trip.");

        return new Result(true, $"{addedUsers.Count} users added successfully to trip.");

    }
}
