using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Entities;
using Travel.Domain.Service.CheckListTrips.Commands;

namespace Travel.Domain.Service.CheckListTrips.Handlers;

public class AddCheckListTripHandler : IRequestHandler<AddCheckListTripCommand, Result>
{
    private readonly ICheckListTripRepository _checkListTripRepository;
    private readonly ITripRepository _tripRepository;

    private readonly IUserTripRepository _userTripRepository;
    public AddCheckListTripHandler(ITripRepository tripRepository, ICheckListTripRepository checkListTripRepository, IUserTripRepository userTripRepository)
    {
        _tripRepository = tripRepository;
        _checkListTripRepository = checkListTripRepository;
        _userTripRepository = userTripRepository;
    }

    public async Task<Result> Handle(AddCheckListTripCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var userId = request.UserId;
        var result = await _tripRepository.CheckTripExist(dto.TripId, cancellationToken);
        if (!result)
            return new Result(false, "Trip does not exist.");

        var ownerCheck = await _userTripRepository.CheckUserIsOwner(userId, dto.TripId, cancellationToken);

        if (!ownerCheck)
            return new Result(false, "only the owner of trip can add check lists!!!");

        var addResult = await _checkListTripRepository.AddCheckListTrip(dto, cancellationToken);
        if (!addResult)
            return new Result(false, "one or more check list id is not correct!!!");

        return new Result(true, "CheckListTrip added successfully!!!");

    }
}
