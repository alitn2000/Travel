using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Contracts.Services;

namespace Travel.Domain.Service.NewFolder;

public class OTPService : IOTPService
{
    private readonly IMemoryCache _memoryCache;
    //private readonly IEmailService _emailService;

    public OTPService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        //_emailService = emailService;
    }

    public string GenerateOtp()
    {
       var random = new Random();
        var otp = random.Next(1000, 9999).ToString();
        return otp;
    }

    public async Task StoreOtp(string email, string otp, TimeSpan timeSpan)
    {
        _memoryCache.Set(email, otp, timeSpan);
       
       
    }

    public bool ValidateOtp(string key, string otp)
    {
        if (_memoryCache.TryGetValue<string>(key, out var storedOtp))
        {
            return storedOtp == otp;
        }
        return false;
    }
}
