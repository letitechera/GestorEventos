using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GestorEventos.Models.Entities
{
    public class Activity: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ActivityTypeId { get; set; }
        public DateTime StartTime { get; set; }
        public int EventScheduleId { get; set; }

        public virtual EventSchedule EventSchedule { get; set; }
        public virtual ActivityType ActivityType { get; set; }
        
        public virtual IList<Speaker> Speakers { get; set; }
    }
}
