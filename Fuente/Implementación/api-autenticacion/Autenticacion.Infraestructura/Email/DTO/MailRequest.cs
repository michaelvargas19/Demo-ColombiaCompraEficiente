using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Infraestructura.Email.DTO
{
    public class MailRequest
    {
        public Dictionary<string,string> Destinatarios { get; set; }
        
        public string Asunto { get; set; }
        
        public string Body { get; set; }
        

    }
}
