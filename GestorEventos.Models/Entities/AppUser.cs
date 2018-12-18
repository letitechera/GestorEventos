using System;
using Microsoft.AspNetCore.Identity;

namespace GestorEventos.Models.Entities
{
    public class AppUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Enabled { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Description { get; set; }
    }
}
