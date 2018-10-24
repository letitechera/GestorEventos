using System;
using System.Collections.Generic;
using System.Text;

namespace GestorEventos.Models.Entities
{
    public class Checkpoint: BaseEntity
    {
        public Event Event { get; set; }
        public IList<Creditor> Creditors { get; set; }
        public string Point { get; set; }
    }
}
