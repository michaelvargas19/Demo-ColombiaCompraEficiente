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
    /// <summary>
    /// IUnitOfWork es una interfaz que contiene
    /// firmas de métodos que permiten acceder a instancias de 
    /// los repositorios, servicios útiles, acceso al appsettings.json
    /// y contexto de la base de datos
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        // Manejo de Transacciones en la Persistencia
        void BeginTransaction();
        void SaveChanges();
        void Commit();
        void Rollback();

        //Contexto de la base de datos
        ContextoDB ContextoAuthDB();

        //Configuración contenida en el appsettings
        IConfiguration ConfigurationAppSettings();


        //Query: Repositorios que permiten ejecutar consultas sobre la base de datos
        IRepositoryBaseQuery<Descuento> RepositoryDescuentoQuery();
        IRepositoryBaseQuery<OrdenCompra> RepositoryOrdenCompraQuery();
        IRepositoryBaseQuery<OrdenCompraDetalle> RepositoryOrdenCompraDetalleQuery();
        IRepositoryBaseQuery<OrdenCompraEstado> RepositoryOrdenCompraEstadoQuery();
        IRepositoryBaseQuery<Pago> RepositoryPagoQuery();
        IRepositoryBaseQuery<Producto> RepositoryProductoQuery();
        IRepositoryBaseQuery<Usuario> RepositoryUsuarioQuery();



        //Command: Repositorios que permiten ejecutar comandos sobre la base de datos
        IRepositoryBaseCommand<OrdenCompra> RepositoryOrdenCompraCommand();
        IRepositoryBaseCommand<OrdenCompraDetalle> RepositoryOrdenCompraDetalleCommand();
        IRepositoryBaseCommand<OrdenCompraEstado> RepositoryOrdenCompraEstadoCommand();
        IRepositoryBaseCommand<Pago> RepositoryPagoCommand();




        //Log: Repositorios de auditoría
        IRepositoryBaseQuery<_AuditoriaOrdenes> RepositoryLogQuery();
        IRepositoryBaseCommand<_AuditoriaOrdenes> RepositoryLogCommand();
        


        //Utils: Interfaz usada para la transformación y conversión de entidades
        IUtils Utils();
    }
}
