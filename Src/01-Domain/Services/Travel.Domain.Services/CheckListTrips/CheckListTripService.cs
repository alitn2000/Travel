using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Contracts.Services;
using Travel.Domain.Core.DTOs.CheckListTripDtos;
using Travel.Domain.Core.Entities;
using Travel.InfraStructure.EfCore.Repositories;

namespace Travel.Domain.Service.CheckListTrips;

public class CheckListTripService : ICheckListTripService
{
    private readonly ICheckListTripRepository _checkListTripRepository;
    private readonly ITripRepository _tripRepository;
  
    private readonly IUserTripRepository _userTripRepository;

    public CheckListTripService(ICheckListTripRepository checkListTripRepository, ITripRepository tripRepository, IUserTripRepository userTripRepository)
    {
        _checkListTripRepository = checkListTripRepository;
        _tripRepository = tripRepository;
        _userTripRepository = userTripRepository;
    }

    public async Task<List<CheckListTripListDto>> GetAllCheckListTrips(CancellationToken cancellationToken)
        => await _checkListTripRepository.GetAllCheckListTrips(cancellationToken);

    public async Task<Result> UpdateIsChecked(UpdateCheckListTripDto dto,int userId, CancellationToken cancellationToken)
    {

       

        var tripResult = await _userTripRepository.CheckUsersHaveTripById(userId, dto.TripId, cancellationToken);
        if(!tripResult)
            return new Result(false, "Trip does not exist.");

        var res = await _checkListTripRepository.UpdateIsChecked(dto, cancellationToken);
        if (!res)
            return new Result(false, "ChekListTrip not found!!!");

        return new Result(true, "ChekListTrip updated successfully!!!");
    }

    public async Task<Result> AddCheckListTrip(AddCheckListToTripDto dto,int userId, CancellationToken cancellationToken)
    {
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

    public async Task<List<CheckListTripListDto>> GetAllIsCheckedLists(CancellationToken cancellationToken)
        => await _checkListTripRepository.GetAllIsCheckedLists(cancellationToken);
}
