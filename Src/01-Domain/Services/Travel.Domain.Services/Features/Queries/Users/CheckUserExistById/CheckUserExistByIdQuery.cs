using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;

namespace Travel.Domain.Service.Features.Queries.Users.CheckUserExistById;

public class CheckUserExistByIdQuery : IRequest<Result>
{
    public int UserId { get; }

    public CheckUserExistByIdQuery(int userId)
    {
        UserId = userId;
    }
}
