using System;
using System.Collections.Generic;

namespace GestorEventos.Models.Entities
{
    public class Event: BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
        public int OrganizerId { get; set; }
        public int EventTopicId { get; set; }
        public bool? Canceled { get; set; }
        public IList<EventSchedule> Schedules { get; set; }
        public IList<Participant> Participants { get; set; }

        public virtual Location Location { get; set; }
        public virtual Organizer Organizer { get; set; }
        public virtual EventTopic EventTopic { get; set; }
    }
}
