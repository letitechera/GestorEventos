﻿using Microsoft.AspNetCore.Identity;

namespace GestorEventos.Models
{
    public class AppUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Enabled { get; set; }
    }
}
