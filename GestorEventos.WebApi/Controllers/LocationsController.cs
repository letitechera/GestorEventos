using System;
using System.Collections.Generic;
using GestorEventos.BLL.Interfaces;
using GestorEventos.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.WebApi.Controllers
{
    [Authorize]
    [Route("api/locations")]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationsLogic _locationsLogic;

        public LocationsController(ILocationsLogic locationsLogic)
        {
            _locationsLogic = locationsLogic;
        }

        [Route("all")]
        [HttpGet]
        public IEnumerable<Location> GetAllLocations()
        {
            return _locationsLogic.GetLocations();
        }

        [HttpGet("{id}")]
        public Location GetLocation(int id)
        {
            try
            {
                return _locationsLogic.GetLocation(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Route("CreateLocation")]
        [HttpPost]
        public IActionResult CreateLocation([FromBody]Location location)
        {
            if (_locationsLogic.SaveLocation(location))
            {
                return Ok(location);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }


        [Route("UpdateLocation")]
        [HttpPut]
        public IActionResult UpdateLocation([FromBody]Location location)
        {
            if (_locationsLogic.SaveLocation(location, true))
            {
                return Ok(location);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("DeleteLocation/{id}")]
        public IActionResult DeleteLocation(int locationId)
        {
            if (_locationsLogic.DeleteLocation(locationId))
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }



    }
}