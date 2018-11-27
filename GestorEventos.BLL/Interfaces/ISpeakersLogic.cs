using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GestorEventos.Models.Entities;

namespace GestorEventos.BLL.Interfaces
{
    public interface ISpeakersLogic
    {
        bool SaveSpeaker(Speaker speaker, bool update = false);

        bool DeleteSpeaker(int speakerId);

        IEnumerable<Speaker> GetSpeakers();

        Speaker GetSpeaker(int speakerId);

        bool SaveImage(int speakerId, FileInfo image);
    }
}
