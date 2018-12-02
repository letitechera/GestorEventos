using Newtonsoft.Json;

namespace GestorEventos.Models.SendGridHelpers
{
    public class DynamicData
    {
        [JsonProperty("recipientName")]
        public string RecipientName { get; set; }
    }
}
