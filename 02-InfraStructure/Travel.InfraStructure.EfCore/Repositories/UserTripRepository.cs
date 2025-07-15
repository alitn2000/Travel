using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.DTOs.TripDtos;
using Travel.Domain.Core.Entities.UserManagement;
using Travel.InfraStructure.EfCore.Common;

namespace Travel.InfraStructure.EfCore.Repositories;

public class UserTripRepository : IUserTripRepository
{
    private readonly AppDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public UserTripRepository(AppDbContext cotext, IUnitOfWork unitOfWork)
    {
        _context = cotext;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> AddUserTrip(UserTrip userTrip,CancellationToken cancellationToken)
    {

        await _context.UserTrips.AddAsync(userTrip, cancellationToken);
        return await _unitOfWork.Commit(userTrip.UserId,cancellationToken)> 0;
    }

    public async Task<bool> CheckUserIsOwner(int userId, int tripId,CancellationToken cancellationToken)
    {
        var existUserTrip = await _context.UserTrips
        .FirstOrDefaultAsync(ut => ut.UserId == userId && ut.TripId == tripId, cancellationToken);

        return existUserTrip?.IsOwner ?? false;
    }

    public async Task<bool> CheckUserTripExist(int userId, int tripId, CancellationToken cancellationToken)
        => await _context.UserTrips.AnyAsync(ut => ut.UserId == userId && ut.TripId == tripId, cancellationToken);

    public async Task<bool> AddUserTrips(List<UserTrip> userTrips,int userId, CancellationToken cancellationToken)
    {
        await _context.UserTrips.AddRangeAsync(userTrips, cancellationToken);
        return await _unitOfWork.Commit(userId,cancellationToken) > 0;
    }

    public async Task<bool> CheckUsersHaveTripById(int userId, int tripId, CancellationToken cancellationToken)
       => await _context.UserTrips
       .AsNoTracking()
           .AnyAsync(c => c.UserId == userId && c.TripId == tripId, cancellationToken);
}
