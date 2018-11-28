using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestorEventos.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public void Post([FromBody]string Name, [FromBody]string To)
        {
            _sendGridLogic.SendSingleMail(Name, To);
        }
    }
}
