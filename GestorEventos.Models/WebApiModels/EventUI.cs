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
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string Topic { get; set; }
        public string CreatedById { get; set; }

        public EventUI(Event _event, string location, string topic)
        {
            Id = _event.Id;
            Name = _event.Name;
            Description = _event.Description;
            Image = _event.Name;
            Location = location ?? "N/A";
            StartDate = _event.StartDate;
            FinishDate = _event.EndDate;
            Topic = topic ?? "N/A";
            CreatedById = _event.CreatedById;
        }
    }
}
