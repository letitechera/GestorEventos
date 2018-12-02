using GestorEventos.Models.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorEventos.Models.SendGridHelpers
{
    public class ParticipantEmailData : EventEmailData
    {
        [JsonProperty("QR")]
        public string QRCOde { get; set; }

        public ParticipantEmailData(Event _event, string recipName, string qrCode) : base(_event)
        {
            RecipientName = recipName;
            QRCOde = qrCode;
        }
    }
}
