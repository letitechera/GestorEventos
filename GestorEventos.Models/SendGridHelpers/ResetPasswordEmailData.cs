using Newtonsoft.Json;

namespace GestorEventos.Models.SendGridHelpers
{
    public class ResetPasswordEmailData: DynamicData
    {
        [JsonProperty("linkUrl")]
        public string LinkUrl { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
