using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorEventos.Models.Entities
{
    public class Attendant : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CellPhone { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public Attendant() { }

        public Attendant(Participant p)
        {
            FirstName = p.FirstName;
            LastName = p.LastName;
            Email = p.Email;
            Phone = p.Phone;
            CellPhone = p.CellPhone;
        }
    }
}
