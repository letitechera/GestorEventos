using System.Collections.Generic;

namespace GestorEventos.Models.Entities
{
    public class Organizer: AppUser
    {
        public Organization Organization { get; set; }
        public string Image { get; set; }
        public IList<Event> Events { get; set; }
    }
}
