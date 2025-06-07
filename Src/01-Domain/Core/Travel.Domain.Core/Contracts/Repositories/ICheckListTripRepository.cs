using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.DTOs;

namespace Travel.Domain.Core.Contracts.Repositories;

public interface ICheckListTripRepository
{
    Task<bool> UpdateIsChecked(UpdateCheckListTripDto dto, CancellationToken cancellationToken);
}
