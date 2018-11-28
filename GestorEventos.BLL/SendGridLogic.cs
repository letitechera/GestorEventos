using System.Threading.Tasks;
using GestorEventos.BLL.Interfaces;
using SendGrid;

namespace GestorEventos.BLL
{
    public class SendGridLogic : ISendGridLogic
    {
        public Task<Response> SendEmailValidation(string recipName, string recipEmail, string linkUrl)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> SendPasswordReset(string recipName, string recipEmail, string linkUrl)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> SendUserRegistrationAlert(string recipName, string recipEmail, string linkUrl)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> SendUserRegistrationByAdminAlert(string recipName, string recipEmail, string linkUrl)
        {
            throw new System.NotImplementedException();
        }
    }
}
