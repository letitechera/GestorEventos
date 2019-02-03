using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestorEventos.BLL.Interfaces;
using GestorEventos.Models.WebApiModels;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.WebApi.Controllers
{
    [Route("api/public")]
    public class PublicPagesController : Controller
    {
        private readonly IEventsLogic _eventsLogic;

        public PublicPagesController(IEventsLogic eventsLogic)
        {
            _eventsLogic = eventsLogic;
        }

        [Route("all")]
        [HttpGet]
        public IEnumerable<EventUI> GetAllEvents()
        {
            return _eventsLogic.GetEvents();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
