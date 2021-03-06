﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace GestorEventos.Models.Entities
{
    public class Event : BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Image { get; set; }
        public string SmallImage { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
        public int EventTopicId { get; set; }
        public bool? Canceled { get; set; }
        public float? AttendancePercentage { get; set; }

        public virtual Location Location { get; set; }
        public virtual EventTopic EventTopic { get; set; }

        [JsonIgnore]
        public virtual IList<EventSchedule> Schedules { get; set; }
        [JsonIgnore]
        public virtual IList<Participant> Participants { get; set; }

        [NotMapped]
        public string PrettyStartDate
        {
            get
            {
                return StartDate.ToString("dddd d MMMM, yyyy", CultureInfo.CurrentUICulture);
            }
        }

        [NotMapped]
        public string PrettyEndDate
        {
            get
            {
                return EndDate.ToString("dddd d MMMM, yyyy", CultureInfo.CurrentUICulture);
            }
        }

        [NotMapped]
        public string PrettyStartTime
        {
            get
            {
                return StartDate.ToString("hh:mm");
            }
        }

        [NotMapped]
        public string PrettyEndTime
        {
            get
            {
                return EndDate.ToString("hh:mm");
            }
        }
    }
}
