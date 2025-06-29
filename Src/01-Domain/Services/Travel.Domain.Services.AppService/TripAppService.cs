using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.Contracts.Jobs;
using Travel.Domain.Core.Contracts.Services;
using Travel.Domain.Core.DTOs.TripDtos;
using Travel.Domain.Core.Entities;

namespace Travel.Domain.Services.AppService;

public class TripAppService : ITripAppService
{
    private readonly ITripJobScheduler _tripJobScheduler;
    private readonly ITripService _tripService;

    public TripAppService(ITripService tripService, ITripJobScheduler tripJobScheduler)
    {
        _tripService = tripService;
        _tripJobScheduler = tripJobScheduler;
    }

    public async Task<Result> AddTrip(AddTripDto trip, CancellationToken cancellationToken)
    {
        var result = await _tripService.AddTrip(trip, cancellationToken);

        return result;
    }


    public async Task<List<GetUsersTripDto>> GetUsersTripsById(int userId, CancellationToken cancellationToken)
        => await _tripService.GetUsersTripsById(userId, cancellationToken);

    public async Task<Result> UpdateTrip(UpdateTripDto dto, CancellationToken cancellationToken)
    {

        var result = await _tripService.UpdateTrip(dto, cancellationToken);

        if (result.Flag)
            await _tripJobScheduler.ScheduleTripJobsAsync(dto.Id, dto.Start, dto.End);
        return result;
    }

    public async Task<Result> AddUsersToTrip(AddUsersToTripDto dto, CancellationToken cancellationToken) 
        => await _tripService.AddUsersToTrip(dto, cancellationToken);
}
