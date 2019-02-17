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

<div style='width: 800px; height: 800px; padding: 20px; text - align:center; border: 10px solid #787878'>
<div style= 'width:750px; height:750px; padding:20px; text-align:center; border: 5px solid #787878'>
            <span style= 'font-size:50px; font-weight:bold' > Certificate of Participation</span>
                <br><br>  
                <span style = 'font-size:25px'><i> This is to certify that </i></span>
                       <br><br>
                       <span style = 'font-size:30px'><b>{0}</b></span><br/><br/>                
                             <span style = 'font-size:25px'><i> has participanted on the event</i></span><br/><br/>
                                    <span style= 'font-size:30px'>{1}</span><br/><br/>
                <img src='{2}' style='width:300px'></br></br>                              
                <span style= 'font-size:25px'>{3}</span></br>
                <span style= 'font-size:25px'><i> organized by</i></span></br>
                <span style= 'font-size:30px'><b>{4}</b></span>
    </div>
    </div>
    ", participant.Attendant.FullName, participant.Event.Name, participant.Event.Image, participant.Event.StartDate.ToShortDateString(), participant.Event.CreatedByName);
            return sb.ToString();
        }
    }
}