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
    [Route("api/lookup")]
    public class LookUpDataController : ControllerBase
    {
        private readonly IGeographicsLogic _geographicsLogic;

        public LookUpDataController(IGeographicsLogic geographicsLogic)
        {
            _geographicsLogic = geographicsLogic;
        }

        [Route("all")]
        [HttpGet]
        public IEnumerable<Country> GetCountries()
        {
            try
            {
                var ret = _geographicsLogic.GetCountries();
                return ret;
            }
            catch(Exception e){
                var ex = e;
                throw ex;
            }
        }

        [Route("{countryId}/cities")]
        [HttpGet]
        public IEnumerable<City> GetCities(int countryId)
        {
            return _geographicsLogic.GetCities(countryId);
        }

        [HttpGet("countries/{id}")]
        public Country GetCountry(int id)
        {
            return _geographicsLogic.GetCountry(id);
        }

        [HttpGet("cities/{id}")]
        public City GetCity(int id)
        {
            return _geographicsLogic.GetCity(id);
        }
    }
}
