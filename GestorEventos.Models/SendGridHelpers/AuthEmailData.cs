using Newtonsoft.Json;

namespace GestorEventos.Models.SendGridHelpers
{
    public class AuthEmailData: DynamicData
    {
        [JsonProperty("linkUrl")]
        public string LinkUrl { get; set; }
        [JsonProperty("eventName")]
        public string EventName { get; set; }

        public AuthEmailData(string eventName = null) : base()
        {
            EventName = eventName;
        }
    }
}
