using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Contracts.Services;

namespace Travel.Domain.Service;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmail(string reciever, string subject, string body)
    {
        var smtpClient = new SmtpClient(_configuration["Email:SmtpServer"])
        {
            Port = int.Parse(_configuration["Email:SmtpPort"]),
            Credentials = new NetworkCredential(
                _configuration["Email:UserName"],
                _configuration["Email:Password"]),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_configuration["Email:SenderEmail"], _configuration["Email:SenderName"]),
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(reciever);
        await smtpClient.SendMailAsync(mailMessage);

    }

    public async Task GenerateEmail(string otp, string email, TimeSpan timeSpan)
    {
        var subject = "OTP Code registeration";
        var body = $"Your OTP code is: <b>{otp}</b>. It will expire in {timeSpan.TotalMinutes} minutes.";
        await SendEmail(email, subject, body);
    }
}
