using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GestorEventos.BLL.Interfaces
{
    public interface IFilesLogic
    {
        Task<string> LoadEventImage(int eventId, IFormFile file);

        Task<string> LoadSpeakerImage(IFormFile file);

        bool ImportAttendantsFromXml(IFormFile file);
    }
}
