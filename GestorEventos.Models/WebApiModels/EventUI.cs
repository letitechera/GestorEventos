using GestorEventos.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorEventos.Models.WebApiModels
{
    public class EventUI
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Location { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }
        public string Topic { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedById { get; set; }

        public EventUI(Event _event, string location, string topic)
        {
            Id = _event.Id;
            Name = _event.Name;
            Description = _event.Description;
            Image = _event.Name;
            Location = location ?? "N/A";
            StartDate = _event.PrettyShortStartDate;
            FinishDate = _event.PrettyShortEndDate;
            StartTime = _event.PrettyStartTime;
            FinishTime = _event.PrettyEndTime;
            Topic = topic ?? "N/A";
            CreatedByName = _event.CreatedByName;
            CreatedById = _event.CreatedById;
        }
    }
}
