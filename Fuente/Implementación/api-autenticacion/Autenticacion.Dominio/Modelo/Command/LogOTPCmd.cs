using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Dominio.Modelo.Command
{
    public class LogOTPCmd
    {

        public string Aplicacion { get; set; }

        public string Tipo { get; set; }
        
        public string OTP { get; set; }
        public string Llave { get; set; }
        public bool Verificado { get; set; }

        public string Request { get; set; }

        public string Response { get; set; }
      
        public string Metodo { get; set; }

        public bool EsExcepcion { get; set; }

        public string Host { get; set; }

    }
}
