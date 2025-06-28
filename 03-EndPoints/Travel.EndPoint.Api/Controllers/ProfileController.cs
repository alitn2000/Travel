using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.DTOs.Profile;
using Travel.Domain.Services.AppService;

namespace Travel.EndPoint.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileAppService _profileAppService;

        public ProfileController(IProfileAppService profileAppService)
        {
            _profileAppService = profileAppService;
        }

        [HttpPatch("UpdateProfile")]
        public async Task<ActionResult> UpdateProfile(UpdateProfileDto dto,CancellationToken cancellationToken)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _profileAppService.UpdateProfile(dto, cancellationToken);

            if (!result.Flag)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
