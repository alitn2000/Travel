using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Repositories;

namespace Travel.Domain.Service.Features.Queries.Users.CheckUserExistById;

public class CheckUserExistByIdHandler : IRequestHandler<CheckUserExistByIdQuery, Result>
{
    private readonly IUserRepository _userRepository;

    public CheckUserExistByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Result> Handle(CheckUserExistByIdQuery request, CancellationToken cancellationToken)
    {
        var exists = await _userRepository.ChekUSerExistById(request.UserId, cancellationToken);
        if (exists)
            return new Result(true, "User exists");

        return new Result(false, "User not found");
    }
}
