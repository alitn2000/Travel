using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Entities;
using Travel.Domain.Service.CheckLists.Commands;

namespace Travel.Domain.Service.CheckLists.Handlers;

public class GetCheckListByIdHandler : IRequestHandler<GetCheckListByIdCommand, CheckList?>
{
    private readonly ICheckListRepository _checkListRepository;

    public GetCheckListByIdHandler(ICheckListRepository checkListRepository)
    {
        _checkListRepository = checkListRepository;
    }

    public async Task<CheckList?> Handle(GetCheckListByIdCommand request, CancellationToken cancellationToken)
        => await _checkListRepository.GetCheckListById(request.CheckListId, cancellationToken);
}
