using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using Middle_Manager_API.Helpers.Interfaces;
using SharedClassLibrary;

namespace Middle_Manager_API.Helpers
{
    public class EmailHelper : IEmailHelper
    {
        private readonly IConfiguration _config;
        private readonly string Key;

        public EmailHelper(IConfiguration config)
        {
            _config = config;
            Key = _config.GetSection("AppSettings:SendGridKey").Value;
        }

        public async Task SendEmail(string fromEmail, string fromName, string subject, string toEmail,
            string toName, string plainTextContent, string htmlContent)
        {
            var client = new SendGridClient(Key);
            var from = new EmailAddress(fromEmail, fromName);
            var to = new EmailAddress(toEmail, toName);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        public async Task SendInviteEmail(string fromEmail, string fromName, string toEmail,
            string toName, string htmlContent)
        {
            var client = new SendGridClient(Key);

            var from = new EmailAddress(fromEmail, fromName);
            var to = new EmailAddress(toEmail, toName);
            var msg = MailHelper.CreateSingleEmail(from, to, SharedResources.InviteSubject, String.Empty, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

    }
}
