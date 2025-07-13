using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.DTOs.Profile;
using Travel.Domain.Core.Entities.UserUserManagement;

namespace Travel.Domain.Core.Contracts.Repositories;

public interface IProfileRepository
{
    Task UpdateProfile(Profile profile, CancellationToken cancellationToken);
    Task<Profile?> GetByUserIdAsync(int userId, CancellationToken cancellationToken);
}
