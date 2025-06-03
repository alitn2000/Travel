using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Entities;
using Travel.Domain.Core.Enums;

namespace Travel.Domain.Core.Contracts.Repositories;

public interface ITripRepository
{
    Task<bool> AddTrip(Trip trip, CancellationToken cancellationToken);
    Task<bool> CheckTripTypeExist(TripEnums type, CancellationToken cancellationToken);
    Task<List<Trip>> GetUsersTripsById(int userId, CancellationToken cancellationToken);
}
