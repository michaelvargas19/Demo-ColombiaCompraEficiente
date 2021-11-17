using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autenticacion.Infraestructura.Entities.OTP
{
    [Table("otpGenerada")]
    public class DatosOTP
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [MaxLength(6)]
        public string OTP { get; set; }

        public string canal { get; set; }

        public string llave { get; set; }

        public int TTL { get; set; }

        public DateTime fechaEnvio { get; set; }

        public DateTime fechaVencimiento { get; set; }

        public bool verificada { get; set; }

        public string destinatario { get; set; }


        public bool usuarioVerificado { get; set; }
        
        public bool indHabilitado { get; set; }

    }
}
