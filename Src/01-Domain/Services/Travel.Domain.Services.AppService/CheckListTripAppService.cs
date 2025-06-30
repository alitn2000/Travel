using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.Contracts.Services;
using Travel.Domain.Core.DTOs.CheckListTripDtos;
using Travel.Domain.Core.Entities;

namespace Travel.Domain.Services.AppService;

public class CheckListTripAppService : ICheckListTripAppService
{
    private readonly ICheckListTripService _checkListTripService;

    public CheckListTripAppService(ICheckListTripService checkListTripService)
    {
        _checkListTripService = checkListTripService;
    }

    public async Task<Result> AddCheckListTrip(AddCheckListToTripDto dto,int userId, CancellationToken cancellationToken)
        => await _checkListTripService.AddCheckListTrip(dto,userId, cancellationToken);

    public async Task<List<CheckListTripListDto>> GetAllCheckListTrips(CancellationToken cancellationToken)
        => await _checkListTripService.GetAllCheckListTrips(cancellationToken);

    public async Task<List<CheckListTripListDto>> GetAllIsCheckedLists(CancellationToken cancellationToken)
        => await _checkListTripService.GetAllIsCheckedLists(cancellationToken);

    public async Task<Result> UpdateIsChecked(UpdateCheckListTripDto dto,int userId, CancellationToken cancellationToken)
        => await _checkListTripService.UpdateIsChecked(dto,userId, cancellationToken);
}
