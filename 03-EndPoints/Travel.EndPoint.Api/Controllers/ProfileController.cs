using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.DTOs.Profile;
using Travel.Domain.Service.Profiles.Commands;
using Travel.Domain.Services.AppService;
using Travel.EndPoint.Api.Controllers.Base;

namespace Travel.EndPoint.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : BaseController
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("UpdateProfile")]
        public async Task<ActionResult> UpdateProfile(UpdateProfileDto dto,CancellationToken cancellationToken)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = GetCurrentUserId();
            if (userId == 0)
            {
                return Unauthorized("User not logged in.");
            }

            var result = await _mediator.Send(new UpdateProfileCommand(dto, userId), cancellationToken);

            if (!result.Flag)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
