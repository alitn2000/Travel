﻿//using Microsoft.Extensions.Caching.Memory;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;
//using Travel.Domain.Core.BaseEntities;
//using Travel.Domain.Core.Contracts.Repositories;
//using Travel.Domain.Core.Contracts.Services;
//using Travel.Domain.Core.DTOs.Login;
//using Travel.Domain.Core.Entities;

//namespace Travel.Domain.Service.NewFolder;

//public class UserService : IUserService
//{
//    private readonly IUserRepository _userRepository;
//    private readonly IConfiguration _configuration;
//    private readonly IOTPService _otpService;
//    public UserService(IUserRepository userRepository, IConfiguration configuration, IOTPService otpService)
//    {
//        _userRepository = userRepository;
//        _configuration = configuration;
//        _otpService = otpService;
//    }

//    public async Task<Result> CheckUserExistById(int userId, CancellationToken cancellationToken)
//    {
//        var check = await _userRepository.ChekUSerExistById(userId, cancellationToken);
//        if (check)
//            return new Result(true, "User Exist");

//        return new Result(false, "User Not Exist");
//    }

//    public async Task<Result> Login(LoginDto dto, CancellationToken cancellationToken)
//    {
        
//        if (!await _userRepository.CheckUserExistByUserName(dto, cancellationToken))
//        {
//            if(!await _userRepository.RegisterUser(dto.UserName, dto.UserNameType, cancellationToken))
//                return new Result(false, "User Registration Failed");
//        }
        
//        var OTP = _otpService.GenerateOtp();
//        await _otpService.StoreOtp(dto.UserName, OTP, TimeSpan.FromMinutes(3));

//        return new Result(true, "Your Login Code: " + OTP);

//    }

//    public async Task<Result> GetToken(GetTokenDto dto, CancellationToken cancellationToken)
//    {
//        var vaild = _otpService.ValidateOtp(dto.UserName, dto.OTP);

//        if (!vaild)
//            return new Result(false, "Invalid OTP or OTP expired.");

//       var token = await GenerateToken(dto.UserName,cancellationToken);
//        return new Result(true, "token = " + token);

//    }

//    private async Task<string> GenerateToken(string userName, CancellationToken cancellationToken)
//    {
//        var id = await _userRepository.GetUserIdByUserName(userName, cancellationToken);
//        if (id == 0)
//            return "user not found";
//        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
//        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//        var claims = new[]
//        {
//        new Claim(ClaimTypes.Name, userName),
//        new Claim("Id",id.ToString())
//        //new Claim(ClaimTypes.Email, dto.Email),
//        //new Claim(ClaimTypes.Role, "User")                
//    };

//        var token = new JwtSecurityToken(
//            issuer: _configuration["Jwt:Issuer"],
//            audience: _configuration["Jwt:Audience"],
//            claims: claims,
//            expires: DateTime.UtcNow.AddMinutes(30),
//            signingCredentials: signIn
//        );

//        return new JwtSecurityTokenHandler().WriteToken(token);
//    }


//}
