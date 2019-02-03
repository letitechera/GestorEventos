namespace GestorEventos.Models.Requests.Account
{
    public class EditAccountRequest
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
        public string Image { get; set; }
    }
}