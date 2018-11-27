namespace GestorEventos.Models.DTO
{
    public class ResetPasswordRequest
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
    }
}
