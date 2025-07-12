using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.DTOs.Profile;
using Travel.Domain.Service.Features.Commands.Profiles.UpdateProfile;
using Travel.EndPoint.Api.Controllers.Base;

namespace Travel.EndPoint.Api.Controllers;


public class ProfileController : BaseController
{

    public ProfileController(IMediator mediator) : base(mediator) { }

    [HttpPatch("UpdateProfile")]
    public async Task<ActionResult<Result>> UpdateProfile(UpdateProfileDto dto,CancellationToken cancellationToken)
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
