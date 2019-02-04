using System;
using System.Collections.Generic;
using System.IO;
using DinkToPdf;
using DinkToPdf.Contracts;
using GestorEventos.BLL.Interfaces;
using GestorEventos.Models.Entities;
using GestorEventos.Models.WebApiModels;
using GestorEventos.WebApi.Utility;
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
        private readonly ISendGridLogic _sendGridLogic;
        private IConverter _converter;

        public EventsController(IEventsLogic eventsLogic, ISendGridLogic sendGridLogic, IConverter converter)
        {
            _eventsLogic = eventsLogic;
            _sendGridLogic = sendGridLogic;
            _converter = converter;
        }

        [Route("all")]
        [HttpGet]
        public IEnumerable<EventUI> GetAllEvents()
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
        public EventUI GetEvent(int id)
        {
            try
            {
                return _eventsLogic.GetEventUI(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet("{id}/whole")]
        public Event GetWholeEvent(int id)
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

        [HttpDelete("DeleteEvent/{eventId}")]
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

        [Route("RegisterToEvent/{eventId}")]
        [HttpPost]
        public IActionResult RegisterToEvent(int eventId, [FromBody]Attendant attendant)
        {
            var qrCode = _eventsLogic.RegisterToEvent(eventId, attendant);
            if (qrCode != null)
            {
                return Ok(qrCode);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [Route("CreateTopic/{name}")]
        [HttpPost]
        public IActionResult CreateEventTopic(string name)
        {
            if (_eventsLogic.CreateEventTopic(name))
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("DeleteTopic/{topicId}")]
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

        [Route("Accredit/{participantId}")]
        [HttpGet]
        public IActionResult Accredit(int participantId)
        {
            var result = _eventsLogic.Accredit(participantId);
            return Ok(result);
        }

        [Route("{eventId}/participants")]
        [HttpGet]
        public IEnumerable<Participant> GetParticipants(int eventId)
        {
            return _eventsLogic.GetParticipants(eventId);
        }

        [Route("certificate/{participantId}")]
        [HttpGet]
        public IActionResult CreateCertificate(int participantId)
        {
            var participant = _eventsLogic.GetParticipant(participantId);

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "Certificate",
                Out = string.Format(@"C:\Certificates\{0}_{1}.pdf", participant.Event.Name, participant.Attendant.FullName)
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = TemplateGenerator.GetHTMLString(participant),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            _converter.Convert(pdf);

            return Ok("Successfully created PDF document.");
        }
    }
}