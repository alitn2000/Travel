using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.DTOs.Profile;
using Travel.Domain.Core.Entities;

namespace Travel.Domain.Service.Features.Commands.Profiles.UpdateProfile;

public class UpdateProfileHandler : IRequestHandler<UpdateProfileCommand, Result>
{
    private readonly IProfileRepository _profileRepository;

    public UpdateProfileHandler(IProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }

    public async Task<Result> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {

        var dto = request.Dto;
        var userId = request.UserId;
        var mainDto = new UpdateProfileDtoWithId()
        {
            Address = dto.Address,
            Age = dto.Age,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Gender = dto.Gender,
            UserId = userId
        };
        //return await _profileRepository.UpdateProfile(mainDto, cancellationToken);

        var profile = await _profileRepository.GetByUserIdAsync(request.UserId, cancellationToken);
        if (profile == null)
            return new Result(false,"Profile not found.");

        profile.UpdateInformation(
            request.Dto.FirstName,
            request.Dto.LastName,
            request.Dto.Age,
            request.Dto.Gender,
            request.Dto.Address
        );

        await _profileRepository.UpdateProfile(profile, cancellationToken);
        return Result();
    }
}
