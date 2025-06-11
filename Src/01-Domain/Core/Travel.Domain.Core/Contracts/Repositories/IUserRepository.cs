using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.DTOs.Login;
using Travel.Domain.Core.Enums;

namespace Travel.Domain.Core.Contracts.Repositories;

public interface IUserRepository
{
    Task<bool> ChekUSerExistById(int userId, CancellationToken cancellationToken);
    Task<bool> Login(LoginDto dto, CancellationToken cancellationToken);
    Task<bool> CheckUserExistByUserName(LoginDto dto, CancellationToken cancellationToken);
    Task<bool> RegisterUser(string userName, UserNameEnum userNameEnum, CancellationToken cancellationToken);
}
