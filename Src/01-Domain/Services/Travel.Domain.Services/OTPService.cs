using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Contracts.Services;

namespace Travel.Domain.Service;

public class OTPService : IOTPService
{
    private readonly IMemoryCache _memoryCache;

    public OTPService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public string GenerateOtp()
    {
       var random = new Random();
        var otp = random.Next(1000, 9999).ToString();
        return otp;
    }

    public void StoreOtp(string key, string otp, TimeSpan timeSpan)
    {
        _memoryCache.Set(key, otp, timeSpan);
        var subject = "OTP Code registeration";
        var body = $"Your OTP code is: {otp}. It will expire in {timeSpan.TotalMinutes} minutes.";
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
