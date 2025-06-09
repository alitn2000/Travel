using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.DTOs.TripDtos;
using Travel.Domain.Core.Entities;

namespace Travel.Domain.Core.Contracts.Services
{
    public interface ITripService
    {
        Task<Result> AddTrip(Trip trip, CancellationToken cancellationToken);
        Task<List<GetUsersTripDto>> GetUsersTripsById(int userId, CancellationToken cancellationToken);
        Task<bool> CheckTripExist(int tripId, CancellationToken cancellationToken);
        Task<bool> CheckUsersHaveTripById(int userId, int tripId, CancellationToken cancellationToken);
        Task<Result> UpdateTrip(UpdateTripDto dto, CancellationToken cancellationToken);
    }
}
