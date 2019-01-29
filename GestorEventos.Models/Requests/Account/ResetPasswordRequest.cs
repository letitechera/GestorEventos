namespace GestorEventos.Models.DTO
{
    public class ResetPasswordRequest
    {
        public string Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
