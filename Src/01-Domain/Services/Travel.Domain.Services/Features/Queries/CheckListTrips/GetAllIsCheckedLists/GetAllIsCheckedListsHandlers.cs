using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.DTOs.CheckListTripDtos;

namespace Travel.Domain.Service.Features.Queries.CheckListTrips.GetAllIsCheckedLists;

public class GetAllIsCheckedListsHandlers : IRequestHandler<GetAllIsCheckedListsQuery, List<CheckListTripListDto>>
{
    private readonly ICheckListTripRepository _checkListTripRepository;

    public GetAllIsCheckedListsHandlers(ICheckListTripRepository checkListTripRepository)
    {
        _checkListTripRepository = checkListTripRepository;
    }

    public async Task<List<CheckListTripListDto>> Handle(GetAllIsCheckedListsQuery request, CancellationToken cancellationToken)
        => await _checkListTripRepository.GetAllIsCheckedLists(cancellationToken);
}
