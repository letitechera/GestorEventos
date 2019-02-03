using GestorEventos.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorEventos.Models.WebApiModels
{
    public class ScheduleUI
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int EventId { get; set; }
        public virtual IList<ActivityUI> Activities { get; set; }

        public ScheduleUI(EventSchedule e)
        {
            Id = e.Id;
            Date = e.Date;
            EventId = e.EventId;
            Activities = new List<ActivityUI>();
        }
    }
}
