using Newtonsoft.Json;

namespace GestorEventos.Models.SendGridHelpers
{
    public class CampaignEmailData: DynamicData
    {
        [JsonProperty("linkUrl")]
        public string LinkUrl { get; set; }

        public CampaignEmailData(string recipName, string linkUrl) : base()
        {
            RecipientName = recipName;
            LinkUrl = linkUrl;
        }
    }
}