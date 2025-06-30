using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.DTOs.TripDtos;
using Travel.Domain.Core.Entities;
using Travel.Domain.Service.Trips.Queries;

namespace Travel.Domain.Service.Trips.Handler;

public class GetUsersTripsByIdHandler : IRequestHandler<GetUsersTripsByIdQuery, List<GetUsersTripDto>>
{
    private readonly ITripRepository _tripRepository;

    public GetUsersTripsByIdHandler(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async Task<List<GetUsersTripDto>> Handle(GetUsersTripsByIdQuery request, CancellationToken cancellationToken)
    => await _tripRepository.GetUsersTripsById(request.UserId, cancellationToken);
}
