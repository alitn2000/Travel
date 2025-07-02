using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Services;
using Travel.Domain.Service.Users.Commands;

namespace Travel.Domain.Service.Users.Handlers;

public class GetTokenHandler : IRequestHandler<GetTokenCommand, Result>
{
    private readonly IMediator _mediator;
    private readonly IOTPService _otpService;
    public GetTokenHandler(IMediator mediator, IOTPService otpService)
    {
        _mediator = mediator;
        _otpService = otpService;
    }

    public async Task<Result> Handle(GetTokenCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var vaild = _otpService.ValidateOtp(dto.UserName, dto.OTP);

        if (!vaild)
            return new Result(false, "Invalid OTP or OTP expired.");

        var token = await _mediator.Send(new GenerateTokenCommand(dto.UserName),cancellationToken);
        return new Result(true, "token = " + token);
    }
}
