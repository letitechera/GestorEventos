using GestorEventos.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorEventos.BLL.Interfaces
{
    public interface IGeographicsLogic
    {
        IEnumerable<Country> GetCountries();

        Country GetCountry(int countryId);

        IEnumerable<City> GetCities(int countryId);

        City GetCity(int cityId);
    }
}
