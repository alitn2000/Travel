using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.DTOs.Profile;

namespace Travel.Domain.Core.Contracts.Services;

public interface IProfileService
{
    Task<Result> UpdateProfile(UpdateProfileDto dto, CancellationToken cancellationToken);
}
