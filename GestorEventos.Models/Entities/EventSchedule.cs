using System;
using System.Collections.Generic;
using System.Text;

namespace GestorEventos.Models.Entities
{
    public class EventSchedule: BaseEntity
    {
        public Event Event { get; set; }
        public DateTime Date { get; set; }
        public IList<Activity> Activities { get; set; }
    }
}
