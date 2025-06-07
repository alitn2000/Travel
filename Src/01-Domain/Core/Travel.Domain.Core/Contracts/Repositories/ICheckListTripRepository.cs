using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.DTOs.CheckListTripDtos;
using Travel.Domain.Core.Entities;

namespace Travel.Domain.Core.Contracts.Repositories;

public interface ICheckListTripRepository
{
    Task<bool> UpdateIsChecked(UpdateCheckListTripDto dto, CancellationToken cancellationToken);
    Task<List<CheckListTripListDto>> GetAllCheckListTrips(CancellationToken cancellationToken);
    Task<bool> AddCheckListTrip(AddCheckListToTripDto dto, CancellationToken cancellationToken);
    Task<List<CheckListTripListDto>> GetAllIsCheckedLists(CancellationToken cancellationToken);
}
