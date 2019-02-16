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

        [HttpPost("import/xml")]
        public IActionResult ImportAttendantsByXml()
        {
            try
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    var uploaded = _filesLogic.ImportAttendantsFromXml(file);

                    if (uploaded)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NoContent();
                    }
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
