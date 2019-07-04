namespace GestorEventos.Models.Entities
{
    public class Certificate: BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string BadgeImage { get; set; }
        public bool Sent { get; set; }
        public string TemplateId { get; set; }
        public int ParticipantId { get; set; }

        public virtual Participant Participant { get; set; }
    }
}
