using GestorEventos.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorEventos.BLL.Interfaces
{
    public interface ILocationsLogic
    {
        bool SaveLocation(Location _location, bool update = false);

        bool DeleteLocation(int locationId);

        IEnumerable<Location> GetLocations();

        Location GetLocation(int locationId);
    }
}
