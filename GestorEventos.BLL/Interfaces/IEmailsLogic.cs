using GestorEventos.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorEventos.BLL.Interfaces
{
    public interface IEmailsLogic
    {
        bool SendQRCodeEmail(Participant participant);
        bool SendCancelationEmails(int eventId);
    }
}
