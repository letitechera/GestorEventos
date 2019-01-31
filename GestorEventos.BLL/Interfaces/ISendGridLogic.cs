using GestorEventos.Models.Entities;
using SendGrid;
using System.Threading.Tasks;

namespace GestorEventos.BLL.Interfaces
{
    public interface ISendGridLogic
    {
        Task<Response> SendEmailValidation(string recipName, string recipEmail, string linkUrl);
        Task<Response> SendPasswordReset(string recipName, string recipEmail, string linkUrl, string code);
        Task<Response> SendUserRegistrationAlert(string recipName, string recipEmail, string linkUrl);
        //Task<Response> SendUserRegistrationByAdminAlert(string recipName, string recipEmail, string linkUrl);
        Task<Response> SendQRCodeEmail(Participant participant, byte[] qrCode);
        Task SendCampaignEmail(int eventId);
    }
}
