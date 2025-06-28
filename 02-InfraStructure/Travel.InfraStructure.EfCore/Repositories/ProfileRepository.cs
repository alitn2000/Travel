using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.DTOs.Profile;
using Travel.Domain.Core.Entities;
using Travel.InfraStructure.EfCore.Common;

namespace Travel.InfraStructure.EfCore.Repositories;

public class ProfileRepository : IProfileRepository
{
    private readonly AppDbContext _context;

    public ProfileRepository(AppDbContext context)
    {
        _context = context;
    }

   public async Task<Result> UpdateProfile (UpdateProfileDtoWithId dto, CancellationToken cancellationToken)
    {
        var existProfile = await _context.Profiles.FirstOrDefaultAsync(p => p.Id == p.UserId, cancellationToken);
        if (existProfile == null)
            return new Result(false, "profile not found!!!");

        existProfile.FirstName = dto.FirstName;
        existProfile.LastName = dto.LastName;
        existProfile.Address = dto.Address;
        existProfile.Age = dto.Age;
        existProfile.Gender = dto.Gender;

        await _context.SaveChangesAsync(cancellationToken);

        return new Result(true, "profile updated successfully!!!");
    } 


}
