namespace GestorEventos.Core.Helpers
{
    public class MailchimpConfiguration
    {
        public string MailChimpApiKey { get; set; }

        public string MailChimpApiBaseUrl { get; set; }

        public string MailChimpReplayToMail { get; set; }

        public string MailChimpFromName { get; set; }

        public int MailChimpCancelationTemplate { get; set; }
    }
}
