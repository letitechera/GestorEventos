using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GestorEventos.Models.Entities
{
    public class Location : BaseEntity
    {
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int CityId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int Capacity { get; set; }

        public virtual City City { get; set; }

        [NotMapped]
        public string PrettyLocationAddress
        {
            get
            {
                return Address1 + ", " + (Address2 != null ? Address2 + ", " : "") + City;
            }
        }
    }
}
