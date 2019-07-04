using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestorEventos.BLL.Interfaces;
using GestorEventos.Models.Entities;
using GestorEventos.Models.WebApiModels;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.WebApi.Controllers
{
    [Route("api/public")]
    public class PublicPagesController : Controller
    {
        private readonly IEventsLogic _eventsLogic;
        private readonly ISchedulesLogic _schedulesLogic;

        public PublicPagesController(IEventsLogic eventsLogic, ISchedulesLogic schedulesLogic)
        {
            _eventsLogic = eventsLogic;
            _schedulesLogic = schedulesLogic;
        }

        [Route("events")]
        [HttpGet]
        public IEnumerable<EventUI> GetAllEvents()
        {
            return _eventsLogic.GetEvents();
        }

        [Route("events/{id}")]
        [HttpGet]
        public Event GetEvent(int id)
        {
            return _eventsLogic.GetEvent(id);
        }

        [Route("event/{eventId}/schedule")]
        [HttpGet]
        public IEnumerable<ScheduleUI> GetSchedulesByEvent(int eventId)
        {
            try
            {
                return _schedulesLogic.GetSchedules(eventId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
