using System;
using System.Collections.Generic;
using GestorEventos.BLL.Interfaces;
using GestorEventos.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.WebApi.Controllers
{
    [Authorize]
    [Route("api/attendants")]
    public class AttendantsController : Controller
    {
        private readonly IAttendantsLogic _attendantsLogic;

        public AttendantsController(IAttendantsLogic attendantsLogic)
        {
            _attendantsLogic = attendantsLogic;
        }

        [Route("all")]
        [HttpGet]
        public IEnumerable<Attendant> GetAllAttendants()
        {
            return _attendantsLogic.GetAttendants();
        }

        [HttpGet("{id}")]
        public Attendant GetAttendant(int id)
        {
            try
            {
                return _attendantsLogic.GetAttendant(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Route("CreateAttendant")]
        [HttpPost]
        public IActionResult CreateAttendant([FromBody]Attendant attendant)
        {
            if (_attendantsLogic.SaveAttendant(attendant))
            {
                return Ok(attendant);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }


        [Route("UpdateAttendant")]
        [HttpPut]
        public IActionResult UpdateAttendant([FromBody]Attendant attendant)
        {
            if (_attendantsLogic.SaveAttendant(attendant, true))
            {
                return Ok(attendant);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("DeleteAttendant/{id}")]
        public IActionResult DeleteAttendant(int id)
        {
            if (_attendantsLogic.DeleteAttendant(id))
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
