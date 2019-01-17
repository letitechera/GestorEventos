using System;
using System.Collections.Generic;
using GestorEventos.BLL.Interfaces;
using GestorEventos.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestorEventos.WebApi.Controllers
{
    [Authorize]
    [Route("api/speakers")]
    public class SpeakersController : Controller
    {
        private readonly ISpeakersLogic _speakersLogic;

        public SpeakersController(ISpeakersLogic speakersLogic)
        {
            _speakersLogic = speakersLogic;
        }

        [Route("{activityId}/speakers")]
        [HttpGet]
        public IEnumerable<Speaker> GetAllSpeakers(int activityId)
        {
            return _speakersLogic.GetSpeakers(activityId);
        }

        [HttpGet("{id}")]
        public Speaker GetSpeaker(int id)
        {
            try
            {
                return _speakersLogic.GetSpeaker(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Route("CreateSpeaker")]
        [HttpPost]
        public IActionResult CreateSpeaker([FromBody]Speaker speaker)
        {
            if (_speakersLogic.SaveSpeaker(speaker))
            {
                return Ok(speaker);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }


        [Route("UpdateSpeaker")]
        [HttpPut]
        public IActionResult UpdateSpeaker([FromBody]Speaker speaker)
        {
            if (_speakersLogic.SaveSpeaker(speaker, true))
            {
                return Ok(speaker);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("DeleteSpeaker/{id}")]
        public IActionResult DeleteSpeaker(int speakerId)
        {
            if (_speakersLogic.DeleteSpeaker(speakerId))
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

    }
}
