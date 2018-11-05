using System;
using System.Collections.Generic;
using System.Text;
using GestorEventos.Models.Entities;

namespace GestorEventos.BLL.Interfaces
{
    public interface IEventsLogic
    {
        bool SaveEvent(Event _event, bool update = false);
        bool DeleteEvent(int eventId);
        IEnumerable<Event> GetEvents();
        Event GetEvent(int eventId);
    }
}
