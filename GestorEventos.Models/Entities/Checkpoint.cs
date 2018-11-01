using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorEventos.Models.Entities
{
    public class Checkpoint: BaseEntity
    {
        public string Point { get; set; }
        public int EventId { get; set; }
        public IList<Creditor> Creditors { get; set; }

        public virtual Event Event { get; set; }
    }
}
