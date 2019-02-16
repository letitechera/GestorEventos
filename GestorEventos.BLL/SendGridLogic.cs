using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using GestorEventos.BLL.Interfaces;
using GestorEventos.DAL.Repositories.Interfaces;
using GestorEventos.Models.Entities;
using GestorEventos.Models.SendGridHelpers;
using GestorEventos.WebApi.Models;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace GestorEventos.BLL
{
    public class SendGridLogic : ISendGridLogic
    {
        readonly IConfiguration _configuration;
        readonly IRepository<Attendant> _attendantsRepository;
        readonly IRepository<Event> _eventsRepository;
        readonly IRepository<Participant> _participantsRepository;
        private readonly SendGridClient _client;
        private readonly SendGridOptions _options;
        private readonly EmailAddress _from;

        public SendGridLogic(IConfiguration configuration, IRepository<Attendant> attendantsRepository, IRepository<Participant> participantsRepository,
            IRepository<Event> eventsRepository)
        {
            _attendantsRepository = attendantsRepository;
            _eventsRepository = eventsRepository;
            _participantsRepository = participantsRepository;
            _configuration = configuration;

            _options = new SendGridOptions();
            _configuration.Bind("SendGridOptions", _options);
            _client = new SendGridClient(_options.APIKeyValue);
            _from = new EmailAddress(_options.FromEmail, _options.FromName);
        }

        #region Auth Mailing

        public async Task<Response> SendEmailValidation(string recipName, string recipEmail, string linkUrl)
        {
            var templateId = _options.TemplateEmailValidation;

            return await SendRegistrationEmail(recipName, recipEmail, templateId, linkUrl);
        }

        public async Task<Response> SendPasswordReset(string recipName, string recipEmail, string linkUrl)
        {
            var templateId = _options.TemplatePasswordReset;

            var recipient = new EmailAddress(recipEmail, recipName);
            var dynamicTemplateData = new ResetPasswordEmailData
            {
                LinkUrl = linkUrl,
            };

            return await SendTemplateEmail(recipient, templateId, dynamicTemplateData);
        }

        public async Task<Response> SendUserRegistrationAlert(string recipName, string recipEmail, string linkUrl)
        {
            var templateId = _options.TemplateUserRegistrationAlert;

            return await SendRegistrationEmail(recipName, recipEmail, templateId, linkUrl);
        }

        #endregion

        #region Events Mailing

        public async Task SendCancelationEmails(int eventId)
        {
            var _event = _eventsRepository.FindById(eventId);
            var participants = _participantsRepository.List(p => p.EventId == eventId);
            var linkUrl = _configuration.GetValue<string>("SiteOptions:EventDetails") + eventId;

            foreach (var item in participants)
            {
                var recipient = new EmailAddress(item.Email, item.FirstName + " " + item.LastName);
                var templateId = _options.TemplateEventCancelation;
                var dynamicTemplateData = new CampaignEmailData(item.FirstName + " " + item.LastName, linkUrl, _event.Name);

                await SendTemplateEmail(recipient, templateId, dynamicTemplateData);
            }
        }

        #endregion

        #region Accreditations Mailing

        public async Task<Response> SendQRCodeEmail(Participant participant, byte[] qrCode)
        {
            var attendant = _attendantsRepository.FindById(participant.AttendantId);
            var _event = _eventsRepository.FindById(participant.EventId);

            var recipient = new EmailAddress(attendant.Email, attendant.FullName);
            var templateId = _options.TemplateParticipantCode;
            var base64QR = Convert.ToBase64String(qrCode);
            var dynamicTemplateData = new ParticipantEmailData(_event, attendant.FullName, base64QR);

            return await SendTemplateEmail(recipient, templateId, dynamicTemplateData);
        }

        public async Task SendCampaignEmail(int eventId)
        {
            var _event = _eventsRepository.FindById(eventId);
            var attendants = _attendantsRepository.List();
            var linkUrl = _configuration.GetValue<string>("SiteOptions:EventDetails") + eventId;

            foreach (var item in attendants)
            {
                var recipient = new EmailAddress(item.Email, item.FullName);
                var templateId = _options.TemplateEventCampaign;
                var dynamicTemplateData = new CampaignEmailData(item.FullName, linkUrl, _event.Name);

                await SendTemplateEmail(recipient, templateId, dynamicTemplateData);
            }
        }

        #endregion

        #region Certificates Mailing

        public async Task<Response> SendCertificateEmail(string recipEmail, byte[] certificate)
        {
            var templateId = _options.TemplateCertificate;

            var recipient = new EmailAddress(recipEmail);

            string base64Cert = Convert.ToBase64String(certificate);

            return await SendAttachmentEmail(recipient, templateId, base64Cert);
        }

        #endregion

        #region Private Methods

        private async Task<Response> SendRegistrationEmail(string recipName, string recipEmail, string templateId, string linkUrl)
        {
            var recipient = new EmailAddress(recipEmail, recipName);

            var dynamicTemplateData = new AuthEmailData
            {
                RecipientName = recipName,
                LinkUrl = linkUrl
            };

            return await SendTemplateEmail(recipient, templateId, dynamicTemplateData);
        }

        private async Task<Response> SendTemplateEmail(EmailAddress recipient, string templateId, DynamicData dynamicTemplateData)
        {
            var msg = new SendGridMessage();

            msg.SetFrom(_from);

            var recipients = new List<EmailAddress> { recipient };
            msg.AddTos(recipients);

            msg.SetTemplateData(dynamicTemplateData);
            msg.TemplateId = templateId;

            return await _client.SendEmailAsync(msg);
        }

        private async Task<Response> SendAttachmentEmail(EmailAddress recipient, string templateId, string certificate)
        {
            var msg = new SendGridMessage();

            msg.SetFrom(_from);

            var recipients = new List<EmailAddress> { recipient };
            msg.AddTos(recipients);

            msg.AddAttachment("certificate.pdf", certificate, "application/pdf", "attachment", "banner");
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

        #endregion
    }
}
