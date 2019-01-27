using System;
using System.Linq;
using System.Collections.Generic;
using GestorEventos.BLL.Interfaces;
using GestorEventos.DAL.Repositories.Interfaces;
using GestorEventos.Models.Entities;
using GestorEventos.Models.WebApiModels;
using System.Drawing;
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

        //TODO: Load Event Image To Cloud
        //public bool SaveImage(int eventId, object file)
        //{
        //    try
        //    {
        //        var speakersBlob = "";
        //        var imageUrl = _imagesLogic.LoadImage(file, speakersBlob);

        //        var _event = _eventsRepository.FindById(eventId);
        //        _event.Image = imageUrl;
        //        _eventsRepository.Update(_event);

        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}

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

        public IEnumerable<EventUI> GetEvents(string userId)
        {
            try
            {
                var ret = _eventsRepository.List(e => e.CreatedById == userId).ToList().Select(e => new EventUI(e, e.Location.Name, e.EventTopic.Name));
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
                return new EventUI(eventData, eventData.Location.Name, eventData.EventTopic.Name);
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

        public Bitmap RegisterToEvent(int eventId, Attendant attendant)
        {
            var participant = new Participant
            {
                EventId = eventId
            };

            try
            {
                // Check if an attendant with the same Email exists
                var existant = _attendantsRepository
                    .List()
                    .FirstOrDefault(x => x.Email.ToLower() == attendant.Email.ToLower());

                if (existant == null)
                {
                    participant.AttendantId = _attendantsRepository.Add(attendant);
                }
                else
                {
                    participant.AttendantId = existant.Id;
                }

                var registered = _participantRepository
                    .List()
                    .FirstOrDefault(x => x.AttendantId == participant.AttendantId && x.EventId == participant.EventId);

                if (registered == null)
                {
                    // Save participant and get registration ID
                    _participantRepository.Add(participant);

                    var registrationID = _participantRepository.List(p => p.EventId == eventId && p.AttendantId == participant.AttendantId).FirstOrDefault().Id;

                    // Generate QR Code
                    var qrCode = _accreditationLogic.GenerateQRCode(registrationID);

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

        public bool Accredit(string qrCode)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
