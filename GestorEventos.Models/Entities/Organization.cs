using System.Collections.Generic;

namespace GestorEventos.Models.Entities
{
    public class Organization: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public IList<Organizer> Organizers { get; set; }
    }
}
