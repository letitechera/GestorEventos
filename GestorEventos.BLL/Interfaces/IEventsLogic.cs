using System.Collections.Generic;
using System.Drawing;
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

        Event GetEvent(int eventId);

        bool CancelEvent(int eventId);

        Bitmap RegisterToEvent(int eventId, Attendant attendant);

        bool CreateEventTopic(string topicName);

        bool DeleteEventTopic(int topicId);

        IEnumerable<EventTopic> GetAllTopics();

        bool Accredit(string qrCode);
    }
}