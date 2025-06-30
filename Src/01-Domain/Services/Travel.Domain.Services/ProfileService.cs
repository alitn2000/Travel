using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Contracts.Services;
using Travel.Domain.Core.DTOs.Profile;

namespace Travel.Domain.Service;

public class ProfileService : IProfileService
{
    private readonly IProfileRepository _profileRepository;
 

    public ProfileService(IProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }

    public async Task<Result> UpdateProfile(UpdateProfileDto dto,int userId, CancellationToken cancellationToken)
    {
        var mainDto = new UpdateProfileDtoWithId()
        {
            Address = dto.Address,
            Age = dto.Age,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Gender = dto.Gender,
            UserId = userId
        };
        return await _profileRepository.UpdateProfile(mainDto, cancellationToken);
        
    }

}
