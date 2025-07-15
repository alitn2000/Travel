using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Entities.TripManagement;

namespace Travel.Domain.Service.Features.Commands.Trips.GetTripById;

public class GetTripByIdHandler : IRequestHandler<GetTripByIdCommand, Trip>
{
    private readonly ITripRepository _tripRepository;
    public GetTripByIdHandler(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }
    public async Task<Trip?> Handle(GetTripByIdCommand request, CancellationToken cancellationToken)
    {
        return await _tripRepository.GetTripById(request.TripId, cancellationToken);
    }
}

