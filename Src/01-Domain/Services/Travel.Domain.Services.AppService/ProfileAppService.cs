//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Travel.Domain.Core.BaseEntities;
//using Travel.Domain.Core.Contracts.AppServices;
//using Travel.Domain.Core.Contracts.Services;
//using Travel.Domain.Core.DTOs.Profile;

//namespace Travel.Domain.Services.AppService;

//public class ProfileAppService : IProfileAppService
//{
//    private readonly IProfileService _profileService;

//    public ProfileAppService(IProfileService profileService)
//    {
//        _profileService = profileService;
//    }

//    public async Task<Result> UpdateProfile(UpdateProfileDto dto,int userId, CancellationToken cancellationToken)
//        => await _profileService.UpdateProfile(dto,userId, cancellationToken);
//}
