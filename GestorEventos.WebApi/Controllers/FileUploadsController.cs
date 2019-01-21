using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GestorEventos.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestorEventos.WebApi.Controllers
{
    [Authorize]
    [Route("api/fileupload")]
    public class FileUploadsController : Controller
    {
        private readonly IEventsLogic _eventsLogic;

        public FileUploadsController(IEventsLogic eventsLogic)
        {
            _eventsLogic = eventsLogic;
        }

        [Route("eventimage/{id}")]
        [HttpPost]
        public void PostEventImage(int id, [FromBody]object file)
        {
            try
            {
                //_eventsLogic.SaveImage(id, file);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
