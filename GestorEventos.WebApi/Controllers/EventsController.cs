using System;
using System.Collections.Generic;
using GestorEventos.BLL.Interfaces;
using GestorEventos.Models.Entities;
using GestorEventos.Models.WebApiModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.WebApi.Controllers
{
    [Authorize]
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

        [Route("all/{userId}")]
        [HttpGet]
        public IEnumerable<EventUI> GetAllEventsByUser(string userId)
        {
            return _eventsLogic.GetEvents(userId);
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
        public IActionResult CancelEvent([FromBody]int eventId)
        {
            if (_eventsLogic.CancelEvent(eventId))
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [Route("RegisterToEvent")]
        [HttpPut]
        public IActionResult RegisterToEvent([FromBody]int eventId, [FromBody]Attendant attendant)
        {
            if (_eventsLogic.RegisterToEvent(eventId, attendant))
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [Route("CreateTopic")]
        [HttpPost]
        public IActionResult CreateEventTopic([FromBody]string topicName)
        {
            if (_eventsLogic.CreateEventTopic(topicName))
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("DeleteTopic/{id}")]
        public IActionResult DeleteEventTopic(int topicId)
        {
            if (_eventsLogic.DeleteEventTopic(topicId))
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [Route("topics")]
        [HttpGet]
        public IEnumerable<EventTopic> GetAllTopics()
        {
            return _eventsLogic.GetAllTopics();
        }

        [Route("accredit")]
        [HttpGet]
        public IActionResult Accredit(string qrCode)
        {
            var result = _eventsLogic.Accredit(qrCode);
            return Ok(result);
        }
    }
}