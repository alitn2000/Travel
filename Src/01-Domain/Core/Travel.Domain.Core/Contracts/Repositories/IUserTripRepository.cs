using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.DTOs.TripDtos;
using Travel.Domain.Core.Entities;

namespace Travel.Domain.Core.Contracts.Repositories;

public interface IUserTripRepository
{
    Task<bool> AddUserTrip(UserTrip userTrip, CancellationToken cancellationToken);
    Task<bool> CheckUserIsOwner(int userId, int tripId, CancellationToken cancellationToken);
    Task<bool> CheckUserTripExist(int userId, int tripId, CancellationToken cancellationToken);
    Task<bool> AddUserTrips(List<UserTrip> userTrips, CancellationToken cancellationToken);
    Task<bool> CheckUsersHaveTripById(int userId, int tripId, CancellationToken cancellationToken);
}
