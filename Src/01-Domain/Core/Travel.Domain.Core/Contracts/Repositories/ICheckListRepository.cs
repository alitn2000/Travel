using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Entities;

namespace Travel.Domain.Core.Contracts.Repositories;

 public interface ICheckListRepository
{
    Task<List<CheckList>> GetAllCheckListsAsync(CancellationToken cancellationToken);

}
