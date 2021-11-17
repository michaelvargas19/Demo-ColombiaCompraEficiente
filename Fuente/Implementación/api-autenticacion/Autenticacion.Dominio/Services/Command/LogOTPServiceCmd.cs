using Autenticacion.Dominio.IServices.Command;
using Autenticacion.Dominio.IUnitOfWorks;
using Autenticacion.Dominio.Modelo.Command;
using Autenticacion.Dominio.Modelo.Queries;
using Autenticacion.Dominio.Specification;
using Autenticacion.Infraestructura.Entities;
using Autenticacion.Infraestructura.Entities.OTP;
using Autenticacion.Infraestructura.IRepositories.Command;
using Autenticacion.Infraestructura.Specification;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace Autenticacion.Dominio.Services.Command
{
    public class LogOTPServiceCmd : ILogOTPServiceCmd
    {
        private readonly string IdApp;
        private readonly string Issuer;
        private readonly IUnitOfWork _ufw;
        
        public LogOTPServiceCmd(IConfiguration configuration,
                               IUnitOfWork ufwLog)
        {
            
            this.IdApp = configuration["IdentifierAPP:Id"];
            this.Issuer = configuration["JwtConfig:issuer"];
            this._ufw = ufwLog;
            
        }

        public void AgregarLog(LogOTPCmd log)
        {

            try
            {
                _LogOTPAPI logE = new _LogOTPAPI(log.Aplicacion, log.Tipo, log.OTP, log.Metodo, log.Llave, log.Request, log.Response, log.Verificado, log.Host, true);
                _ufw.InsertarLogOTP(logE);
            }
            catch (Exception e)
            {
                _ufw.Rollback();
                throw e;
            }

        }
                    
    }
}
