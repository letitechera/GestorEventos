namespace GestorEventos.Models.Entities
{
    public class Creditor: AppUser
    {
        public int CheckpointId { get; set; }
        public virtual Checkpoint Checkpoint { get; set; }
    }
}
