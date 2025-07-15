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
using Travel.Domain.Core.Entities.UserManagement;
using Travel.InfraStructure.EfCore.Common;

namespace Travel.InfraStructure.EfCore.Repositories;

public class ProfileRepository : IProfileRepository
{
    private readonly AppDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public ProfileRepository(AppDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    //public async Task<Result> UpdateProfile (UpdateProfileDtoWithId dto, CancellationToken cancellationToken)
    //{
    //    var existProfile = await _context.Profiles.FirstOrDefaultAsync(p => p.Id == p.UserId, cancellationToken);
    //    if (existProfile == null)
    //        return new Result(false, "profile not found!!!");

    //    existProfile.FirstName = dto.FirstName;
    //    existProfile.LastName = dto.LastName;
    //    existProfile.Address = dto.Address;
    //    existProfile.Age = dto.Age;
    //    existProfile.Gender = dto.Gender;

    //    await _unitOfWork.Commit(existProfile.UserId, cancellationToken);

    //    return new Result(true, "profile updated successfully!!!");
    //}
    public async Task UpdateProfile(Profile profile, CancellationToken cancellationToken)
    {
        _context.Profiles.Update(profile);
        await _unitOfWork.Commit(profile.UserId, cancellationToken);
    }
    public async Task<Profile?> GetByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        return await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);
    }

}
