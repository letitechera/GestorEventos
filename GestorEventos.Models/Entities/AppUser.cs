using Microsoft.AspNetCore.Identity;
using System;

namespace GestorEventos.Models
{
    public class AppUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Enabled { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
