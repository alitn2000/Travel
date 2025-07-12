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
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.DTOs.Login;
using Travel.Domain.Service.Features.Commands.Users.GetToken;
using Travel.Domain.Service.Features.Commands.Users.Login;
using Travel.EndPoint.Api.Controllers.Base;
using Travel.EndPoint.Api.Extentions;
using Travel.EndPoint.Api.Models.UserModels;

namespace Travel.EndPoint.Api.Controllers;


public class AuthController : BaseController
{
    private readonly IValidator<UserLoginModel> _validator;
    public AuthController(IMediator mediator,IValidator<UserLoginModel> validator) : base(mediator) 
    {
        _validator = validator;
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<ActionResult<Result>> Login([FromBody] UserLoginModel model, CancellationToken cancellationToken)
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
    [AllowAnonymous]
    [HttpPost("VerifyOTP")]
    public async Task<ActionResult<Result>> VerifyOTP(GetTokenDto dto, CancellationToken cancellationToken)
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
