using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestorEventos.BLL.Interfaces;
using GestorEventos.DAL.Repositories.Interfaces;
using GestorEventos.Models.Entities;

namespace GestorEventos.BLL
{
    public class ActivitiesLogic : IActivitiesLogic
    {
        private readonly IRepository<Activity> _activitiesRepository;
        private readonly IRepository<Speaker> _speakersRepository;
        private readonly IRepository<ActivityType> _atypeRepository;

        public ActivitiesLogic(IRepository<Activity> activitiesRepository, IRepository<Speaker> speakersRepository,
            IRepository<ActivityType> atypeRepository)
        {
            _activitiesRepository = activitiesRepository;
            _speakersRepository = speakersRepository;
            _atypeRepository = atypeRepository;
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
                    if (activity.Speakers != null && activity.Speakers.Any())
                    {
                        _speakersRepository.AddRange(activity.Speakers);
                    }
                    _activitiesRepository.Add(activity);
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool SaveActivities(IEnumerable<Activity> activities)
        {
            try
            {
                foreach (var activity in activities)
                {
                    SaveActivity(activity);
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
                DeleteSpeakers(activityId);
                _activitiesRepository.Delete(activityId);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Activity> GetActivities(int scheduleId)
        {
            try
            {
                return _activitiesRepository.List(a => a.EventScheduleId == scheduleId);
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

        public IEnumerable<ActivityType> GetActivityTypes()
        {
            try
            {
                return _atypeRepository.List();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Private Methods

        private void DeleteSpeakers(int activityId)
        {
            try
            {
                var toDelete = _speakersRepository.List(a => a.ActivityId == activityId);
                _speakersRepository.DeleteRange(toDelete);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }
}
