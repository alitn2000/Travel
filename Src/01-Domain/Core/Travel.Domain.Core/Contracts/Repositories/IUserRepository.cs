using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.DTOs.Login;
using Travel.Domain.Core.DTOs.TripDtos;
using Travel.Domain.Core.Entities.UserManagement;
using Travel.Domain.Core.Enums;

namespace Travel.Domain.Core.Contracts.Repositories;

public interface IUserRepository
{
    Task<bool> ChekUSerExistById(int userId, CancellationToken cancellationToken);
    Task<bool> CheckUserExistByUserName(LoginDto dto, CancellationToken cancellationToken);
    Task<bool> RegisterUser(User user, CancellationToken cancellationToken);
    Task<int> GetUserIdByUserName(string userName, CancellationToken cancellationToken);
    Task<User?> GetUserWithProfileById(int userId, CancellationToken cancellationToken);
    Task UpdateUserProfile(User user, CancellationToken cancellationToken);
    Task<List<GetUsersTripDto>> GetUsersTripsByUserId(int userId, CancellationToken cancellationToken);
    Task<User?> GetUserById(int userId, CancellationToken cancellationToken);
}
