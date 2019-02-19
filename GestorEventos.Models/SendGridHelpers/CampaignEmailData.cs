using Newtonsoft.Json;

namespace GestorEventos.Models.SendGridHelpers
{
    public class CampaignEmailData: DynamicData
    {
        [JsonProperty("linkUrl")]
        public string LinkUrl { get; set; }

        [JsonProperty("eventName")]
        public string EventName { get; set; }

        [JsonProperty("eventInfo")]
        public string EventInfo { get; set; }

        public CampaignEmailData(string recipName, string linkUrl, string eventName, string eventInfo = null) : base()
        {
            RecipientName = recipName;
            LinkUrl = linkUrl;
            EventName = eventName;
            EventInfo = eventInfo;
        }
    }
}