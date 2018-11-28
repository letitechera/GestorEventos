using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorEventos.WebApi.Models
{
    public class SendGridOptions
    {
        public string APIKeyName { get; set; }
        public string APIKeyValue { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
    }
}
