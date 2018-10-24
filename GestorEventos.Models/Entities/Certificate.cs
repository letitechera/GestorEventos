using System;
using System.Collections.Generic;
using System.Text;

namespace GestorEventos.Models.Entities
{
    public class Certificate: BaseEntity
    {
        public Participant Participant { get; set; }
        public Event Event { get; set; }
        public bool Sent { get; set; }
        public string TemplateId { get; set; }
    }
}
