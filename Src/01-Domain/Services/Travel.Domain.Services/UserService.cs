using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Contracts.Services;

namespace Travel.Domain.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result> CheckUserExistById(int userId, CancellationToken cancellationToken)
    {
       var check = await _userRepository.ChekUSerExistById(userId, cancellationToken);
        if (check)
            return new Result(true,"User Exist");

        return new Result(false, "User Not Exist");
    }
}
