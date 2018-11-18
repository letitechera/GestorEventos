using GestorEventos.BLL.Interfaces;
using GestorEventos.DAL.Repositories.Interfaces;
using GestorEventos.Models.Entities;
using System;
using System.Collections.Generic;

namespace GestorEventos.BLL
{
    public class LocationsLogic : ILocationsLogic
    {
        readonly IRepository<Location> _locationsRepository;

        public LocationsLogic(IRepository<Location> locationsRepository)
        {
            _locationsRepository = locationsRepository;
        }

        #region Locations

        public bool SaveLocation(Location _location, bool update = false)
        {
            try
            {
                if (update)
                {
                    _locationsRepository.Update(_location);
                }
                else
                {
                    _locationsRepository.Add(_location);
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool DeleteLocation(int locationId)
        {
            try
            {
                _locationsRepository.Delete(locationId);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Location> GetLocations()
        {
            try
            {
                return _locationsRepository.List();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Location GetLocation(int locationId)
        {
            try
            {
                return _locationsRepository.FindById(locationId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }
}