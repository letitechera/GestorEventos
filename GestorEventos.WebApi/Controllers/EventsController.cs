using System;
using System.Collections.Generic;
using GestorEventos.BLL.Interfaces;
using GestorEventos.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.WebApi.Controllers
{
    //[Authorize]
    [Route("api/events")]
    public class EventsController : Controller
    {
        private readonly IEventsLogic _eventsLogic;

        public EventsController(IEventsLogic eventsLogic)
        {
            _eventsLogic = eventsLogic;
        }

        [Route("all")]
        [HttpGet]
        public IEnumerable<Event> GetAllEvents()
        {
            return _eventsLogic.GetEvents();
        }

        [HttpGet("{id}")]
        public Event GetEvent(int id)
        {
            try
            {
                return _eventsLogic.GetEvent(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Route("CreateEvent")]
        [HttpPost]
        public IActionResult CreateEvent([FromBody]Event _event)
        {
            if (_eventsLogic.SaveEvent(_event))
            {
                return Ok(_event);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [Route("UpdateEvent")]
        [HttpPut]
        public IActionResult UpdateEvent([FromBody]Event _event)
        {
            if (_eventsLogic.SaveEvent(_event, true))
            {
                return Ok(_event);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("DeleteEvent/{id}")]
        public IActionResult DeleteEvent(int eventId)
        {
            if (_eventsLogic.DeleteEvent(eventId))
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [Route("CancelEvent")]
        [HttpPost]
        public IActionResult PostTopic([FromBody]int eventId)
        {
            if (_eventsLogic.CancelEvent(eventId))
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [Route("CreateEventTopic")]
        [HttpPost]
        public IActionResult PostTopic([FromBody]string topicName)
        {
            if (_eventsLogic.CreateEventTopic(topicName))
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("DeleteTopic/{id}")]
        public IActionResult DeleteTopic(int eventId)
        {
            if (_eventsLogic.DeleteEvent(eventId))
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
