using System;
using System.Collections.Generic;
using System.Text;
using GestorEventos.Models.Entities;

namespace GestorEventos.BLL.Interfaces
{
    public interface IActivitiesLogic
    {
        bool SaveActivity(Activity activity, bool update = false);

        bool DeleteActivity(int activityId);

        IEnumerable<Activity> GetActivities();

        Activity GetActivity(int activityId);
    }
}
