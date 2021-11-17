using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autenticacion.Infraestructura.Entities.OTP
{
    [Table("otpLog")]
    public class _LogOTPAPI {
        private bool v;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idLog { get; set; }

        public string aplicacion { get; set; }

        public string OTP { get; set; }

        public string llave { get; set; }

        public string tipo { get; set; }
        
        public string request { get; set; }

        public string response { get; set; }

        public string metodo { get; set; }

        public bool verificado { get; set; }
        
        public bool esExcepcion{ get; set; }
        
        public string host { get; set; }

        public DateTime fechaRegistro { get; set; }

        public bool indHabilitado { get; set; }


        public _LogOTPAPI(string aplicacion, string tipo, string OTP, string metodo, string llave, string request, string response, bool verificado, bool esExcepcion, string host, bool indHabilitado)
        {
            this.aplicacion = aplicacion;
            this.OTP = OTP;
            this.tipo = tipo;
            this.llave = llave;
            this.request = request;
            this.response = response;
            this.metodo = metodo;
            this.verificado = verificado;
            this.esExcepcion = esExcepcion;
            this.host = host;
            this.fechaRegistro = DateTime.Now;
            this.indHabilitado = indHabilitado;
        }

        public _LogOTPAPI(string aplicacion, string tipo, string oTP, string metodo, string llave, string request, string response, bool verificado, string host, bool v)
        {
            this.aplicacion = aplicacion;
            this.tipo = tipo;
            OTP = oTP;
            this.metodo = metodo;
            this.llave = llave;
            this.request = request;
            this.response = response;
            this.verificado = verificado;
            this.host = host;
            this.v = v;
        }
    }

    

}
