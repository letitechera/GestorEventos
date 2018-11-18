using System;
using System.Linq;
using System.Collections.Generic;
using GestorEventos.BLL.Interfaces;
using GestorEventos.DAL.Repositories.Interfaces;
using GestorEventos.Models.Entities;
using Microsoft.Extensions.Logging;

namespace GestorEventos.BLL
{
    public class EventsLogic : IEventsLogic
    {
        readonly IRepository<Event> _eventsRepository;
        readonly IRepository<EventTopic> _topicsRepository;
        readonly IRepository<EventSchedule> _schedulesRepository;
        readonly IRepository<Participant> _participantRepository;
        readonly IRepository<Attendant> _attendantsRepository;
        readonly IAccreditationLogic _accreditationLogic;
        readonly IMailingLogic _mailingLogic;

        public EventsLogic(IRepository<Event> eventsRepository, IRepository<EventSchedule> schedulesRepository, 
            IRepository<Participant> participantRepository, IRepository<EventTopic> topicsRepository, 
            IAccreditationLogic accreditationLogic, IMailingLogic mailingLogic)
        {
            _eventsRepository = eventsRepository;
            _schedulesRepository = schedulesRepository;
            _participantRepository = participantRepository;
            _topicsRepository = topicsRepository;
            _accreditationLogic = accreditationLogic;
            _mailingLogic = mailingLogic;
        }

        #region Events

        public bool SaveEvent(Event _event, bool update = false)
        {
            try
            {
                if (update)
                {
                    _eventsRepository.Update(_event);
                }
                else
                {
                    //TODO: change default image url
                    if (_event.Image == null) _event.Image = "{urlToDefault}";
                    _eventsRepository.Add(_event);
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //TODO: Load Event Image To Cloud
        public bool LoadImage()
        {
            return true;
        }

        public bool DeleteEvent(int eventId)
        {
            try
            {
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

        public bool RegisterToEvent(int eventId, Attendant attendant)
        {
            var participant = new Participant
            {
                EventId = eventId
            };

            try
            {
                //Check if an attendant with the same Email exists
                var existant = _attendantsRepository.List()
                    .Where(x => x.Email.ToLower() == attendant.Email.ToLower())
                    .FirstOrDefault();

                if (existant == null)
                {
                    participant.AttendantId = _attendantsRepository.Add(attendant);
                }
                else
                {
                    participant.AttendantId = existant.Id;
                }

                //Generate QR Code
                participant.QRCode = _accreditationLogic.GenerateQRCode();

                _participantRepository.Add(participant);

                //Send Email with QR to Participant
                _mailingLogic.SendQRCodeEmail(participant);

                return true;
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

                //TODO: 
                _mailingLogic.SendCancelationEmails(eventId);

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        #endregion

        #region Event Topics

        public bool CreateEventTopic(string topicName)
        {
            try
            {
               var existant = _topicsRepository.List(t => t.Name == topicName).FirstOrDefault();

                if (existant != null)
                {
                    //return new ApiResult(false, "Topic already exists.");
                }

                var topic = new EventTopic { Name = topicName };
                _topicsRepository.Add(topic);

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool DeleteEventTopic(int topicId)
        {
            try
            {
                _topicsRepository.Delete(topicId);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Private Methods

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

        #endregion

    }
}
