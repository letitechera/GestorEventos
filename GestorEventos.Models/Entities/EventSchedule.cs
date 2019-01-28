using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GestorEventos.Models.Entities
{
    public class EventSchedule: BaseEntity
    {
        public DateTime? Date { get; set; }
        public int EventId { get; set; }

        public virtual Event Event { get; set; }
        //[JsonIgnore]
        public virtual IList<Activity> Activities { get; set; }

    }
}
