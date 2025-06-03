using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Core.Contracts.Repositories;

public interface IUserRepository
{
    Task<bool> ChekUSerExistById(int userId, CancellationToken cancellationToken);
}
