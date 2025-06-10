using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.DTOs.Login;
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
    
       => await _context.Users.AsNoTracking().AnyAsync(u => u.Email == dto.Email , cancellationToken);
    
}
