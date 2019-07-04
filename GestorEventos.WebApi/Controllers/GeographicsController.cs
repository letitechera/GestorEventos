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
    [Route("api/countries")]
    public class GeographicsController : ControllerBase
    {
        private readonly IGeographicsLogic _geographicsLogic;

        public GeographicsController(IGeographicsLogic geographicsLogic)
        {
            _geographicsLogic = geographicsLogic;
        }

        [Route("all")]
        [HttpGet]
        public IEnumerable<Country> GetAllCountries()
        {
            var result =  _geographicsLogic.GetCountries();
            return result;
        }

        [HttpGet("{id}/cities")]
        public IEnumerable<City> GetCities(int id)
        {
            try
            {
                return _geographicsLogic.GetCities(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}