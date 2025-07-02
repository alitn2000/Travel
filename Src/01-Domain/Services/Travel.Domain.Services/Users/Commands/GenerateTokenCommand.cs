using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Service.Users.Commands;

public class GenerateTokenCommand : IRequest<string>
{
    public string UserName { get; set; }
    public GenerateTokenCommand(string userName)
    {
        UserName = userName;
    }
}
