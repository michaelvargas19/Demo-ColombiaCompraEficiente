using Autenticacion.Dominio.IServices.Command;
using Autenticacion.Dominio.IServices.Queries;
using Autenticacion.Infraestructura;
using Autenticacion.Infraestructura.Email.IIntegracion;
using Autenticacion.Infraestructura.Entities.Auth;
using Autenticacion.Infraestructura.Entities.OTP;
using Autenticacion.Infraestructura.IRepositories.Command;
using Autenticacion.Infraestructura.IRepositories.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;

namespace Autenticacion.Dominio.IUnitOfWorks
{
    public interface IUnitOfWork : IDisposable 
    {

        // Persistencia
        void BeginTransaction();
        void SaveChanges();
        void Commit();
        void Rollback();


        public ContextoAuthDB ContextoAuthDB();

        IConfiguration ConfigurationAppSettings();
        RoleManager<Rol> RoleManagerIdentity();
        UserManager<Usuario> UserManagerIdentity();

        string GenerarCodigoTransaccion();

        IAplicacionesServiceQuery ServicioQueryAplicacion();
        IRolesServiceQuery RolesServiceQuery();
        
        
        
        ICuentaServiceCmd CuentaServiceCmd();

        ISesionesServiceCmd SesionesServiceCmd();

        ILogServiceCmd LogServiceCmd();



        //  Command
        IRepositoryBaseCommand<Usuario> RepositoryCommandUsuario();
        IRepositoryBaseCommand<Rol> RepositoryCommandRol();
        IRoleIdentityRepository RoleIdentityRepository();
        IRepositoryBaseCommand<Aplicacion> RepositoryCommandAplicacion();
        IRepositoryBaseCommand<AlgoritmoDeSeguridad> RepositoryCommandAlgoritmo();
        IRepositoryBaseCommand<Token> RepositoryCommandToken();
        IRepositoryBaseCommand<_LogAutenticacionAPI> RepositoryCommandLog();
        IRepositorySessionesCmd RepositorySessionesCmd();

        IRepositoryBaseCommand<DatosOTP> RepositoryCommandDatosOTP();
        IRepositoryBaseCommand<_LogOTPAPI> RepositoryCommandLogOTP();

        //  Queries
        IRepositoryBaseQuery<Usuario> RepositoryQueryUsuario();
        IRepositoryBaseQuery<Rol> RepositoryQueryRol();
        IRepositoryBaseQuery<TipoAutenticacion> RepositoryQueryTipoAuth();
        
        IRepositoryBaseQuery<TipoPlantillaEmail> RepositoryQueryTipoPlantillaEmail();
        
        IRepositoryBaseQuery<PlantillaEmail> RepositoryQueryPlantillaEmail();

        IRepositoryBaseQuery<Aplicacion> RepositoryQueryAplicacion();
        IRepositoryBaseQuery<AlgoritmoDeSeguridad> RepositoryQueryAlgoritmo();
        IRepositoryBaseQuery<Token> RepositoryQueryToken();
        IRepositoryBaseQuery<_LogAutenticacionAPI> RepositoryQueryLog();
        IRepositorySessionesQueries RepositorySessionesQueries();
        IUserIdentityRepository UserIdentityRepository();
        
        IRepositoryBaseQuery<TipoDocumento> RepositoryQueryTipoDocumento();


        IRepositoryBaseQuery<DatosOTP> RepositoryQueryDatosOTP();
        IRepositoryBaseQuery<_LogOTPAPI> RepositoryQueryLogOTP();



        //Integración
        IConsumidorEmail ConsumidorEmail();


        //Log
        void InsertarLog(_LogAutenticacionAPI log);
        
        void InsertarLogOTP(_LogOTPAPI log);
    }
}
