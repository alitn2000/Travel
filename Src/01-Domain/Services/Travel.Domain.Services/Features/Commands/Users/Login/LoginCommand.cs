using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.DTOs.Login;

namespace Travel.Domain.Service.Features.Commands.Users.Login;

public class LoginCommand : IRequest<Result>
{
    public LoginDto Dto{ get; set; }
    public LoginCommand(LoginDto dto)
    {
        Dto = dto;
    }
}
