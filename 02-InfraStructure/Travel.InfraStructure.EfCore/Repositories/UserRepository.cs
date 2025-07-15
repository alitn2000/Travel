using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.DTOs.Login;
using Travel.Domain.Core.DTOs.TripDtos;
using Travel.Domain.Core.Entities;
using Travel.Domain.Core.Entities.UserManagement;
using Travel.Domain.Core.Enums;
using Travel.InfraStructure.EfCore.Common;

namespace Travel.InfraStructure.EfCore.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public UserRepository(AppDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ChekUSerExistById(int userId, CancellationToken cancellationToken)
        => await _context.Users.AsNoTracking().AnyAsync(u => u.Id == userId, cancellationToken);




    public async Task<bool> CheckUserExistByUserName(LoginDto dto, CancellationToken cancellationToken)
       => await _context.Users
            .AsNoTracking()
            .AnyAsync(u => u.UserName == dto.UserName && u.UserNameType == dto.UserNameType, cancellationToken);

    public async Task<bool> RegisterUser(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        var addUserResult = await _unitOfWork.Commit(user.Id, cancellationToken);
        return addUserResult > 0;
    }

    public async Task<int> GetUserIdByUserName(string userName, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName, cancellationToken);

        if (user == null)
            return 0;
        return user.Id;
       
    }

    public async Task<User?> GetUserWithProfileById(int userId, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.Profile)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public async Task UpdateUserProfile(User user , CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _unitOfWork.Commit(user.Id, cancellationToken);
        user.RegisterUserCompleted();
    }

    public async Task<List<GetUsersTripDto>> GetUsersTripsByUserId(int userId, CancellationToken cancellationToken)
    {
        return await _context.Users
       .Where(u => u.Id == userId)
       .SelectMany(u => u.UserTrips)
       .Include(ut => ut.Trip)
       .Select(ut => new GetUsersTripDto
       {
           Id = ut.Trip.Id,
           Destination = ut.Trip.Destination,
           Start = ut.Trip.Start,
           End = ut.Trip.End,
           TripType = ut.Trip.TripType
       })
       .ToListAsync(cancellationToken);
    }

    public async Task<User?> GetUserById(int userId, CancellationToken cancellationToken)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }
}
