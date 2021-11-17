using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Infraestructura.Email.Settings
{
    public class SMTP_Settings
    {
        public string Server {get;set;}

        public int Port { get; set; }

        public string SenderEmail { get; set; }
        public string SenderName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

    }
}
