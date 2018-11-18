using GestorEventos.BLL.Interfaces;
using GestorEventos.Core.Helpers;
using GestorEventos.Models.Entities;
using MailChimp.Net;
using MailChimp.Net.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorEventos.BLL
{
    public class MailingLogic : IMailingLogic
    {
        readonly IConfiguration _configuration;
        readonly IMailChimpManager _mailChimpManager;

        public MailingLogic(IConfiguration configuration)
        {
            _configuration = configuration;

            var mailchimpConfig = new MailchimpConfiguration();
            _configuration.GetSection("MailChimpOptions").Bind(mailchimpConfig);
            _mailChimpManager = new MailChimpManager(mailchimpConfig.MailChimpApiKey);
        }

        public bool SendQRCodeEmail(Participant participant)
        {
            // TODO:  
            throw new NotImplementedException();
        }

        public bool SendCancelationEmails(int eventId)
        {
            // TODO:  
            throw new NotImplementedException();
        }
    }
}
