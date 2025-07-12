using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace Travel.EndPoint.Api.Controllers.Base;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BaseController  : ControllerBase
{
    protected readonly IMediator _mediator;

    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected int GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst("Id");
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            return 0;

        return userId;
    }
}
