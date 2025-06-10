using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.DTOs.Login;

namespace Travel.EndPoint.Api.Controllers;
[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserAppService _userAppService;
    public AuthController(IConfiguration configuration, IUserAppService userAppService)
    {
        _configuration = configuration;
        _userAppService = userAppService;
    }

    
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDto dto, CancellationToken cancellationToken)
    {
        var token = await _userAppService.Login(dto, cancellationToken);

        if (token == "Invalid username or password")
            return Unauthorized();

        return Ok(new { Token = token });
    }
}
