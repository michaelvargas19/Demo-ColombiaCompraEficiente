using Autenticacion.Dominio.IServices.Queries;
using Autenticacion.Dominio.IUnitOfWorks;
using Autenticacion.Dominio.Modelo.Queries;
using Autenticacion.Infraestructura.Entities.Auth;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Autenticacion.Dominio.Services.Queries
{
    public class UsuariosServiceQuery : IUsuariosServiceQuery
    {
        private readonly IUnitOfWork _ufw;
        
        public UsuariosServiceQuery(IConfiguration configuration,
                               IUnitOfWork ufw)
        {
            this._ufw = ufw;
            
        }

        public UsuarioQuery consultarUsuario(string usuario)
        {
            UsuarioQuery usrQ = null;

            try
            {

                Usuario usrE = _ufw.UserIdentityRepository().ReadByNameUser(usuario);
                usrQ = new UsuarioQuery();
                usrQ.IdUsuario = usrE.Id;
                usrQ.Usuario = usrE.UserName;
                usrQ.Nombres = usrE.PrimerNombre + " " + usrE.SegundoNombre;
                usrQ.Apellidos = usrE.PrimerApellido + " " + usrE.SegundoApellido;
                usrQ.Identificacion = usrE.Identificacion;
                usrQ.TelefonoMovil = usrE.PhoneNumber;
                usrQ.Email = usrE.Email;
                usrQ.Identificacion = usrE.Identificacion;
                usrQ.Organizacion = usrE.Organizacion;
                usrQ.Cargo = usrE.Cargo;
                usrQ.Description = usrE.Descripcion;
                usrQ.EsExterno = usrE.EsExterno;

                

            }
            catch (Exception e)
            {
                throw e;
            }
            return usrQ;
        }

        public UsuarioQuery consultarUsuario(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UsuarioQuery> getUsuarios(string usuario)
        {
            throw new NotImplementedException();
        }



    }
}
