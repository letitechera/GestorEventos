using GestorEventos.Models.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorEventos.Models.SendGridHelpers
{
    public class EventEmailData : DynamicData
    {
        [JsonProperty("eventName")]
        public string EventName { get; set; }

        [JsonProperty("eventDescription")]
        public string EventDescription { get; set; }

        [JsonProperty("eventImage")]
        public string EventImage { get; set; }

        [JsonProperty("eventTopic")]
        public string EventTopic { get; set; }

        [JsonProperty("locationName")]
        public string LocationName { get; set; }

        [JsonProperty("locationAddress")]
        public string LocationAddress { get; set; }

        [JsonProperty("startDate")]
        public string StartDate { get; set; }

        [JsonProperty("endDate")]
        public string EndDate { get; set; }

        [JsonProperty("startTime")]
        public string StartTime { get; set; }

        [JsonProperty("endTime")]
        public string EndTime { get; set; }

        [JsonProperty("scheduleUrl")]
        public string ScheduleUrl { get; set; }

        public EventEmailData(Event _event)
        {
            EventName = _event.Name;
            EventDescription = _event.Description;
            EventImage = _event.Image;
            EventTopic = _event.EventTopic.Name;
            LocationName = _event.Location.Name;
            LocationAddress = _event.Location.Address1;
            StartDate = _event.PrettyStartDate;
            StartTime = _event.PrettyStartTime;
            EndDate = _event.PrettyEndDate;
            EndTime = _event.PrettyEndTime;
            ScheduleUrl = _event.Id.ToString();
        }
    }
}
