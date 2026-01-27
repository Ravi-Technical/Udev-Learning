using Microsoft.Extensions.Options;
using MimeKit;
using System.Net;
using System.Net.Mail;
using Udemy_Backend.Interface;
using Udemy_Backend.Models;
using MailKit.Net.Smtp;


namespace Udemy_Backend.Services
{
    public class MailService : IEmailService
    {

        private readonly IConfiguration _configuration;
        public MailService(IConfiguration configuration) {
              _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("OTP Login", _configuration["Email:From"]));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart("plain") { Text = body};
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            await smtp.ConnectAsync(_configuration["Email:SmtpHost"], int.Parse(_configuration["Email:SmtpPort"]), false);
            await smtp.AuthenticateAsync(_configuration["Email:User"], _configuration["Email:Pass"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

    }
}
