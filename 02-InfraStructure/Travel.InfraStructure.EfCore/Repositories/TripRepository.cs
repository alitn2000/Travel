using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.DTOs.TripDtos;
using Travel.Domain.Core.Entities;
using Travel.Domain.Core.Enums;
using Travel.InfraStructure.EfCore.Common;

namespace Travel.InfraStructure.EfCore.Repositories;

public class TripRepository : ITripRepository
{
    private readonly AppDbContext _context;

    public TripRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AddTrip(Trip trip, CancellationToken cancellationToken)
    {
        await _context.Trips.AddAsync(trip, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    
    }

    public bool CheckTripTypeExist(TripEnums type)
    {
        return Enum.IsDefined(typeof(TripEnums), type);
    }

    public async Task<List<GetUsersTripDto>> GetUsersTripsById(int userId,CancellationToken cancellationToken)
        => await _context.Trips.Where(t => t.UserId == userId)
        .Select(t => new GetUsersTripDto
        {
            Id = t.Id,
            Destination = t.Destination,
            Start = t.Start,
            End = t.End,
            TripType = t.TripType
        })
        .ToListAsync(cancellationToken);

    public async Task<bool> CheckTripExist(int tripId, CancellationToken cancellationToken)
        => await _context.Trips.AsNoTracking().AnyAsync(t => t.Id == tripId, cancellationToken);

    public async Task<bool> CheckUsersHaveTripById(int userId, int tripId ,CancellationToken cancellationToken)
        => await _context.Trips
        .AsNoTracking()
            .AnyAsync(c => c.UserId == userId && c.Id == tripId, cancellationToken);

    public async Task<Result> UpdateTrip(UpdateTripDto dto, CancellationToken cancellationToken)
    {
        var existTrip = await _context.Trips.FirstOrDefaultAsync(t => t.Id == dto.Id, cancellationToken);

        if (existTrip == null)
            return new Result(false, "Trip not found!!!");

        existTrip.Destination = dto.Destination;
        existTrip.Start = dto.Start;
        existTrip.End = dto.End;
        existTrip.TripType = dto.TripType;
        existTrip.UserId = dto.UserId;
        await _context.SaveChangesAsync(cancellationToken);

        return new Result(true, "Trip updated successfully!!!");
    }

    public async Task<Trip?> GetTripById(int tripId, CancellationToken cancellationToken)
        =>await _context.Trips.FirstOrDefaultAsync(t => t.Id == tripId, cancellationToken); 

}
