using Catalogos.Dominio.Util;
using Catalogos.Infraestructura.Entities;
using Catalogos.Infraestructura.IRepositories.Query;
using Catalogos.Infraestructura.SettingsDB;
using Microsoft.Extensions.Configuration;
using System;

namespace Catalogos.Dominio.IUnitOfWorks
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

        IRepositoryBaseQuery<Catalogo> RepositoryCatalogoQuery();
        IRepositoryBaseQuery<Descuento> RepositoryDescuentoQuery();
        IRepositoryBaseQuery<MultimediaCatalogo> RepositoryMultimediaCatalogoQuery();
        IRepositoryBaseQuery<MultimediaProducto> RepositoryMultimediaProductoQuery();
        IRepositoryBaseQuery<Producto> RepositoryProductoQuery();        
        IRepositoryBaseQuery<Usuario> RepositoryUsuarioQuery();


        //Log: Repositorio para la auditoría de transacciones
        IRepositoryBaseQuery<_AuditoriaCatalogos> RepositoryLogQuery();


        //Utils: Interfaz usada para la transformación y conversión de entidades
        IUtils Utils();
    }


}
