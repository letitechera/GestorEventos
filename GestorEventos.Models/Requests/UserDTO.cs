﻿using GestorEventos.Models.Entities;

namespace GestorEventos.Models.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public UserDTO(AppUser user)
        {
        }
    }
}