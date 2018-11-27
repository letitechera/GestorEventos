using GestorEventos.BLL.Interfaces;
using GestorEventos.Core;
using GestorEventos.Core.Helpers;
using GestorEventos.DAL.Repositories.Interfaces;
using GestorEventos.Models.Entities;
using MailChimp.Net;
using MailChimp.Net.Interfaces;
using MailChimp.Net.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestorEventos.BLL
{
    public class EmailsLogic : IEmailsLogic
    {
        readonly IMailchimpLogic _mailchimpLogic;
        readonly IRepository<Participant> _participantRepository;

        public EmailsLogic(IMailchimpLogic mailchimpLogic, IRepository<Participant> participantRepository)
        {
            _mailchimpLogic = mailchimpLogic;
            _participantRepository = participantRepository;
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

        public bool DistributeEvent(int eventId)
        {
            var participants = _participantRepository.List(p => p.EventId == eventId);
            var members = new List<Member>();

            foreach (Participant participant in participants)
            {

                var member = _mailchimpLogic.GetMailChimpMemberFromParticipant(participant);
                member.MergeFields.Add(mergeField.Tag, reminder.Id);

                members.Add(member);

                if (reminder.ForcedContactMode == ContactMode.SMS)
                {
                    if (panelmember.Phone != null)
                        // SMS REMINDER DISTRIBUTION
                        SmsReminderDistribution(panelmember, reminder);
                }
            }

            // EMAIL REMINDER DISTRIBUTION
            if (members.Count > 0)
            {
                await AddOrUpdateMemberToMailChimp(members);
                await StartCampaign(reminder, mergeField);
            }
        }

        private async Task<MergeField> CreateMergeField(int eventId)
        {
            // Create Merge Field for event in contact list
            var mergeFieldName = $"E{eventId}";

            var mergeField = new MergeField
            {
                Name = mergeFieldName,
                Type = Constants.MailChimp_MergeFieldType_Text,
                HelpText = Constants.MailChimp_MergeFieldHelp_Event,
                Tag = mergeFieldName.ToUpper()
            };

            return await _mailchimpLogic.AddColumnToList(Constants.MailChimp_ContactListId, mergeField);
        }

        private async Task AddOrUpdateMemberToMailChimp(List<Member> members)
        {
            await members.ForEach(async member =>
            {
                await _mailchimpLogic.AddUserToList(Constants.MailChimp_ContactListId, member);
            }, 8);
        }
    }
}
