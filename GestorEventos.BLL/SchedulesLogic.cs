﻿using System;
using System.Collections.Generic;
using System.Linq;
using GestorEventos.BLL.Interfaces;
using GestorEventos.DAL.Repositories.Interfaces;
using GestorEventos.Models.Entities;
using GestorEventos.Models.WebApiModels;
using Microsoft.EntityFrameworkCore.Internal;

namespace GestorEventos.BLL
{
    public class SchedulesLogic : ISchedulesLogic
    {
        private readonly IRepository<EventSchedule> _schedulesRepository;
        private readonly IRepository<Activity> _activitiesRepository;
        private readonly IRepository<Speaker> _speakersRepository;

        public SchedulesLogic(IRepository<EventSchedule> schedulesRepository, IRepository<Activity> activitiesRepository,
            IRepository<Speaker> speakersRepository)
        {
            _schedulesRepository = schedulesRepository;
            _activitiesRepository = activitiesRepository;
            _speakersRepository = speakersRepository;
        }

        #region Schedules

        public bool SaveSchedule(EventSchedule schedule, bool update = false)
        {
            try
            {
                if (update)
                {
                    _schedulesRepository.Update(schedule);
                }
                else
                {
                    if (schedule.Activities != null && schedule.Activities.Any())
                    {
                        _activitiesRepository.AddRange(schedule.Activities);
                    }
                    _schedulesRepository.Add(schedule);
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool DeleteSchedule(int scheduleId)
        {
            try
            {
                DeleteActivities(scheduleId);
                _schedulesRepository.Delete(scheduleId);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<ScheduleUI> GetSchedules(int eventId)
        {
            try
            {
                var schedules =  _schedulesRepository.List(s => s.EventId == eventId).Select(e => new ScheduleUI(e)).ToList();
                foreach (var s in schedules)
                {
                    var activities = _activitiesRepository.List(a => a.EventScheduleId == s.Id).Select(a => new ActivityUI(a)).ToList();
                    s.Activities = activities;
                }
                return schedules;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public EventSchedule GetSchedule(int scheduleId)
        {
            try
            {
                return _schedulesRepository.FindById(scheduleId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Private Methods

        private void DeleteActivities(int scheduleId)
        {
            try
            {
                var toDelete = _activitiesRepository.List(a => a.EventScheduleId == scheduleId);
                foreach (var a in toDelete)
                {
                    var speakers = _speakersRepository.List(s => s.ActivityId == a.Id);
                    _speakersRepository.DeleteRange(speakers);

                }
                _activitiesRepository.DeleteRange(toDelete);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }
}
