namespace GestorEventos.Models.DTO
{
    public class ForgotPasswordRequest
    {
        public string Email { get; set; }
        public string Id { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
    }
}
