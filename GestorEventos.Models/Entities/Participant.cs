namespace GestorEventos.Models.Entities
{
    public class Participant: BaseEntity
    {
        public Attendant Attendant { get; set; }
        public Event Event { get; set; }
        public string QRCode { get; set; }
        public Certificate Certificate { get; set; }
        //Control de asistencia?
    }
}
