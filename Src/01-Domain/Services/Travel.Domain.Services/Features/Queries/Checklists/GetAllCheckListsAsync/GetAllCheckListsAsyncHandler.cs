using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.DTOs.CheckListDtos;
using Travel.Domain.Core.Entities;

namespace Travel.Domain.Service.Features.Queries.Checklists.GetAllCheckListsAsync;

public class GetAllCheckListsAsyncHandler : IRequestHandler<GetAllCheckListsAsyncQuery, List<CheckListListsDto>>
{
    private readonly ICheckListRepository _checkListRepository;

    public GetAllCheckListsAsyncHandler(ICheckListRepository checkListRepository)
    {
        _checkListRepository = checkListRepository;
    }

    public async Task<List<CheckListListsDto>> Handle(GetAllCheckListsAsyncQuery request, CancellationToken cancellationToken)
        => await _checkListRepository.GetAllCheckListsAsync(cancellationToken);
}
