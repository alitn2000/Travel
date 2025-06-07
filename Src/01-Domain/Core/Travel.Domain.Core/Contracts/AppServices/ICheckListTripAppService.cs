using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.DTOs;

namespace Travel.Domain.Core.Contracts.AppServices;

public interface ICheckListTripAppService
{
    Task<Result> UpdateIsChecked(UpdateCheckListTripDto dto, CancellationToken cancellationToken);
}
