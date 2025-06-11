using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.Contracts.Services;
using Travel.Domain.Core.DTOs.Login;

namespace Travel.Domain.Services.AppService;

public class UserAppService : IUserAppService
{
    private readonly IUserService _userService;

    public UserAppService(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result> CheckUserExistById(int userId, CancellationToken cancellationToken)
        => await _userService.CheckUserExistById(userId, cancellationToken);

    public async Task<Result> Login(LoginDto dto,CancellationToken cancellationToken)
        => await _userService.Login(dto,cancellationToken);
}
