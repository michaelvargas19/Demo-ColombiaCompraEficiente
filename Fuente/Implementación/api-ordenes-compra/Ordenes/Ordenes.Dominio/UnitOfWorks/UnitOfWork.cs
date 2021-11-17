using Microsoft.Extensions.Configuration;
using Ordenes.Dominio.IUnitOfWorks;
using Ordenes.Dominio.Util;
using Ordenes.Infraestructura.Entities;
using Ordenes.Infraestructura.IRepositories.Command;
using Ordenes.Infraestructura.IRepositories.Query;
using Ordenes.Infraestructura.Repositories;
using Ordenes.Infraestructura.Repositories.Command;
using Ordenes.Infraestructura.SettingsDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordenes.Dominio.UnitOfWorks
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
        private IRepositoryBaseQuery<Descuento> _RepositoryDescuentoQuery;
        private IRepositoryBaseQuery<OrdenCompra> _RepositoryOrdenCompraQuery;
        private IRepositoryBaseQuery<OrdenCompraDetalle> _RepositoryOrdenCompraDetalleQuery;
        private IRepositoryBaseQuery<OrdenCompraEstado> _RepositoryOrdenCompraEstadoQuery;
        private IRepositoryBaseQuery<Pago> _RepositoryPagoQuery;
        private IRepositoryBaseQuery<Producto> _RepositoryProductoQuery;
        private IRepositoryBaseQuery<Usuario> _RepositoryUsuarioQuery;
        private IRepositoryBaseQuery<_AuditoriaOrdenes> _RepositoryLogQuery;

        //Command: Repositorios que permiten ejecutar comandos sobre la base de datos
        private IRepositoryBaseCommand<Descuento> _RepositoryDescuentoCommand;
        private IRepositoryBaseCommand<OrdenCompra> _RepositoryOrdenCompraCommand;
        private IRepositoryBaseCommand<OrdenCompraDetalle> _RepositoryOrdenCompraDetalleCommand;
        private IRepositoryBaseCommand<OrdenCompraEstado> _RepositoryOrdenCompraEstadoCommand;
        private IRepositoryBaseCommand<Pago> _RepositoryPagoCommand;
        private IRepositoryBaseCommand<_AuditoriaOrdenes> _RepositoryLogCommand;

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
        public IRepositoryBaseQuery<Descuento> RepositoryDescuentoQuery()
        {
            if (this._RepositoryDescuentoQuery == null)
            {
                this._RepositoryDescuentoQuery = new RepositoryBaseQuery<Descuento>(this._contexto);
            }
            return this._RepositoryDescuentoQuery;
        }

        public IRepositoryBaseQuery<OrdenCompra> RepositoryOrdenCompraQuery()
        {
            if (this._RepositoryOrdenCompraQuery == null)
            {
                this._RepositoryOrdenCompraQuery = new RepositoryBaseQuery<OrdenCompra>(this._contexto);
            }
            return this._RepositoryOrdenCompraQuery;
        }
        
        public IRepositoryBaseQuery<OrdenCompraDetalle> RepositoryOrdenCompraDetalleQuery()
        {
            if (this._RepositoryOrdenCompraDetalleQuery == null)
            {
                this._RepositoryOrdenCompraDetalleQuery = new RepositoryBaseQuery<OrdenCompraDetalle>(this._contexto);
            }
            return this._RepositoryOrdenCompraDetalleQuery;
        }


        public IRepositoryBaseQuery<OrdenCompraEstado> RepositoryOrdenCompraEstadoQuery()
        {
            if (this._RepositoryOrdenCompraEstadoQuery == null)
            {
                this._RepositoryOrdenCompraEstadoQuery = new RepositoryBaseQuery<OrdenCompraEstado>(this._contexto);
            }
            return this._RepositoryOrdenCompraEstadoQuery;
        }


        public IRepositoryBaseQuery<Pago> RepositoryPagoQuery()
        {
            if (this._RepositoryPagoQuery == null)
            {
                this._RepositoryPagoQuery = new RepositoryBaseQuery<Pago>(this._contexto);
            }
            return this._RepositoryPagoQuery;
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


        public IRepositoryBaseQuery<_AuditoriaOrdenes> RepositoryLogQuery()
        {
            if (this._RepositoryLogQuery == null)
            {
                this._RepositoryLogQuery = new RepositoryBaseQuery<_AuditoriaOrdenes>(this._contexto);
            }
            return this._RepositoryLogQuery;
        }



        //Command


        public IRepositoryBaseCommand<OrdenCompra> RepositoryOrdenCompraCommand()
        {
            if (this._RepositoryOrdenCompraCommand == null)
            {
                this._RepositoryOrdenCompraCommand = new RepositoryBaseCommand<OrdenCompra>(this._contexto);
            }
            return this._RepositoryOrdenCompraCommand;
        }

        public IRepositoryBaseCommand<OrdenCompraDetalle> RepositoryOrdenCompraDetalleCommand()
        {
            if (this._RepositoryOrdenCompraDetalleCommand == null)
            {
                this._RepositoryOrdenCompraDetalleCommand = new RepositoryBaseCommand<OrdenCompraDetalle>(this._contexto);
            }
            return this._RepositoryOrdenCompraDetalleCommand;
        }

        public IRepositoryBaseCommand<OrdenCompraEstado> RepositoryOrdenCompraEstadoCommand()
        {
            if (this._RepositoryOrdenCompraEstadoCommand == null)
            {
                this._RepositoryOrdenCompraEstadoCommand = new RepositoryBaseCommand<OrdenCompraEstado>(this._contexto);
            }
            return this._RepositoryOrdenCompraEstadoCommand;
        }

        public IRepositoryBaseCommand<Pago> RepositoryPagoCommand()
        {
            if (this._RepositoryPagoCommand == null)
            {
                this._RepositoryPagoCommand = new RepositoryBaseCommand<Pago>(this._contexto);
            }
            return this._RepositoryPagoCommand;
        }




        //Log
        public IRepositoryBaseCommand<_AuditoriaOrdenes> RepositoryLogCommand()
        {
            if (this._RepositoryLogCommand == null)
            {
                this._RepositoryLogCommand = new RepositoryBaseCommand<_AuditoriaOrdenes>(this._contexto);
            }
            return this._RepositoryLogCommand;
        }


        public IUtils Utils()
        {
            if (this._utils == null)
            {
                this._utils = new Utils();
            }
            return this._utils;
        }
    }
}
