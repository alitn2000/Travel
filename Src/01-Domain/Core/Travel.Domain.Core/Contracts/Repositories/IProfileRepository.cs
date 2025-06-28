using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.DTOs.Profile;

namespace Travel.Domain.Core.Contracts.Repositories;

public interface IProfileRepository
{
    Task<Result> UpdateProfile(UpdateProfileDtoWithId dto, CancellationToken cancellationToken);
}
