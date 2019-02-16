using GestorEventos.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GestorEventos.BLL.Interfaces
{
    public interface IAttendantsLogic
    {
        bool SaveAttendant(Attendant attendant, bool update = false);

        bool DeleteAttendant(int attendantId);

        IEnumerable<Attendant> GetAttendants();

        Attendant GetAttendant(int attendantId);

        Attendant ExistsAttendant(string email);
    }
}
