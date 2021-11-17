using Autenticacion.Dominio.IServices.Command;
using Autenticacion.Dominio.IServices.Queries;
using Autenticacion.Dominio.IUnitOfWorks;
using Autenticacion.Dominio.Services.Command;
using Autenticacion.Dominio.Services.Queries;
using Autenticacion.Infraestructura;
using Autenticacion.Infraestructura.Email.IIntegracion;
using Autenticacion.Infraestructura.Email.Integracion;
using Autenticacion.Infraestructura.Email.Settings;
using Autenticacion.Infraestructura.Entities.Auth;
using Autenticacion.Infraestructura.Entities.OTP;
using Autenticacion.Infraestructura.IRepositories.Command;
using Autenticacion.Infraestructura.IRepositories.Queries;
using Autenticacion.Infraestructura.Repositories.Command;
using Autenticacion.Infraestructura.Repositories.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace Autenticacion.Dominio.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private IAplicacionesServiceQuery _servQueryAplicacion;
        private IRolesServiceQuery _servQueryRoles;
        
        private ICuentaServiceCmd _servCmdCuenta;
        private ISesionesServiceCmd _servCmdSesiones;
        
        private ILogServiceCmd _servLogAuth;


        private readonly ContextoAuthDB _contexto;
        private IRepositoryBaseCommand<Usuario> _repoCommandUsuario;
        private IRepositoryBaseCommand<Rol> _repoCommandRol;
        private IRoleIdentityRepository _repoRolIdentity;
        private IUserIdentityRepository _repoUsuarioIdentity;
        private IRepositoryBaseCommand<Aplicacion> _repoCommandAplicacion;
        private IRepositoryBaseCommand<AlgoritmoDeSeguridad> _repoCommandAlgoritmo;
        private IRepositoryBaseCommand<Token> _repoCommandToken;
        private IRepositoryBaseCommand<_LogAutenticacionAPI> _repoCommandLog;
        private IRepositoryBaseCommand<_LogOTPAPI> _repoCommandLogOTP;
        private IRepositoryBaseCommand<DatosOTP> _repoCommandDatosOTP;
        private readonly IRepositorySessionesCmd _repoSessionCmd;


        private readonly IConfiguration configuration;
        private readonly RoleManager<Rol> roleManager;
        private readonly UserManager<Usuario> userManager;


        private IRepositoryBaseQuery<Rol> _repoQueryRol;
        private IRepositoryBaseQuery<Usuario> _repoQueryUsuario;
        private IRepositoryBaseQuery<TipoAutenticacion> _repoQueryTipoAuth;
        private IRepositoryBaseQuery<TipoDocumento> _repoQueryTipoDoc;
        private IRepositoryBaseQuery<TipoPlantillaEmail> _repoQueryTipoPlanilla;
        private IRepositoryBaseQuery<PlantillaEmail> _repoQueryPlanilla;
        private IRepositoryBaseQuery<Aplicacion> _repoQueryAplicacion;
        private IRepositoryBaseQuery<AlgoritmoDeSeguridad> _repoQueryAlgoritmo;
        private IRepositoryBaseQuery<Token> _repoQueryToken;
        private IRepositoryBaseQuery<_LogAutenticacionAPI> _repoQueryLog;
        private IRepositoryBaseQuery<_LogOTPAPI> _repoQueryLogOTP;
        private IRepositoryBaseQuery<DatosOTP> _repoQueryDatosOTP;
        private IRepositorySessionesQueries _repoSessionQueries;

        private  readonly IOptions<SMTP_Settings> _smtpSettingsOptions;
        private  readonly SMTP_Settings _smtpSettings;
        private  IConsumidorEmail _consumidorEmail;

        


        public UnitOfWork(ContextoAuthDB contexto,
                          IRepositorySessionesCmd repoSession,
                          IRepositorySessionesQueries repoSessionQueries,
                          IConfiguration configuration,
                          RoleManager<Rol> managerR,
                          UserManager<Usuario> managerU,
                          IOptions<SMTP_Settings> smtpSettings)
        {
            this._contexto = contexto;
            this._repoSessionCmd = repoSession;
            this._repoSessionQueries = repoSessionQueries;

            this.configuration = configuration;
            this.roleManager = managerR;
            this.userManager = managerU;
            
            this._smtpSettingsOptions = smtpSettings;
            this._smtpSettings = smtpSettings.Value;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void BeginTransaction()
        {
            if (this._contexto.Database.CurrentTransaction == null)
                this._contexto.Database.BeginTransaction();
        }
        public void SaveChanges()
        {
            this._contexto.SaveChanges();
        }
        public void Commit()
        {
            if (this._contexto.Database.CurrentTransaction != null)
                this._contexto.Database.CommitTransaction();
        }
        public void Rollback()
        {
            if(this._contexto.Database.CurrentTransaction != null)
                this._contexto.Database.RollbackTransaction();
        }

        public ContextoAuthDB ContextoAuthDB()
        {
            return this._contexto;
        }

        public IConfiguration ConfigurationAppSettings()
        {
            return this.configuration;
        }
        public RoleManager<Rol> RoleManagerIdentity()
        {
            return this.roleManager;
        }
        public UserManager<Usuario> UserManagerIdentity()
        {
            return this.userManager;
        }

        public string GenerarCodigoTransaccion() {
            return Guid.NewGuid().ToString();
        }
        




        public IAplicacionesServiceQuery ServicioQueryAplicacion()
        {
            if (this._servQueryAplicacion == null)
            {
                this._servQueryAplicacion = new AplicacionesServiceQuery(this.configuration, this);
            }
            return this._servQueryAplicacion;
        }
        
        
        public IRolesServiceQuery RolesServiceQuery()
        {
            if (this._servQueryRoles == null)
            {
                this._servQueryRoles = new RolesServiceQuery(this.configuration, this);
            }
            return this._servQueryRoles;
        }


        public ICuentaServiceCmd CuentaServiceCmd()
        {
            if (this._servCmdCuenta == null)
            {
                this._servCmdCuenta = new CuentaServiceCmd(this.configuration, this);
            }
            return this._servCmdCuenta;
        }
        
        public ISesionesServiceCmd SesionesServiceCmd()
        {
            if (this._servCmdSesiones == null)
            {
                this._servCmdSesiones = new SesionesServiceCmd(this.configuration, this, this.RolesServiceQuery() );
            }
            return this._servCmdSesiones;
        }



        #region Command

        public IRepositoryBaseCommand<Usuario> RepositoryCommandUsuario()
        {
            if (this._repoCommandUsuario == null)
            {
                this._repoCommandUsuario = new RepositoryBaseCommand<Usuario>(this._contexto);
            }
            return this._repoCommandUsuario;
        }

        public IRepositoryBaseCommand<Rol> RepositoryCommandRol()
        {

            if (this._repoCommandRol == null)
            {
                this._repoCommandRol = new RepositoryBaseCommand<Rol>(this._contexto);
            }
            return this._repoCommandRol;
        }



        public IUserIdentityRepository UserIdentityRepository()
        {

            if (this._repoUsuarioIdentity == null)
            {
                this._repoUsuarioIdentity = new UserIdentityRepository(this.configuration, this.userManager, this.roleManager, this._contexto);
            }
            return this._repoUsuarioIdentity;
        }
        public IRoleIdentityRepository RoleIdentityRepository()
        {

            if (this._repoRolIdentity == null)
            {
                this._repoRolIdentity = new RoleIdentityRepository(this.configuration, this.roleManager, this.userManager, this._contexto);
            }
            return this._repoRolIdentity;
        }

        public IRepositoryBaseCommand<Aplicacion> RepositoryCommandAplicacion()
        {
            if (this._repoCommandAplicacion == null)
            {
                this._repoCommandAplicacion = new RepositoryBaseCommand<Aplicacion>(this._contexto);
            }
            return this._repoCommandAplicacion;
        }

        public IRepositoryBaseCommand<AlgoritmoDeSeguridad> RepositoryCommandAlgoritmo()
        {
            if (this._repoCommandAlgoritmo == null)
            {
                this._repoCommandAlgoritmo = new RepositoryBaseCommand<AlgoritmoDeSeguridad>(this._contexto);
            }
            return this._repoCommandAlgoritmo;
        }

        public IRepositoryBaseCommand<Token> RepositoryCommandToken()
        {
            if (this._repoCommandToken == null)
            {
                this._repoCommandToken = new RepositoryBaseCommand<Token>(this._contexto);
            }
            return this._repoCommandToken;
        }

       
        public IRepositoryBaseCommand<_LogAutenticacionAPI> RepositoryCommandLog()
        {
            if (this._repoCommandLog == null)
            {
                this._repoCommandLog = new RepositoryBaseCommand<_LogAutenticacionAPI>(this._contexto);
            }
            return this._repoCommandLog;
        }


        public IRepositorySessionesCmd RepositorySessionesCmd()
        {
            return this._repoSessionCmd;
        }



        public IRepositoryBaseCommand<DatosOTP> RepositoryCommandDatosOTP()
        {
            if (this._repoCommandDatosOTP == null)
            {
                this._repoCommandDatosOTP = new RepositoryBaseCommand<DatosOTP>(this._contexto);
            }
            return this._repoCommandDatosOTP;
        }

        public IRepositoryBaseCommand<_LogOTPAPI> RepositoryCommandLogOTP()
        {
            if (this._repoCommandLogOTP == null)
            {
                this._repoCommandLogOTP = new RepositoryBaseCommand<_LogOTPAPI>(this._contexto);
            }
            return this._repoCommandLogOTP;
        }



        #endregion





        //  Queries

        public IRepositorySessionesQueries RepositorySessionesQueries()
        {
            return this._repoSessionQueries;
        }


        #region Query
        public IRepositoryBaseQuery<Rol> RepositoryQueryRol()
        {
            if (this._repoQueryRol == null)
            {
                this._repoQueryRol = new RepositoryBaseQuery<Rol>(this._contexto);
            }
            return this._repoQueryRol;
        }



        public IRepositoryBaseQuery<TipoAutenticacion> RepositoryQueryTipoAuth()
        {

            if (this._repoQueryTipoAuth == null)
            {
                this._repoQueryTipoAuth = new RepositoryBaseQuery<TipoAutenticacion>(this._contexto);
            }
            return this._repoQueryTipoAuth;
        }


        public IRepositoryBaseQuery<TipoPlantillaEmail> RepositoryQueryTipoPlantillaEmail()
        {

            if (this._repoQueryTipoPlanilla  == null)
            {
                this._repoQueryTipoPlanilla = new RepositoryBaseQuery<TipoPlantillaEmail>(this._contexto);
            }
            return this._repoQueryTipoPlanilla;
        }


        public IRepositoryBaseQuery<PlantillaEmail> RepositoryQueryPlantillaEmail()
        {

            if (this._repoQueryPlanilla == null)
            {
                this._repoQueryPlanilla = new RepositoryBaseQuery<PlantillaEmail>(this._contexto);
            }
            return this._repoQueryPlanilla;
        }




        public IRepositoryBaseQuery<Usuario> RepositoryQueryUsuario()
        {
            if (this._repoQueryUsuario == null)
            {
                this._repoQueryUsuario = new RepositoryBaseQuery<Usuario>(this._contexto);
            }
            return this._repoQueryUsuario;
        }

        public IRepositoryBaseQuery<TipoDocumento> RepositoryQueryTipoDocumento()
        {
            
            if (this._repoQueryTipoDoc == null)
            {
                this._repoQueryTipoDoc = new RepositoryBaseQuery<TipoDocumento>(this._contexto);
            }
            return this._repoQueryTipoDoc;
        }



        public IRepositoryBaseQuery<Aplicacion> RepositoryQueryAplicacion()
        {
            if (this._repoQueryAplicacion == null)
            {
                this._repoQueryAplicacion = new RepositoryBaseQuery<Aplicacion>(this._contexto);
            }
            return this._repoQueryAplicacion;
        }

        public IRepositoryBaseQuery<AlgoritmoDeSeguridad> RepositoryQueryAlgoritmo()
        {
            if (this._repoQueryAlgoritmo == null)
            {
                this._repoQueryAlgoritmo = new RepositoryBaseQuery<AlgoritmoDeSeguridad>(this._contexto);
            }
            return this._repoQueryAlgoritmo;
        }

        public IRepositoryBaseQuery<Token> RepositoryQueryToken()
        {
            if (this._repoQueryToken == null)
            {
                this._repoQueryToken = new RepositoryBaseQuery<Token>(this._contexto);
            }
            return this._repoQueryToken;
        }


        public IRepositoryBaseQuery<_LogAutenticacionAPI> RepositoryQueryLog()
        {
            if (this._repoQueryLog == null)
            {
                this._repoQueryLog = new RepositoryBaseQuery<_LogAutenticacionAPI>(this._contexto);
            }
            return this._repoQueryLog;
        }



        public IRepositoryBaseQuery<DatosOTP> RepositoryQueryDatosOTP()
        {
            if (this._repoQueryDatosOTP == null)
            {
                this._repoQueryDatosOTP = new RepositoryBaseQuery<DatosOTP>(this._contexto);
            }
            return this._repoQueryDatosOTP;
        }

        public IRepositoryBaseQuery<_LogOTPAPI> RepositoryQueryLogOTP()
        {
            if (this._repoQueryLogOTP == null)
            {
                this._repoQueryLogOTP = new RepositoryBaseQuery<_LogOTPAPI>(this._contexto);
            }
            return this._repoQueryLogOTP;
        }

        #endregion




        #region Integración
        public IConsumidorEmail ConsumidorEmail()
        {
            if (this._consumidorEmail == null)
            {
                this._consumidorEmail = new ConsumidorEmail(this._smtpSettingsOptions);
            }
            return this._consumidorEmail;
        }

        #endregion



        public ILogServiceCmd LogServiceCmd()
        {
            if (this._servLogAuth == null)
            {
                this._servLogAuth = new LogServiceCmd(configuration, this);
            }
            return this._servLogAuth;
        }


        public void InsertarLog(_LogAutenticacionAPI log)
        {
            try
            {

                this.BeginTransaction();
                this._contexto.Logs.Add(log);
                this._contexto.SaveChanges();
                this.Commit();

            }
            catch (Exception e)
            {
                this.Rollback();
                throw e;
            }
        }


        public void InsertarLogOTP(_LogOTPAPI log)
        {
            throw new NotImplementedException();
        }
    }
}
