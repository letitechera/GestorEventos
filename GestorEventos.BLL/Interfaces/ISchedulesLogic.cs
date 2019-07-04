using System;
using System.Collections.Generic;
using System.Text;
using GestorEventos.Models.Entities;
using GestorEventos.Models.WebApiModels;

namespace GestorEventos.BLL.Interfaces
{
    public interface ISchedulesLogic
    {
        bool SaveSchedule(EventSchedule schedule, bool update = false);

        bool DeleteSchedule(int scheduleId);

        IEnumerable<ScheduleUI> GetSchedules(int eventId);

        EventSchedule GetSchedule(int scheduleId);
    }
}
