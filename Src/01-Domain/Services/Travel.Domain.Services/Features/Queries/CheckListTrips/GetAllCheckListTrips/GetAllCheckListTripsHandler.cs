using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.DTOs.CheckListTripDtos;

namespace Travel.Domain.Service.Features.Queries.CheckListTrips.GetAllCheckListTrips;

public class GetAllCheckListTripsHandler : IRequestHandler<GetAllCheckListTripsQuery, List<CheckListTripListDto>>
{
    private readonly ICheckListTripRepository _checkListTripRepository;

    public GetAllCheckListTripsHandler(ICheckListTripRepository checkListTripRepository)
    {
        _checkListTripRepository = checkListTripRepository;
    }

    public async Task<List<CheckListTripListDto>> Handle(GetAllCheckListTripsQuery request, CancellationToken cancellationToken)
        => await _checkListTripRepository.GetAllCheckListTrips(cancellationToken);
}
