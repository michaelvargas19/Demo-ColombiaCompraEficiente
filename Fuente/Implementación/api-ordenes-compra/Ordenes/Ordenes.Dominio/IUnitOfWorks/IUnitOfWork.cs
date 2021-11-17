using Microsoft.Extensions.Configuration;
using Ordenes.Dominio.Util;
using Ordenes.Infraestructura.Entities;
using Ordenes.Infraestructura.IRepositories.Command;
using Ordenes.Infraestructura.IRepositories.Query;
using Ordenes.Infraestructura.SettingsDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordenes.Dominio.IUnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        // Persistencia
        void BeginTransaction();
        void SaveChanges();
        void Commit();
        void Rollback();

        ContextoDB ContextoAuthDB();

        IConfiguration ConfigurationAppSettings();


        //Query
        IRepositoryBaseQuery<Descuento> RepositoryDescuentoQuery();
        IRepositoryBaseQuery<OrdenCompra> RepositoryOrdenCompraQuery();
        IRepositoryBaseQuery<OrdenCompraDetalle> RepositoryOrdenCompraDetalleQuery();
        IRepositoryBaseQuery<OrdenCompraEstado> RepositoryOrdenCompraEstadoQuery();
        IRepositoryBaseQuery<Pago> RepositoryPagoQuery();
        IRepositoryBaseQuery<Producto> RepositoryProductoQuery();
        IRepositoryBaseQuery<Usuario> RepositoryUsuarioQuery();



        //Command
        IRepositoryBaseCommand<OrdenCompra> RepositoryOrdenCompraCommand();
        IRepositoryBaseCommand<OrdenCompraDetalle> RepositoryOrdenCompraDetalleCommand();
        IRepositoryBaseCommand<OrdenCompraEstado> RepositoryOrdenCompraEstadoCommand();
        IRepositoryBaseCommand<Pago> RepositoryPagoCommand();




        //Log
        IRepositoryBaseQuery<_AuditoriaOrdenes> RepositoryLogQuery();
        IRepositoryBaseCommand<_AuditoriaOrdenes> RepositoryLogCommand();
        


        //Utils
        IUtils Utils();
    }
}
