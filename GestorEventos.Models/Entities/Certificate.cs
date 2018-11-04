namespace GestorEventos.Models.Entities
{
    public class Certificate: BaseEntity
    {
        public bool Sent { get; set; }
        public string TemplateId { get; set; }
        public int ParticipantId { get; set; }

        public virtual Participant Participant { get; set; }
    }
}
