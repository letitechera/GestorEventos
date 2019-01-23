using System;
using System.Collections.Generic;
using System.Text;

namespace GestorEventos.Models.Entities
{
    public class City: BaseEntity
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}
