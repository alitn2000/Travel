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
using Travel.Domain.Service.Users.Commands;

namespace Travel.Domain.Service.Users.Handlers
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
                if (!await _userRepository.RegisterUser(dto.UserName, dto.UserNameType, cancellationToken))
                    return new Result(false, "User Registration Failed");
            }

            var OTP = _otpService.GenerateOtp();
            await _otpService.StoreOtp(dto.UserName, OTP, TimeSpan.FromMinutes(3));

            return new Result(true, "Your Login Code: " + OTP);
        }
    }
}
