using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Contracts.Services;
using Travel.Domain.Core.DTOs;

namespace Travel.Domain.Service;

public class CheckListTripService : ICheckListTripService
{
    private readonly ICheckListTripRepository _checkListTripRepository;

    public CheckListTripService(ICheckListTripRepository checkListTripRepository)
    {
        _checkListTripRepository = checkListTripRepository;
    }

    public async Task<Result> UpdateIsChecked(UpdateCheckListTripDto dto, CancellationToken cancellationToken)
    {
       var res = await _checkListTripRepository.UpdateIsChecked(dto, cancellationToken);
        if (!res)
            return new Result(false, "ChekListTrip not found!!!");

        return new Result(true, "ChekListTrip updated successfully!!!");
    }
}
