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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProfileService(IProfileRepository profileRepository, IHttpContextAccessor httpContextAccessor)
    {
        _profileRepository = profileRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result> UpdateProfile(UpdateProfileDto dto, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
            return new Result(false, "user not logged in");
        var mainDto = new UpdateProfileDtoWithId()
        {
            Address = dto.Address,
            Age = dto.Age,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Gender = dto.Gender,
            UserId = int.Parse(user?.FindFirst("Id")?.Value)
        };
        return await _profileRepository.UpdateProfile(mainDto, cancellationToken);
        
    }

}
