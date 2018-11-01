namespace GestorEventos.Models.Entities
{
    public class Participant: BaseEntity
    {
        public string QRCode { get; set; }
        public int AttendantId { get; set; }
        public int EventId { get; set; }
        public int CertificateId { get; set; }

        public virtual Attendant Attendant { get; set; }
        public virtual Event Event { get; set; }
        public virtual Certificate Certificate { get; set; }
        //Control de asistencia?
    }
}
