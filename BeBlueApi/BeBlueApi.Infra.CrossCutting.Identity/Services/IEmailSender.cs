using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeBlueApi.Infra.CrossCutting.Identity.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
