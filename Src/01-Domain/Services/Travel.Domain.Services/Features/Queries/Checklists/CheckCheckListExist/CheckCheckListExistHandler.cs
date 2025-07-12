using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Entities;

namespace Travel.Domain.Service.Features.Queries.Checklists.CheckCheckListExist
{
    public class CheckCheckListExistHandler : IRequestHandler<CheckCheckListExistQuery, bool>
    {
        private readonly ICheckListRepository _checkListRepository;

        public CheckCheckListExistHandler(ICheckListRepository checkListRepository)
        {
            _checkListRepository = checkListRepository;
        }

        public async Task<bool> Handle(CheckCheckListExistQuery request, CancellationToken cancellationToken)
            => await _checkListRepository.CheckCheckListExist(request.CheckListId, cancellationToken);
        
    }
}
