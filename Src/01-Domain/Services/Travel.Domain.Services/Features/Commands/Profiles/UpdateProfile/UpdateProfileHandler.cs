using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    private readonly IUserRepository _userRepository;
    public UpdateProfileHandler(IProfileRepository profileRepository, IUserRepository userRepository)
    {
        _profileRepository = profileRepository;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {

        var dto = request.Dto;
        var userId = request.UserId;


        var user = await _userRepository.GetUserWithProfileById(userId, cancellationToken);
        if (user == null)
            return new Result(false,"user not found.");

        user.UpdateProfile(dto.FirstName, dto.LastName, dto.Age, dto.Gender, dto.Address);
            
       
        await _userRepository.UpdateUserProfile(user, cancellationToken);
        return new Result(true, "profile updated successfully");
    }
}
