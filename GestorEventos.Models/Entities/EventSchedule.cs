using System;
using System.Collections.Generic;
using System.Text;

namespace GestorEventos.Models.Entities
{
    public class EventSchedule: BaseEntity
    {
        public DateTime? Date { get; set; }
        public int EventId { get; set; }
        public IList<Activity> Activities { get; set; }

        public virtual Event Event { get; set; }
    }
}
