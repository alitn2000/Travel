using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Core.Contracts.Services
{
    public interface IOTPService
    {
        string GenerateOtp();
        Task StoreOtp(string email, string otp, TimeSpan timeSpan);
        bool ValidateOtp(string key, string otp);
    }
}
