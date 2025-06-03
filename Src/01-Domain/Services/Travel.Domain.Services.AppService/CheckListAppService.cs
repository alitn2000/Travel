using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.Contracts.Services;
using Travel.Domain.Core.Entities;

namespace Travel.Domain.Services.AppService
{
    public class CheckListAppService : ICheckListAppService
    {
        private readonly ICheckListService _checkListService;

        public CheckListAppService(ICheckListService checkListService)
        {
            _checkListService = checkListService;
        }

        public async Task<List<CheckList>> GetAllCheckListsAsync(CancellationToken cancellationToken)
            => await _checkListService.GetAllCheckListsAsync(cancellationToken);

    }
}
