using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorEventos.Models.Entities
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }

        [JsonIgnore]
        public virtual IList<City> Cities { get; set; }
    }
}
