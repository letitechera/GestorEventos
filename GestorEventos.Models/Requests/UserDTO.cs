using GestorEventos.Models.Entities;

namespace GestorEventos.Models.DTO
{
    public class UserDTO
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CellPhone { get; set; }
        public string Job { get; set; }
        public string Organization { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool Enabled { get; set; }
        public string Image { get; set; }

        public UserDTO(AppUser user)
        {
            UserId = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Phone = user.Phone;
            CellPhone = user.CellPhone;
            Job = user.Job;
            Organization = user.Organization;
            Address1 = user.Address1;
            Address2 = user.Address2;
            City = user.City;
            Country = user.Country;
        }
    }
}