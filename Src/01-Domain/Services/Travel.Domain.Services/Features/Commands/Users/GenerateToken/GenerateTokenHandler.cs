using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Contracts.Repositories;

namespace Travel.Domain.Service.Features.Commands.Users.GenerateToken;

public class GenerateTokenHandler : IRequestHandler<GenerateTokenCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    public GenerateTokenHandler(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<string> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
    {
        var userName = request.UserName;
        var id = await _userRepository.GetUserIdByUserName(userName, cancellationToken);
        if (id == 0)
            return "user not found";
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
        new Claim(ClaimTypes.Name, userName),
        new Claim("Id",id.ToString())
        //new Claim(ClaimTypes.Email, dto.Email),
        //new Claim(ClaimTypes.Role, "User")                
    };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: signIn
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
