using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Entities;
using Travel.InfraStructure.EfCore.Common;

namespace Travel.InfraStructure.EfCore.Repositories;

public class ProfileRepository
{
    private readonly AppDbContext _context;

    public ProfileRepository(AppDbContext context)
    {
        _context = context;
    }

    //public async Task<bool> AddProfile(Profile profile, CancellationToken cancellationToken)
    //{
    //    await _context.Profiles.AddAsync(profile, cancellationToken);
    //    return await _context.SaveChangesAsync(cancellationToken) > 0;
    //}
}
