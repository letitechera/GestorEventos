using System;
using System.Collections.Generic;
using System.Text;
using GestorEventos.Models.Entities;

namespace GestorEventos.BLL.Interfaces
{
    public interface ISchedulesLogic
    {
        bool SaveSchedule(EventSchedule schedule, bool update = false);

        bool DeleteSchedule(int scheduleId);

        IEnumerable<EventSchedule> GetSchedules(int eventId);

        EventSchedule GetSchedule(int scheduleId);
    }
}
