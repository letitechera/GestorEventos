using System;
using System.Collections.Generic;

namespace GestorEventos.Models.Entities
{
    public class Event: BaseEntity
    {
        public string Name { get; set; }
        public Location Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public Organizer Organizer { get; set; }
        public EventTopic EventTopic { get; set; }
        public IList<EventSchedule> Schedules { get; set; }
        public IList<Creditor> Creditors { get; set; }
        public IList<Participant> Participants { get; set; }
        public IList<Checkpoint> Checkpoints { get; set; }

    }
}
