using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using GestorEventos.Models.Entities;
using GestorEventos.Models.WebApiModels;

namespace GestorEventos.BLL.Interfaces
{
    public interface IEventsLogic
    {
        bool SaveEvent(Event _event, bool update = false);

        bool DeleteEvent(int eventId);

        IEnumerable<Event> GetEvents();

        IEnumerable<EventUI> GetEvents(string userId);

        EventUI GetEventUI(int eventId);

        Event GetEvent(int eventId);

        bool CancelEvent(int eventId);

        byte[] RegisterToEvent(int eventId, Attendant attendant);

        //bool SaveImage(int eventId, object image);

        bool CreateEventTopic(string topicName);

        bool DeleteEventTopic(int topicId);

        IEnumerable<EventTopic> GetAllTopics();

        Participant Accredit(int participantId);

        IEnumerable<Participant> GetParticipants(int eventId);

        Participant GetParticipant(int participantId);
    }
}