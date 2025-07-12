using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.DTOs.CheckListTripDtos;
using Travel.Domain.Core.Entities;

namespace Travel.Domain.Service.Features.Commands.CheckListTrips.UpdateIsChecked;

public class UpdateIsCheckedHandler : IRequestHandler<UpdateIsCheckedCommand, Result>
{
    private readonly IUserTripRepository _userTripRepository;
    private readonly ICheckListTripRepository _checkListTripRepository;
public UpdateIsCheckedHandler(IUserTripRepository userTripRepository, ICheckListTripRepository checkListTripRepository)
    {
        _userTripRepository = userTripRepository;
        _checkListTripRepository = checkListTripRepository;
    }

    public async Task<Result> Handle(UpdateIsCheckedCommand request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;
        var dto = request.Dto;
        var tripResult = await _userTripRepository.CheckUsersHaveTripById(userId, dto.TripId, cancellationToken);
        if (!tripResult)
            return new Result(false, "Trip does not exist.");

        var res = await _checkListTripRepository.UpdateIsChecked(dto,userId, cancellationToken);
        if (!res)
            return new Result(false, "ChekListTrip not found!!!");

        return new Result(true, "ChekListTrip updated successfully!!!");
    }
}
