using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;

namespace Travel.Domain.Core.Contracts.Services;

public interface IUserService
{
    Task<Result> CheckUserExistById(int userId, CancellationToken cancellationToken);
}
