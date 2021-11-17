using Autenticacion.Dominio.Modelo.Command;
using Autenticacion.Infraestructura.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Autenticacion.Dominio.Services.Command
{
    public class UtilsTokenServiceCmd
    {

        #region Configuración

        public static CodigoCmd obtenerConfRecuperar(Token token, CodigoCmd recuperar)
        {
            recuperar.FechaGeneracion = token.FechaCreacion;
            recuperar.FechaExpiracion = token.FechaExpiracion;
            recuperar.LongitudCodigo = token.LongitudCodigo;
            recuperar.MinutosVida = token.MinutosDeVida;

            return recuperar;
        }

        #endregion




    }
}
