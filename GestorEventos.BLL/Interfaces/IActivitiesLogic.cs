using System;
using System.Collections.Generic;
using System.Text;
using GestorEventos.Models.Entities;

namespace GestorEventos.BLL.Interfaces
{
    public interface IActivitiesLogic
    {
        bool SaveActivity(Activity activity, bool update = false);

        bool SaveActivities(IEnumerable<Activity> activities);

        bool DeleteActivity(int activityId);

        IEnumerable<Activity> GetActivities(int scheduleId);

        Activity GetActivity(int activityId);
    }
}
