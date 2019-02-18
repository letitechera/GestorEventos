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
            <style>
                @import url('https://fonts.googleapis.com/css?family=Dancing+Script:400,700');
                @import url('https://fonts.googleapis.com/css?family=Raleway');
            </style>
            <div class='c-container'>
              <div class='c-body'>
                <div class='c-title'> CERTIFICATE <span class='c-of'>OF</span> PARTICIPATION</div>
                <div class='c-certify'>THIS IS TO CERTIFY THAT</div>
                <div class='c-name'><span>{0}</span></div>
                <div class='c-reason'>HAS PARTICIPATED IN</div>
                <div class='c-event'>{1}</div>
                <div class='c-footer'>
                  <div class='c-info'>
                    <div class='c-oby'>ORGANIZED BY</div>
                    <div class='c-organizer'>{4}</div>
                    <div class='c-date'>{3}</div>
                  </div>
                  <div class='c-img'>
                    <img src='{2}' style='width:150px; height:120px'>
                  </div>
                </div>
              </div>
            </div>
                ", participant.Attendant.FullName, participant.Event.Name, participant.Event.Image, participant.Event.StartDate.ToShortDateString(), participant.Event.CreatedByName);
            return sb.ToString();
        }
    }
}