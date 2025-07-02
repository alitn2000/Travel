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
using Travel.Domain.Service.Profiles.Commands;

namespace Travel.Domain.Service.Profiles.handlers;

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
        return await _profileRepository.UpdateProfile(mainDto, cancellationToken);
    }
}
