using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestorEventos.BLL.Interfaces;
using GestorEventos.Models.Entities;
using GestorEventos.Models.WebApiModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestorEventos.WebApi.Controllers
{
    [Authorize]
    [Route("api/schedules")]
    public class ScheduleController : Controller
    {
        private readonly ISchedulesLogic _schedulesLogic;
        private readonly IActivitiesLogic _activitiesLogic;

        public ScheduleController(ISchedulesLogic schedulesLogic, IActivitiesLogic activitiesLogic)
        {
            _schedulesLogic = schedulesLogic;
            _activitiesLogic = activitiesLogic;
        }

        #region Schedules

        [Route("event/{eventId}/all")]
        [HttpGet]
        public IEnumerable<ScheduleUI> GetSchedulesByEvent(int eventId)
        {
            try
            {
                return _schedulesLogic.GetSchedules(eventId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public EventSchedule GetSchedule(int id)
        {
            try
            {
                return _schedulesLogic.GetSchedule(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Route("CreateSchedule")]
        [HttpPost]
        public IActionResult CreateSchedule([FromBody]EventSchedule schedule)
        {
            if (_schedulesLogic.SaveSchedule(schedule))
            {
                return Ok(schedule);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }


        [Route("UpdateSchedule")]
        [HttpPut]
        public IActionResult UpdateSchedule([FromBody]EventSchedule schedule)
        {
            if (_schedulesLogic.SaveSchedule(schedule, true))
            {
                return Ok(schedule);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("DeleteSchedule/{scheduleId}")]
        public IActionResult DeleteSchedule(int scheduleId)
        {
            if (_schedulesLogic.DeleteSchedule(scheduleId))
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        #endregion

        #region Activities

        [Route("{scheduleId}/activities")]
        [HttpGet]
        public IEnumerable<Activity> GetActivitiesBySchedule(int scheduleId)
        {
            return _activitiesLogic.GetActivities(scheduleId);
        }

        [HttpGet("activities/{id}")]
        public Activity GetActivity(int id)
        {
            try
            {
                return _activitiesLogic.GetActivity(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Route("CreateActivity")]
        [HttpPost]
        public IActionResult CreateActivity([FromBody]Activity activity)
        {
            if (_activitiesLogic.SaveActivity(activity))
            {
                return Ok(activity);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }


        [Route("UpdateActivity")]
        [HttpPut]
        public IActionResult UpdateActivity([FromBody]Activity activity)
        {
            if (_activitiesLogic.SaveActivity(activity, true))
            {
                return Ok(activity);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("DeleteActivity/{activityId}")]
        public IActionResult DeleteActivity(int activityId)
        {
            if (_activitiesLogic.DeleteActivity(activityId))
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [Route("activitytypes")]
        [HttpGet]
        public IEnumerable<ActivityType> GetActivityTypes()
        {
            return _activitiesLogic.GetActivityTypes();
        }

        #endregion
    }
}
