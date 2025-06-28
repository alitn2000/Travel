using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.DTOs.TripDtos;
using Travel.Domain.Core.Entities;

namespace Travel.Domain.Core.Contracts.AppServices
{
    public interface ITripAppService
    {
        Task<Result> AddTrip(AddTripDto trip, CancellationToken cancellationToken);
        Task<List<GetUsersTripDto>> GetUsersTripsById(int userId, CancellationToken cancellationToken);
        Task<Result> UpdateTrip(UpdateTripDto dto, CancellationToken cancellationToken);
    }
}
