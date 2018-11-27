using GestorEventos.BLL.Interfaces;
using GestorEventos.Core.Helpers;
using MailChimp.Net;
using MailChimp.Net.Core;
using MailChimp.Net.Interfaces;
using MailChimp.Net.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestorEventos.BLL
{
    public class MailchimpLogic : IMailchimpLogic
    {
        readonly IConfiguration _configuration;
        readonly IMailChimpManager _mailChimpManager;

        public MailchimpLogic(IConfiguration configuration)
        {
            _configuration = configuration;
            var mailchimpConfig = new MailchimpConfiguration();
            _configuration.GetSection("MailChimpOptions").Bind(mailchimpConfig);
            _mailChimpManager = new MailChimpManager(mailchimpConfig.MailChimpApiKey);
        }

        public async Task<Member> AddUserToList(string contactListId, Member member)
        {
            await EnsureUserColumns(contactListId);

            try
            {
                return await _mailChimpManager.Members.AddOrUpdateAsync(contactListId, member);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task EnsureUserColumns(string contactListId)
        {
            var currentFields = (await _mailChimpManager.MergeFields.GetAllAsync(contactListId)).ToList();

            var fields = new List<string>
                {
                    Constants.MailChimp_MergeField_Age,
                    Constants.MailChimp_MergeField_Education,
                    Constants.MailChimp_MergeField_Ethnicity,
                    Constants.MailChimp_MergeField_FName,
                    Constants.MailChimp_MergeField_Gender,
                    Constants.MailChimp_MergeField_Income,
                    Constants.MailChimp_MergeField_LName,
                    Constants.MailChimp_MergeField_Parent,
                    Constants.MailChimp_MergeField_Phone,
                    Constants.MailChimp_MergeField_Race,
                    Constants.MailChimp_MergeField_Zipcode
                };

            foreach (var field in fields)
            {
                if (currentFields.All(f => f.Tag != field))
                {
                    var newField = new MergeField
                    {
                        Name = field,
                        Type = Constants.MailChimp_MergeFieldType_Text,
                        HelpText = Constants.MailChimp_MergeFieldHelp_DataField,
                        Tag = field.ToUpper()
                    };

                    await _mailChimpManager.MergeFields.AddAsync(contactListId, newField);
                }
            }
        }

        private async Task<Content> SetCampaignTemplate(string campaignId, int templateId, Dictionary<string, object> sections)
        {
            var contentRequest = new ContentRequest
            {
                Template = new ContentTemplate
                {
                    Id = templateId,
                    Sections = sections
                }
            };

            return await _mailChimpManager.Content.AddOrUpdateAsync(campaignId, contentRequest);
        }

        public async Task<Content> SetCampaignTemplate(string campaignId, int templateId, string linkField, string linkValue, string contentWithMergeFields)
        {
            var sections = new Dictionary<string, object> {
                        {
                            Constants.MailChimp_SurveyLink ,
                            $@"<a href='*|{linkField.ToUpper()}|*'>{
                                await GetSurveyLinkDefaultContent(templateId.ToString()) ?? "Take Survey!"
                            }</a>"
                        },
                        {
                            Constants.MailChimp_Study_Content , contentWithMergeFields
                        }
            };

            return await SetCampaignTemplate(campaignId, templateId, sections);
        }

        public async Task StartCampaign(string campaignId)
        {
            await _mailChimpManager.Campaigns.SendAsync(campaignId);
        }
    }
}
