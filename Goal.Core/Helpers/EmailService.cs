using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Goal.Core.Helpers
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendActivationEmail(string toEmail, string UserName, string Link)
        {
            var fromEmail = _config["EmailSettings:From"];
            var password = _config["EmailSettings:Password"];

            var from = new MailAddress(fromEmail,"Goal");
            var to = new MailAddress(toEmail);

            string subject = "Activation Account";
            string body = $@"
            Hello {UserName},<br><br>
            To Active your account click here: <br>
            <a href='{Link}'>Active Acount<\a><br><br> thank you.";
            using var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(from.Address, password),
                EnableSsl = true
            };
            using var message = new MailMessage(from, to)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            await smtp.SendMailAsync(message);
        }

    }
}
