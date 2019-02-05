namespace GestorEventos.Models.Entities
{
    public class Participant: BaseEntity
    {
        public int AttendantId { get; set; }
        public int EventId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CellPhone { get; set; }
        public string QRCode { get; set; }

        public virtual Attendant Attendant { get; set; }
        public virtual Event Event { get; set; }
    }
}
