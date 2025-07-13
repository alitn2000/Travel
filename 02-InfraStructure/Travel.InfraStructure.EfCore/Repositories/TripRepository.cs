using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Jobs;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.DTOs.TripDtos;
using Travel.Domain.Core.Entities.TripManagement;
using Travel.Domain.Core.Enums;
using Travel.InfraStructure.EfCore.Common;

namespace Travel.InfraStructure.EfCore.Repositories;

public class TripRepository : ITripRepository
{
    private readonly AppDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    public TripRepository(AppDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> AddTrip(Trip trip,int userId, CancellationToken cancellationToken)
    {
        await _context.Trips.AddAsync(trip, cancellationToken);
       
       return await _unitOfWork.Commit(userId,cancellationToken) > 0 ;
       

    }

    public bool CheckTripTypeExist(TripEnums type)
    {
        return Enum.IsDefined(typeof(TripEnums), type);     //jasho avaz kon
    }

    public async Task<List<GetUsersTripDto>> GetUsersTripsById(int userId,CancellationToken cancellationToken)
        => await _context.UserTrips
    .Where(t => t.UserId == userId)
    .Include(t => t.Trip)
    .Select(t => new GetUsersTripDto
    {
        Id = t.Trip.Id,
        Destination = t.Trip.Destination,
        Start = t.Trip.Start,
        End = t.Trip.End,
        TripType = t.Trip.TripType
    })
    .ToListAsync(cancellationToken);

    public async Task<bool> CheckTripExist(int tripId, CancellationToken cancellationToken)
        => await _context.Trips.AsNoTracking().AnyAsync(t => t.Id == tripId, cancellationToken);

    public async Task<bool> CheckUsersHaveTripById(int userId, int tripId ,CancellationToken cancellationToken)
        => await _context.UserTrips
        .AsNoTracking()
            .AnyAsync(c => c.UserId == userId && c.Id == tripId, cancellationToken);

    public async Task<Result> UpdateTrip(UpdateTripDto dto,int userId, CancellationToken cancellationToken)
    {
        var existTrip = await _context.Trips.FirstOrDefaultAsync(t => t.Id == dto.Id, cancellationToken);

        if (existTrip == null)
            return new Result(false, "Trip not found!!!");

        existTrip.Destination = dto.Destination;
        existTrip.Start = dto.Start;
        existTrip.End = dto.End;
        existTrip.TripType = dto.TripType;
        
        await _unitOfWork.Commit(userId, cancellationToken);

        return new Result(true, "Trip updated successfully!!!");
    }

    public async Task<Trip?> GetTripById(int tripId, CancellationToken cancellationToken)
        =>await _context.Trips.FirstOrDefaultAsync(t => t.Id == tripId, cancellationToken); 

   public async Task UpdateStatus(Trip trip,  StatusEnum status, CancellationToken cancellationToken)
    {
        trip.Status = status;
        await _unitOfWork.Commit(trip.CreatedUserId, cancellationToken);
        // await _tripJobScheduler.ScheduleTripJobsAsync(trip.Id, trip.Start, trip.End);
    }
}
