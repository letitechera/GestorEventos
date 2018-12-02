﻿using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorEventos.Models.Entities
{
    public class Attendant: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [NotMapped]
        public string FullName
        {
            get {
                return FirstName + " " + LastName;
            }
        }
    }
}
