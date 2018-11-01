using System.Collections.Generic;

namespace GestorEventos.Models.Entities
{
    public class Organizer: AppUser
    {
        public int OrganizationId { get; set; }
        public string Image { get; set; }
        public IList<Event> Events { get; set; }

        public virtual Organization Organization { get; set; }
    }
}
