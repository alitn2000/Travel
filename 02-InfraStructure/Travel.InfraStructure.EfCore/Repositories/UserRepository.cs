using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.DTOs.Login;
using Travel.Domain.Core.Entities;
using Travel.Domain.Core.Enums;
using Travel.InfraStructure.EfCore.Common;

namespace Travel.InfraStructure.EfCore.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ChekUSerExistById(int userId, CancellationToken cancellationToken)
        => await _context.Users.AsNoTracking().AnyAsync(u => u.Id == userId, cancellationToken);

    public async Task<bool> Login(LoginDto dto, CancellationToken cancellationToken)
    {
        return true;


    }


    public async Task<bool> CheckUserExistByUserName(LoginDto dto, CancellationToken cancellationToken)
       => await _context.Users
            .AsNoTracking()
            .AnyAsync(u => u.UserName == dto.UserName && u.UserNameType == dto.UserNameType, cancellationToken);

    public async Task<bool> RegisterUser(string userName, UserNameEnum userNameEnum, CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserName = userName,
            UserNameType = userNameEnum
        };

        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var profile = new Profile
        {
            UserId = user.Id,
            FirstName = null,
            LastName = null,
            Age = null,
            Address = null,
            Gender = null
        };

        await _context.Profiles.AddAsync(profile, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<int> GetUserIdByUserName(string userName, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName, cancellationToken);

        if (user == null)
            return 0;
        return user.Id;
       
    }

}
