using System;
using System.Threading.Tasks;
using GestorEventos.BLL.Interfaces;
using GestorEventos.Core;
using GestorEventos.WebApi.Models;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace GestorEventos.BLL
{
    public class SendGridLogic : ISendGridLogic
    {
        readonly IConfiguration _configuration;
        private readonly SendGridClient _client;
        private readonly SendGridOptions _options;

        public SendGridLogic(IConfiguration configuration)
        {

            _configuration = configuration;
            _configuration.GetSection("SendGridOptions").Bind(_options);
            _client = new SendGridClient(_options.APIKeyValue);
        }

        public async Task<Response> SendSingleMail(string recipName, string recipEmail)
        {
            var from = new EmailAddress(_options.FromEmail, _options.FromName);
            var subject = Constants.SendGrid_NewEventSubject;
            var to = new EmailAddress(recipEmail, recipName);
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            return await _client.SendEmailAsync(msg);
        }

        public Task<Response> SendEmailValidation(string recipName, string recipEmail, string linkUrl)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> SendPasswordReset(string recipName, string recipEmail, string linkUrl)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> SendUserRegistrationAlert(string recipName, string recipEmail, string linkUrl)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> SendUserRegistrationByAdminAlert(string recipName, string recipEmail, string linkUrl)
        {
            throw new System.NotImplementedException();
        }
    }
}
