using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GestorEventos.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.WebApi.Controllers
{
    //[Authorize]
    [Route("api/upload")]
    public class FileUploadsController : Controller
    {
        private readonly IFilesLogic _filesLogic;

        public FileUploadsController(IFilesLogic filesLogic)
        {
            _filesLogic = filesLogic;
        }

        [Route("eventimage/{eventId}")]
        [HttpPost]
        public async Task<IActionResult> PostEventImageAsync(int eventId)
        {
            try
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                    var newFile = await _filesLogic.LoadEventImage(eventId, file);

                    return Ok(new { newFile });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
