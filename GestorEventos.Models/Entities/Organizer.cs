using System.Collections.Generic;

namespace GestorEventos.Models.Entities
{
    public class Organizer: AppUser
    {
        public int OrganizationId { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public IList<Event> Events { get; set; }
    }
}
