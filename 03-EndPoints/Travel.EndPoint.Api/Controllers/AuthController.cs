using FluentValidation;
using MediatR;
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
using Travel.Domain.Service.Users.Commands;
using Travel.EndPoint.Api.Extentions;
using Travel.EndPoint.Api.Models.UserModels;

namespace Travel.EndPoint.Api.Controllers;
[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
   // private readonly IUserAppService _userAppService;
    private readonly IValidator<UserLoginModel> _validator;
    private readonly IMediator _mediator;
    public AuthController(IValidator<UserLoginModel> validator, IMediator mediator)
    {
        
        _validator = validator;
        _mediator = mediator;
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
        var tokenResult = await _mediator.Send(new LoginCommand(dto),cancellationToken);

        if (!tokenResult.Flag)
            return Unauthorized();

        return Ok(new { Token = tokenResult.Message });
    }

    [HttpPost("VerifyOTP")]
    public async Task<ActionResult> VerifyOTP(GetTokenDto dto, CancellationToken cancellationToken)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result =  await _mediator.Send(new GetTokenCommand(dto), cancellationToken);
        if (!result.Flag)
            return BadRequest(result.Message);
        return Ok(result.Message);
        ;
    }
}
