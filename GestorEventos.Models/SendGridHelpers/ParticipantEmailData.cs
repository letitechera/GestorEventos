using GestorEventos.Models.Entities;
using Newtonsoft.Json;

namespace GestorEventos.Models.SendGridHelpers
{
    public class ParticipantEmailData : EventEmailData
    {
        [JsonProperty("QR")]
        public string QRCode { get; set; }

        public ParticipantEmailData(Event _event, string recipName, string qrCode) : base(_event)
        {
            RecipientName = recipName;
            QRCode = qrCode;
        }
    }
}
