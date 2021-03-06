﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorEventos.Models.Entities
{
    public class Speaker: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Nationality { get; set; }
        public string Company { get; set; }
        public string Contact { get; set; }
        public string Image { get; set; }
        public int ActivityId { get; set; }
        [JsonIgnore]
        public virtual Activity Activity { get; set; }
    }
}
