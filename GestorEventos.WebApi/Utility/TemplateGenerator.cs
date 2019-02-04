using GestorEventos.BLL.Interfaces;
using GestorEventos.Models.Entities;
using System;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace GestorEventos.WebApi.Utility
{
    public class TemplateGenerator
    {
        private readonly IEventsLogic _eventsLogic;

        public TemplateGenerator(IEventsLogic eventsLogic)
        {
            _eventsLogic = eventsLogic;
        }

        public static string GetHTMLString(Participant participant)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>{0}</h1></div>
            ", participant.Event.Name);

            sb.AppendFormat(@"
                            <div class='header'><h2>Certificamos que</h2></div>
                            <div class='header'><h1>{0}</h1></div>
                            <div class='header'><h2>Ha participado del evento</h2></div>

", participant.Attendant.FullName);

            sb.Append(@"
                            </body>
                        </html>");

            return sb.ToString();
        }
    }
}