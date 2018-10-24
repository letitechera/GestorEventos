using System;

namespace GestorEventos.Models.Entities
{
    public class Attendant: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
