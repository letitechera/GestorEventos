using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestorEventos.BLL.Interfaces;
using GestorEventos.Core;
using GestorEventos.WebApi.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace GestorEventos.BLL
{
    public class SendGridLogic : ISendGridLogic
    {
        readonly IConfiguration _configuration;
        private readonly SendGridClient _client;
        private readonly SendGridOptions _options;
        private readonly EmailAddress _from;

        public SendGridLogic(IConfiguration configuration)
        {

            _configuration = configuration;
            _options = new SendGridOptions();
            _configuration.Bind("SendGridOptions", _options);
            _client = new SendGridClient(_options.APIKeyValue);
            _from = new EmailAddress(_options.FromEmail, _options.FromName);
        }

        public async Task<Response> SendEmailValidation(string recipName, string recipEmail, string linkUrl)
        {
            var templateId = _options.TemplateEmailValidation;

            return await SendRegistrationEmail(recipName, recipEmail, templateId, linkUrl);
        }

        public async Task<Response> SendPasswordReset(string recipName, string recipEmail, string linkUrl)
        {
            var templateId = _options.TemplatePasswordReset;

            return await SendRegistrationEmail(recipName, recipEmail, templateId, linkUrl);
        }

        public async Task<Response> SendUserRegistrationAlert(string recipName, string recipEmail, string linkUrl)
        {
            var templateId = _options.TemplateUserRegistrationAlert;

            return await SendRegistrationEmail(recipName, recipEmail, templateId, linkUrl);
        }

        //public Task<Response> SendUserRegistrationByAdminAlert(string recipName, string recipEmail, string linkUrl)
        //{
        //    throw new System.NotImplementedException();
        //}

        #region Private Methods

        private async Task<Response> SendRegistrationEmail(string recipName, string recipEmail, string templateId, string linkUrl)
        {
            var recipient = new EmailAddress(recipEmail, recipName);

            var dynamicTemplateData = new TemplateData
            {
                ParticipantName = recipName,
                LinkUrl = linkUrl
            };

            return await SendTemplateEmail(recipient, templateId, dynamicTemplateData);
        }

        private async Task<Response> SendTemplateEmail(EmailAddress recipient, string templateId, TemplateData dynamicTemplateData)
        {
            var msg = new SendGridMessage();

            msg.SetFrom(_from);

            var recipients = new List<EmailAddress> { recipient };
            msg.AddTos(recipients);

            msg.SetTemplateData(dynamicTemplateData);
            msg.TemplateId = templateId;

            return await _client.SendEmailAsync(msg);
        }

        public async Task<Response> SendHtmlEmail(EmailAddress recipient, string body, string subject)
        {
            var msg = new SendGridMessage();

            msg.SetFrom(_from);

            var recipients = new List<EmailAddress> { recipient };
            msg.AddTos(recipients);

            msg.SetSubject(subject);
            msg.AddContent(MimeType.Html, body);

            return await _client.SendEmailAsync(msg);
        }

        //DYNAMIC DATA HELPER:

        private class TemplateData
        {
            [JsonProperty("participantName")]
            public string ParticipantName { get; set; }

            [JsonProperty("linkUrl")]
            public string LinkUrl { get; set; }
        }

        #endregion
    }
}
