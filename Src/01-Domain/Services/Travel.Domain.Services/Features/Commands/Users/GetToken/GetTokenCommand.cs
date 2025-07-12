using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.DTOs.Login;

namespace Travel.Domain.Service.Features.Commands.Users.GetToken;

public class GetTokenCommand : IRequest<Result>
{
    public GetTokenDto Dto { get; set; }

    public GetTokenCommand(GetTokenDto dto)
    {
        Dto = dto;
    }
}
