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
using Travel.InfraStructure.EfCore.Common;

namespace Travel.InfraStructure.EfCore.Repositories;

public class UserTripRepository : IUserTripRepository
{
    private readonly AppDbContext _context;

    public UserTripRepository(AppDbContext cotext)
    {
        _context = cotext;
    }

    public async Task<bool> AddUserTrip(UserTrip userTrip,CancellationToken cancellationToken)
    {

        await _context.UserTrips.AddAsync(userTrip, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken)> 0;
    }

    public async Task<bool> CheckUserIsOwner(int userId, int tripId,CancellationToken cancellationToken)
    {
        var existUserTrip = await _context.UserTrips.FirstOrDefaultAsync(ut => ut.UserId == userId && ut.TripId == tripId, cancellationToken);

        return existUserTrip.IsOwner;
    }

    public async Task<bool> CheckUserTripExist(int userId, int tripId, CancellationToken cancellationToken)
        => await _context.UserTrips.AnyAsync(ut => ut.UserId == userId && ut.TripId == tripId, cancellationToken);

    public async Task<bool> AddUsersToTrip(AddUsersToTripDto dto, CancellationToken cancellationToken)
    {
        foreach(var userId in dto.UsersId)
        {
            await _context.AddAsync(new UserTrip
            {
                UserId = userId,
                TripId = dto.TripId,
            }, cancellationToken);
        }
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
