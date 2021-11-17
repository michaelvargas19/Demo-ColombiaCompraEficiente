using Catalogos.Dominio.IUnitOfWorks;
using Catalogos.Dominio.Util;
using Catalogos.Infraestructura.Entities;
using Catalogos.Infraestructura.IRepositories.Query;
using Catalogos.Infraestructura.Repositories;
using Catalogos.Infraestructura.SettingsDB;
using Microsoft.Extensions.Configuration;
using System;

namespace Catalogos.Infraestructura.UnitOfWorks
{

    /// <summary>
    /// UnitOfWork implementa la interfaz IUnitOfWork, la cual contiene
    /// instancias de los repositorios, servicios útiles, acceso al appsettings.json
    /// y contexto de la base de datos
    /// </summary>
    
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ContextoDB _contexto;

        private readonly IConfiguration configuration;

        //Query: Repositorios que permiten ejecutar consultas sobre la base de datos
        private IRepositoryBaseQuery<Catalogo> _RepositoryCatalogoQuery;
        private IRepositoryBaseQuery<Descuento> _RepositoryDescuentoQuery;
        private IRepositoryBaseQuery<MultimediaCatalogo> _RepositoryMultimediaCatalogoQuery;
        private IRepositoryBaseQuery<MultimediaProducto> _RepositoryMultimediaProductoQuery;
        private IRepositoryBaseQuery<Producto> _RepositoryProductoQuery;
        private IRepositoryBaseQuery<Usuario> _RepositoryUsuarioQuery;
        private IRepositoryBaseQuery<_AuditoriaCatalogos> _RepositoryLogQuery;
        private IUtils _utils;


        public UnitOfWork(ContextoDB contexto,
                          IConfiguration configuration)
        {
            this._contexto = contexto;
            this.configuration = configuration;
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
            if (this._contexto.Database.CurrentTransaction != null)
                this._contexto.Database.RollbackTransaction();
        }

        public ContextoDB ContextoAuthDB()
        {
            return this._contexto;
        }

        public IConfiguration ConfigurationAppSettings()
        {
            return this.configuration;
        }




        //Query

        public IRepositoryBaseQuery<Catalogo> RepositoryCatalogoQuery()
        {
            if (this._RepositoryCatalogoQuery == null)
            {
                this._RepositoryCatalogoQuery = new RepositoryBaseQuery<Catalogo>(this._contexto);
            }
            return this._RepositoryCatalogoQuery;
        }

        public IRepositoryBaseQuery<Descuento> RepositoryDescuentoQuery()
        {
            if (this._RepositoryDescuentoQuery == null)
            {
                this._RepositoryDescuentoQuery = new RepositoryBaseQuery<Descuento>(this._contexto);
            }
            return this._RepositoryDescuentoQuery;
        }

        public IRepositoryBaseQuery<_AuditoriaCatalogos> RepositoryLogQuery()
        {
            if (this._RepositoryLogQuery == null)
            {
                this._RepositoryLogQuery = new RepositoryBaseQuery<_AuditoriaCatalogos>(this._contexto);
            }
            return this._RepositoryLogQuery;
        }

        public IRepositoryBaseQuery<MultimediaCatalogo> RepositoryMultimediaCatalogoQuery()
        {
            if (this._RepositoryMultimediaCatalogoQuery == null)
            {
                this._RepositoryMultimediaCatalogoQuery = new RepositoryBaseQuery<MultimediaCatalogo>(this._contexto);
            }
            return this._RepositoryMultimediaCatalogoQuery;
        }

        public IRepositoryBaseQuery<MultimediaProducto> RepositoryMultimediaProductoQuery()
        {
            if (this._RepositoryMultimediaProductoQuery == null)
            {
                this._RepositoryMultimediaProductoQuery = new RepositoryBaseQuery<MultimediaProducto>(this._contexto);
            }
            return this._RepositoryMultimediaProductoQuery;
        }

        public IRepositoryBaseQuery<Producto> RepositoryProductoQuery()
        {
            if (this._RepositoryProductoQuery == null)
            {
                this._RepositoryProductoQuery = new RepositoryBaseQuery<Producto>(this._contexto);
            }
            return this._RepositoryProductoQuery;
        }

        public IRepositoryBaseQuery<Usuario> RepositoryUsuarioQuery()
        {
            if (this._RepositoryUsuarioQuery == null)
            {
                this._RepositoryUsuarioQuery = new RepositoryBaseQuery<Usuario>(this._contexto);
            }
            return this._RepositoryUsuarioQuery;
        }

      

        public IUtils Utils()
        {
            if (this._utils== null)
            {
                this._utils = new Utils();
            }
            return this._utils;
        }
    }
}