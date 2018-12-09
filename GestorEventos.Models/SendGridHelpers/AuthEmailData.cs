using Newtonsoft.Json;

namespace GestorEventos.Models.SendGridHelpers
{
    public class AuthEmailData: DynamicData
    {
        [JsonProperty("linkUrl")]
        public string LinkUrl { get; set; }
    }
}
