using GestorEventos.BLL.Interfaces;
using GestorEventos.DAL.Repositories.Interfaces;
using GestorEventos.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorEventos.BLL
{
    public class GeographicsLogic: IGeographicsLogic
    {
        private readonly IRepository<Country> _countriesRepository;
        private readonly IRepository<City> _citiesRepository;

        public GeographicsLogic(IRepository<Country> countriesRepository, IRepository<City> citiesRepository)
        {
            _countriesRepository = countriesRepository;
            _citiesRepository = citiesRepository;
        }

        #region Countries

        public IEnumerable<Country> GetCountries()
        {
            try
            {
                return _countriesRepository.List();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Country GetCountry(int countryId)
        {
            try
            {
                return _countriesRepository.FindById(countryId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Cities

        public IEnumerable<City> GetCities(int countryId)
        {
            try
            {
                return _citiesRepository.List(c => c.CountryId == countryId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public City GetCity(int cityId)
        {
            try
            {
                return _citiesRepository.FindById(cityId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }
}
