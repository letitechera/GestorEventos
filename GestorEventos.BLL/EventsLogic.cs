using System;
using System.Collections.Generic;
using System.Text;
using GestorEventos.BLL.Interfaces;
using GestorEventos.DAL.Repositories.Interfaces;
using GestorEventos.Models.Entities;
using Microsoft.Extensions.Logging;

namespace GestorEventos.BLL
{
    public class EventsLogic : IEventsLogic
    {
        readonly IRepository<Event> _eventsRepository;

        public EventsLogic(IRepository<Event> eventsRepository)
        {
            _eventsRepository = eventsRepository;
        }

        public bool SaveEvent(Event _event, bool update = false)
        {
            try
            {
                if (update)
                {
                    _eventsRepository.Update(_event);

                }
                else
                    _eventsRepository.Add(_event);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool DeleteEvent(int eventId)
        {
            try
            {
                var r = _eventsRepository.FindById(eventId);
                _eventsRepository.Delete(eventId);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Event> GetEvents()
        {
            try
            {
                return _eventsRepository.List();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Event GetEvent(int eventId)
        {
            try
            {
                return _eventsRepository.FindById(eventId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
