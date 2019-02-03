using GestorEventos.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestorEventos.Models.WebApiModels
{
    public class ActivityUI
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ActivityTypeId { get; set; }
        public int ScheduleId { get; set; }
        public DateTime StartTime { get; set; }
        public virtual IList<Speaker> Speakers { get; set; }

        public ActivityUI(Activity a)
        {
            Id = a.Id;
            Name = a.Name;
            Description = a.Description;
            ActivityTypeId = a.ActivityTypeId;
            ScheduleId = a.EventScheduleId;
            StartTime = a.StartTime;
            Speakers = a.Speakers.ToList();
        }
    }
}
