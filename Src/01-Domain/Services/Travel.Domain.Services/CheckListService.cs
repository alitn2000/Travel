using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Contracts.Services;
using Travel.Domain.Core.Entities;

namespace Travel.Domain.Service
{
    public class CheckListService : ICheckListService
    {
        private readonly ICheckListRepository _checkListRepository;

        public CheckListService(ICheckListRepository checkListRepository)
        {
            _checkListRepository = checkListRepository;
        }

        public async Task<List<CheckList>> GetAllCheckListsAsync(CancellationToken cancellationToken)
            =>await _checkListRepository.GetAllCheckListsAsync(cancellationToken);
        
    }
}
