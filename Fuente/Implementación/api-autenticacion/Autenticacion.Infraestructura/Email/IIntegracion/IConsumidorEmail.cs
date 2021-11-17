using Autenticacion.Infraestructura.Email.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.Infraestructura.Email.IIntegracion
{
    public interface IConsumidorEmail
    {
        Task EnviarEmail(MailRequest mail);
    }
}
