using System;
using System.Collections.Generic;
using System.Text;
using GestorEventos.DAL.Repositories.Interfaces;
using GestorEventos.Models.Entities;

namespace GestorEventos.BLL
{
    public class ActivitiesLogic
    {
        private readonly IRepository<Activity> _activitiesRepository;

        public ActivitiesLogic(IRepository<Activity> activitiesRepository)
        {
            _activitiesRepository = activitiesRepository;
        }

        #region Activities

        public bool SaveActivity(Activity activity, bool update = false)
        {
            try
            {
                if (update)
                {
                    _activitiesRepository.Update(activity);
                }
                else
                {
                    _activitiesRepository.Add(activity);
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool DeleteActivity(int activityId)
        {
            try
            {
                _activitiesRepository.Delete(activityId);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Activity> GetActivities()
        {
            try
            {
                return _activitiesRepository.List();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Activity GetActivity(int activityId)
        {
            try
            {
                return _activitiesRepository.FindById(activityId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }
}
