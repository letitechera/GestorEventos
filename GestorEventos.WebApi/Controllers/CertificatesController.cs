using System;
using System.IO;
using DinkToPdf;
using DinkToPdf.Contracts;
using GestorEventos.BLL.Interfaces;
using GestorEventos.WebApi.Utility;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificatesController : ControllerBase
    {
        private readonly IEventsLogic _eventsLogic;
        private readonly ISendGridLogic _sendGridLogic;
        private readonly IConverter _converter;

        public CertificatesController(IEventsLogic eventsLogic, ISendGridLogic sendGridLogic, IConverter converter)
        {
            _eventsLogic = eventsLogic;
            _sendGridLogic = sendGridLogic;
            _converter = converter;
        }

        [Route("{eventId}")]
        [HttpGet]
        public IActionResult SendCertificates(int eventId)
        {
            try{
                var assistants = _eventsLogic.GetAssistants(eventId);

                // Create Certificate
                foreach (var participant in assistants)
                {
                    var folder = Directory.GetCurrentDirectory() + "/Certificates";
                    var path = string.Format("{0}\\{1}_{2}.pdf", folder, participant.Event.Name, participant.Attendant.FullName);
                    var globalSettings = new GlobalSettings
                    {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Landscape,
                        PaperSize = PaperKind.A4,
                        Margins = new MarginSettings { Top = 10 },
                        DocumentTitle = "Certificate",
                        Out = path
                    };

                    var objectSettings = new ObjectSettings
                    {
                        PagesCount = true,
                        HtmlContent = TemplateGenerator.GetHTMLString(participant),
                        WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                    };

                    var pdf = new HtmlToPdfDocument()
                    {
                        GlobalSettings = globalSettings,
                        Objects = { objectSettings }
                    };

                    _converter.Convert(pdf);

                    // Send Certificate
                    var doc = System.IO.File.ReadAllBytes(path);
                    _sendGridLogic.SendCertificateEmail(participant.Attendant.Email, doc);
                }

                return Ok("Successfully sent certificate.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
            
        }
    }
}