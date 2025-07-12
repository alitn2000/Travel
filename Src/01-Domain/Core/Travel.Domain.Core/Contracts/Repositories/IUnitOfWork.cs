using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Core.Contracts.Repositories;

public interface IUnitOfWork
{
     Task<int> Commit(int currentUserId, CancellationToken cancellationToken);
}
