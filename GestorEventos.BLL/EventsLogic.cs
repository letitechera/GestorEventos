using System;
using System.Linq;
using System.Collections.Generic;
using GestorEventos.BLL.Interfaces;
using GestorEventos.DAL.Repositories.Interfaces;
using GestorEventos.Models.Entities;
using GestorEventos.Models.WebApiModels;
using Microsoft.Extensions.Configuration;

namespace GestorEventos.BLL
{
    public class EventsLogic : IEventsLogic
    {
        private readonly IRepository<Event> _eventsRepository;
        private readonly IRepository<EventTopic> _topicsRepository;
        private readonly IRepository<EventSchedule> _schedulesRepository;
        private readonly IRepository<Participant> _participantRepository;
        private readonly IRepository<Attendant> _attendantsRepository;
        private readonly IAccreditationLogic _accreditationLogic;
        private readonly ISendGridLogic _sendgridLogic;
        private readonly IConfiguration Configuration;

        public EventsLogic(IRepository<Event> eventsRepository, IRepository<EventSchedule> schedulesRepository,
            IRepository<Participant> participantRepository, IRepository<EventTopic> topicsRepository,
            IRepository<Attendant> attendantsRepository, IAccreditationLogic accreditationLogic,
            ISendGridLogic sendgridLogic, IConfiguration configuration)
        {
            _eventsRepository = eventsRepository;
            _schedulesRepository = schedulesRepository;
            _participantRepository = participantRepository;
            _topicsRepository = topicsRepository;
            _attendantsRepository = attendantsRepository;
            _accreditationLogic = accreditationLogic;
            _sendgridLogic = sendgridLogic;
            Configuration = configuration;
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
                    if (string.IsNullOrEmpty(_event.Image))
                        _event.Image = Configuration.GetValue<string>("StorageConfig:DefaultEventImage");
                    _eventsRepository.Add(_event);
                }
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

        public IEnumerable<EventUI> GetEvents()
        {
            try
            {
                return _eventsRepository.List().Select(e => new EventUI(e)).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<EventUI> GetEvents(string userId)
        {
            try
            {
                var ret = _eventsRepository.List(e => e.CreatedById == userId).Select(e => new EventUI(e)).ToList();
                return ret;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public EventUI GetEventUI(int eventId)
        {
            try
            {
                var eventData = _eventsRepository.FindById(eventId);
                return new EventUI(eventData);
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

        public byte[] RegisterToEvent(Participant participant)
        {
            try
            {
                // Check if an attendant with the same Email exists
                var existant = _attendantsRepository
                    .List()
                    .FirstOrDefault(x => x.Email == participant.Email);

                if (existant == null)
                {
                    var newAttendant = new Attendant(participant);
                    participant.AttendantId = _attendantsRepository.Add(newAttendant);
                }
                else
                {
                    participant.AttendantId = existant.Id;
                }

                var registered = _participantRepository
                    .List()
                    .FirstOrDefault(x => x.Email == participant.Email && x.EventId == participant.EventId);

                if (registered == null)
                {
                    // Save participant and get registration ID
                    _participantRepository.Add(participant);

                    var participantId = _participantRepository.List(p => p.EventId == participant.EventId && p.Email == participant.Email).FirstOrDefault().Id;

                    // Generate QR Code
                    var qrCode = _accreditationLogic.GenerateQRCode(participantId);

                    // Send Email with QR to Participant
                    _sendgridLogic.SendQRCodeEmail(participant, qrCode);

                    return qrCode;
                }

                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UnregisterParticipant(int eventId, int participantId)
        {
            try
            {
                var existant = _participantRepository
                    .List(p => p.EventId == eventId && p.Id == participantId)
                    .FirstOrDefault();

                if (existant != null)
                {
                    _participantRepository.Delete(participantId);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
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
                //_emailsLogic.SendCancelationEmails(eventId);

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void SendCampaign(int eventId)
        {
            _sendgridLogic.SendCampaignEmail(eventId);
        }

        public IEnumerable<Participant> GetParticipants(int eventId)
        {
            return _participantRepository.List(p => p.EventId == eventId);
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

        public IEnumerable<EventTopic> GetAllTopics()
        {
            return _topicsRepository.List();
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

        public Participant Accredit(int participantId)
        {
            var participant = GetParticipant(participantId);
            return participant;
        }

        public Participant GetParticipant(int participantId)
        {
            return _participantRepository.FindById(participantId);
        }

        #endregion

    }
}
