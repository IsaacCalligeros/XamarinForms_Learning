using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middle_Manager_API.Helpers.Interfaces
{
    public interface IEmailHelper
    {
        Task SendEmail(string fromEmail, string fromName, string subject, string toEmail,
            string toName, string plainTextContent, string htmlContent);

        Task SendInviteEmail(string fromEmail, string fromName, string toEmail,
            string toName, string htmlContent);
    }
}
