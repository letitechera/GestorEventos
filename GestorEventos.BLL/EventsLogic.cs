using System;
using System.Linq;
using System.Collections.Generic;
using GestorEventos.BLL.Interfaces;
using GestorEventos.DAL.Repositories.Interfaces;
using GestorEventos.Models.Entities;

namespace GestorEventos.BLL
{
    public class EventsLogic : IEventsLogic
    {
        readonly IRepository<Event> _eventsRepository;
        readonly IRepository<EventSchedule> _schedulesRepository;
        readonly IRepository<Participant> _participantRepository;

        public EventsLogic(IRepository<Event> eventsRepository, IRepository<EventSchedule> schedulesRepository, IRepository<Participant> participantRepository)
        {
            _eventsRepository = eventsRepository;
            _schedulesRepository = schedulesRepository;
            _participantRepository = participantRepository;
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
                DeleteSchedules(eventId);
                DeleteParticipants(eventId);
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

        public bool CancelEvent(int eventId)
        {
            try
            {
                var canceled = GetEvent(eventId);
                canceled.Canceled = true;

                SaveEvent(canceled, true);

                //TODO: SEND EMAIL TO PARTICIPANTS

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private void DeleteSchedules(int eventId)
        {
            try
            {
                var toDelete = _schedulesRepository.List(s => s.EventId == eventId);
                _schedulesRepository.DeleteRange(toDelete);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void DeleteParticipants(int eventId)
        {
            try
            {
                var toDelete = _participantRepository.List(s => s.EventId == eventId);
                _participantRepository.DeleteRange(toDelete);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
