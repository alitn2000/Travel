using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace Travel.EndPoint.Api.Controllers.Base;

[ApiController]
public class BaseController  : ControllerBase
{
    protected int GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst("Id");
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            return 0;

        return userId;
    }
}
