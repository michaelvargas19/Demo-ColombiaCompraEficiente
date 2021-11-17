using Autenticacion.Dominio.IServices.Queries;
using Autenticacion.Dominio.IUnitOfWorks;
using Autenticacion.Dominio.Modelo.Command;
using Autenticacion.Dominio.Modelo.Queries;
using Autenticacion.Dominio.Services.Command;
using Autenticacion.Infraestructura.Entities.Auth;
using Autenticacion.Infraestructura.Specification;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autenticacion.Dominio.Services.Queries
{
    public class AplicacionesServiceQuery : IAplicacionesServiceQuery
    {
        private readonly IUnitOfWork _ufw;
        
        public AplicacionesServiceQuery(IConfiguration configuration,
                               IUnitOfWork ufw)
        {
            this._ufw = ufw;
            
        }


        public AplicacionQuery consultarAplicacion(string idAplicacion, bool estado, bool permiteJWT)
        {
            AplicacionQuery appQ = null;

            try
            {
                Aplicacion app = _ufw.RepositoryQueryAplicacion().Find(new AplicacionSpecification(idAplicacion, estado, permiteJWT)).FirstOrDefault();

                if (app == null)
                {
                    throw new Exception("La aplicación es inválida");
                }
                
                appQ = new AplicacionQuery();
                appQ.IdAplicacion = app.IdAplicacion;
                appQ.Nombre = app.Nombre;

                appQ.Roles = new List<RolQuery>();
                
                foreach (Rol r in app.Roles)
                {
                    RolQuery rolQ = new RolQuery();
                    rolQ.Id = r.Id;
                    rolQ.Nombre = r.Display;
                    rolQ.Descripcion = r.Descripcion;
                    appQ.Roles.Add(rolQ);
                }

            }
            catch(Exception e)
            {
                throw e;
            }

            return appQ;
        }



        public ConfiguracionNewUserQuery GetConfiguracionNuevoUsuario(TokenJWT token)
        {
            ConfiguracionNewUserQuery configuracion = new ConfiguracionNewUserQuery();

            try
            {
                AplicacionQuery app = _ufw.ServicioQueryAplicacion().consultarAplicacion(token.IdAplicacion, true, true);

                if ( (app == null) )
                {
                    throw new Exception("Ha habido un problema con la solicitud.");
                }

                configuracion.Completado = true;

                configuracion.TiposAutenticacion = UtilsAplicationServiceCmd.tiposdeAutenticacion(this._ufw.RepositoryQueryTipoAuth().Find(new TipoAuthSpecification()).ToList());
                configuracion.TiposDocumento = UtilsAplicationServiceCmd.tiposDeDocumentos(this._ufw.RepositoryQueryTipoDocumento().Find(new TipoDocumentoSpecification()).ToList());
                configuracion.Roles = app.Roles.ToList();
                


            }
            catch(Exception e)
            {
                throw e;
            }

            return configuracion;

        }

    }
}
