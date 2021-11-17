using Autenticacion.Dominio.Modelo.Command;
using Autenticacion.Infraestructura.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Autenticacion.Dominio.Services.Command
{
    public class UtilsEmailServiceCmd
    {

        #region Emails

        public static string generarEmailSolicitudConfirmacion(string plantilla, Aplicacion aplicacion, Usuario usuario)
        {
            plantilla.Replace("$Usuario",usuario.PrimerNombre + " " + usuario.SegundoNombre + " " + usuario.PrimerApellido + " " + usuario.SegundoApellido);
            plantilla.Replace("$Enlace", "sad");


            return plantilla;
        }

        #endregion




    }
}
