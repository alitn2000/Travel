using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.DTOs.CheckListTripDtos;
using Travel.Domain.Core.Entities;

namespace Travel.Domain.Core.Contracts.AppServices;

public interface ICheckListTripAppService
{
    Task<Result> UpdateIsChecked(UpdateCheckListTripDto dto, CancellationToken cancellationToken);
    Task<List<CheckListTripListDto>> GetAllCheckListTrips(CancellationToken cancellationToken);
    Task<Result> AddCheckListTrip(AddCheckListToTripDto dto, CancellationToken cancellationToken);
    Task<List<CheckListTripListDto>> GetAllIsCheckedLists(CancellationToken cancellationToken);
}
