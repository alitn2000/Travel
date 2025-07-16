using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Contracts.Services;
using Travel.Domain.Core.DTOs.Login;
using Travel.Domain.Core.Entities.UserManagement;
using Travel.Domain.Core.Enums;
using Travel.Domain.Service.Exceptions;

namespace Travel.Domain.Service.Features.Commands.Users.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOTPService _otpService;
        public LoginHandler(IUserRepository userRepository, IOTPService otpService)
        {
            _userRepository = userRepository;
            _otpService = otpService;
        }

        public async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            if (!await _userRepository.CheckUserExistByUserName(dto, cancellationToken))
            {
                var newUser = new User(dto.UserName, dto.UserNameType);

                

                if (!await _userRepository.RegisterUser(newUser, cancellationToken))
                    throw new CommandValidationException("User Registration Failed");
            }

            var OTP = _otpService.GenerateOtp();
            await _otpService.StoreOtp(dto.UserName, OTP, TimeSpan.FromMinutes(3));

            return new Result(true, "Your Login Code: " + OTP);
        }
    }
}
