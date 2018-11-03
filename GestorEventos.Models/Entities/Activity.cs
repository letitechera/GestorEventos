using System;
using System.Collections.Generic;
using System.Text;

namespace GestorEventos.Models.Entities
{
    public class Activity: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ActivityTypeId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int EventScheduleId { get; set; }
        public IList<Speaker> Speakers { get; set; }

        public virtual EventSchedule EventSchedule { get; set; }
        public virtual ActivityType ActivityType { get; set; }
    }
}
