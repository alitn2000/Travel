using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.DTOs.Login;
using Travel.EndPoint.Api.Extentions;
using Travel.EndPoint.Api.Models.UserModels;

namespace Travel.EndPoint.Api.Controllers;
[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserAppService _userAppService;
    private readonly IValidator<UserLoginModel> _validator;
    public AuthController(IConfiguration configuration, IUserAppService userAppService, IValidator<UserLoginModel> validator)
    {
        _configuration = configuration;
        _userAppService = userAppService;
        _validator = validator;
    }

    
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserLoginModel model, CancellationToken cancellationToken)
    {
        var modelValidator = await _validator.ValidateAsync(model, cancellationToken);
        if (!modelValidator.IsValid)
        {
            modelValidator.AddToModelState(ModelState);
            return UnprocessableEntity(ModelState);
        }
        var dto = new LoginDto
        {
            UserName = model.UserName,
            UserNameType = model.UserNameType,
        };
        var tokenResult = await _userAppService.Login(dto, cancellationToken);

        if (!tokenResult.Flag)
            return Unauthorized();

        return Ok(new { Token = tokenResult.Message });
    }

    [HttpPost("VerifyOTP")]
    public ActionResult VerifyOTP(GetTokenDto dto)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result =  _userAppService.GetToken(dto);
        if (!result.Flag)
            return BadRequest(result.Message);
        return Ok(result.Message);
        ;
    }
}
