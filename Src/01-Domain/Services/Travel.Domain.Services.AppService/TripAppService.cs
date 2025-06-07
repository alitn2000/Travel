using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.Contracts.Services;
using Travel.Domain.Core.DTOs.TripDtos;
using Travel.Domain.Core.Entities;

namespace Travel.Domain.Services.AppService;

public class TripAppService : ITripAppService
{
    private readonly ITripService _tripService;

    public TripAppService(ITripService tripService)
    {
        _tripService = tripService;
    }

    public async Task<Result> AddTrip(Trip trip, CancellationToken cancellationToken)
        => await _tripService.AddTrip(trip, cancellationToken);

    public Task<List<GetUsersTripDto>> GetUsersTripsById(int userId, CancellationToken cancellationToken)
        => _tripService.GetUsersTripsById(userId, cancellationToken);
}
