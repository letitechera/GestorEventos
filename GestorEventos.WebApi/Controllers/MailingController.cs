using System.Threading.Tasks;
using GestorEventos.BLL.Interfaces;
using GestorEventos.WebApi.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using SendGrid;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestorEventos.WebApi.Controllers
{
    [Route("api/mail")]
    public class MailingController : Controller
    {
        private readonly ISendGridLogic _sendGridLogic;

        public MailingController(ISendGridLogic sendGridLogic)
        {
            _sendGridLogic = sendGridLogic;
        }

        // POST api/<controller>
        [Route("send")]
        [HttpPost]
        public async Task<Response> Post([FromBody] RecipientDTO recipient)
        {
            var response = await _sendGridLogic.SendEmailValidation(recipient.Name, recipient.To, "www.google.com");
            return response;
        }
    }
}
