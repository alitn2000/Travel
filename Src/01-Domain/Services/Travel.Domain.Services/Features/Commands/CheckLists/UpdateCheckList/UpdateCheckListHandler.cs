using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Repositories;

namespace Travel.Domain.Service.Features.Commands.CheckLists.UpdateCheckList;

public class UpdateCheckListHandler : IRequestHandler<UpdateCheckListCommand, Result>
{
    private readonly ICheckListRepository _checkListRepository;

    public UpdateCheckListHandler(ICheckListRepository checkListRepository)
    {
        _checkListRepository = checkListRepository;
    }

    public async Task<Result> Handle(UpdateCheckListCommand request, CancellationToken cancellationToken)
    {
        var result = await _checkListRepository.UpdateCheckList(request.Dto,request.UserId, cancellationToken);
        return result;
    }
}
