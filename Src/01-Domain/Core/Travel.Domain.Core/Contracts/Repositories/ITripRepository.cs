﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.DTOs.TripDtos;
using Travel.Domain.Core.Entities.TripManagement;
using Travel.Domain.Core.Enums;

namespace Travel.Domain.Core.Contracts.Repositories;

public interface ITripRepository
{
    Task<bool> AddTrip(Trip trip, int userId, CancellationToken cancellationToken);  
    Task<List<GetUsersTripDto>> GetUsersTripsById(int userId, CancellationToken cancellationToken);
    Task<bool> CheckTripExist(int tripId, CancellationToken cancellationToken);
    Task<bool> CheckUsersHaveTripById(int userId, int tripId, CancellationToken cancellationToken);
    Task<Result> UpdateTrip(Trip trip, int userId, CancellationToken cancellationToken);
    Task<Trip?> GetTripById(int tripId, CancellationToken cancellationToken);
    Task<Result> CheckUserTripDateConflict(int userId, DateTime start, DateTime end, CancellationToken cancellationToken);
    //Task UpdateStatus(Trip trip, StatusEnum status, CancellationToken cancellationToken);
}
