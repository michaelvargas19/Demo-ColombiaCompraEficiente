using Autenticacion.Infraestructura.Entities.Auth;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autenticacion.Infraestructura.Entities.OTP
{
    [Table("otpConfiguracion")]
    public class ConfiguracionOTP
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idConfiguracion { get; set; }

        [ForeignKey("Aplicacion")]
        public string idAplicacion { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public Aplicacion Aplicacion { get; set; }

        [Required]
        public bool habNumerico { get; set; }

        [Required]
        public bool habAlfaNumerico { get; set; }

        [Required]
        public int logitud { get; set; }

        [Required]
        public DateTime fechaCreacion { get; set; }

        [Required]
        public bool indHabilitado { get; set; }

        

    }
}
