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
        public string Address { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Topic { get; set; }
        public string CreatedById { get; set; }
        public float? Percentage { get; set; }

        public EventUI(Event _event)
        {
            Id = _event.Id;
            Name = _event.Name;
            Description = _event.Description;
            Image = _event.Image;
            Location = _event.Location.Name ?? "N/A";
            Address = _event.Location.Address1 + 
                (string.IsNullOrEmpty(_event.Location.Address2) 
                ? "" : ", "+ _event.Location.Address2);
            StartDate = _event.StartDate;
            EndDate = _event.EndDate;
            Topic = _event.EventTopic.Name;
            CreatedById = _event.CreatedById;
            Percentage = _event.AttendancePercentage;
        }
    }
}
