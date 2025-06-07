using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.Contracts.Services;
using Travel.Domain.Core.DTOs;

namespace Travel.Domain.Services.AppService;

public class CheckListTripAppService : ICheckListTripAppService
{
    private readonly ICheckListTripService _checkListTripService;

    public CheckListTripAppService(ICheckListTripService checkListTripService)
    {
        _checkListTripService = checkListTripService;
    }

    public Task<Result> UpdateIsChecked(UpdateCheckListTripDto dto, CancellationToken cancellationToken)
        => _checkListTripService.UpdateIsChecked(dto, cancellationToken);
}
