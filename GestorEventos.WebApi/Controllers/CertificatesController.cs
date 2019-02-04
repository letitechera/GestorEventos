using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using GestorEventos.BLL.Interfaces;
using GestorEventos.WebApi.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificatesController : ControllerBase
    {
        private readonly IEventsLogic _eventsLogic;
        private readonly IConverter _converter;

        public CertificatesController(IEventsLogic eventsLogic, ISendGridLogic sendGridLogic, IConverter converter)
        {
            _eventsLogic = eventsLogic;
            _converter = converter;
        }

        [Route("{participantId}")]
        [HttpGet]
        public IActionResult CreateCertificate(int participantId)
        {
            var participant = _eventsLogic.GetParticipant(participantId);

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "Certificate",
                Out = string.Format(@"C:\Certificates\{0}_{1}.pdf", participant.Event.Name, participant.Attendant.FullName)
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = TemplateGenerator.GetHTMLString(participant),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            _converter.Convert(pdf);

            return Ok("Successfully created PDF document.");
        }
    }
}