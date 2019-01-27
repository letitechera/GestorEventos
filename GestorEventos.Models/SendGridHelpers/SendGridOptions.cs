namespace GestorEventos.WebApi.Models
{
    public class SendGridOptions
    {
        public string APIKeyName { get; set; }
        public string APIKeyValue { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string TemplateEmailValidation { get; set; }
        public string TemplatePasswordReset { get; set; }
        public string TemplateUserRegistrationAlert { get; set; }
        public string TemplateNewEvent { get; set; }
        public string TemplateRegistrationSuccess { get; set; }
        public string TemplateParticipantCode { get; set; }
        public string TemplateEventCampaign { get; set; }
    }
}