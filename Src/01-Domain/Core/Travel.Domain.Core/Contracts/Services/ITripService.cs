using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Entities;

namespace Travel.Domain.Core.Contracts.Services
{
    public interface ITripService
    {
        Task<Result> AddTrip(Trip trip, CancellationToken cancellationToken);
        Task<List<Trip>> GetUsersTripsById(int userId, CancellationToken cancellationToken);
    }
}
