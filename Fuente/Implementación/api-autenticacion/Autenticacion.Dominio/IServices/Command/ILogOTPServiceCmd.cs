using Autenticacion.Dominio.Modelo.Command;
using Autenticacion.Infraestructura.Entities.OTP;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Dominio.IServices.Command
{
    public interface ILogOTPServiceCmd
    {
        void AgregarLog(LogOTPCmd log);



    }
}
