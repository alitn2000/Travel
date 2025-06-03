using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Contracts.Repositories;
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

    public async Task<bool> CheckTripTypeExist(TripEnums type, CancellationToken cancellationToken)
        => await _context.Trips.AsNoTracking().AnyAsync(t => t.TripType == type, cancellationToken);

    public async Task<List<Trip>> GetUsersTripsById(int userId,CancellationToken cancellationToken)
        => await _context.Trips.Where(t => t.UserId == userId).ToListAsync(cancellationToken);

}
