using Catalogos.Dominio.Util;
using Catalogos.Infraestructura.Entities;
using Catalogos.Infraestructura.IRepositories.Query;
using Catalogos.Infraestructura.SettingsDB;
using Microsoft.Extensions.Configuration;
using System;

namespace Catalogos.Dominio.IUnitOfWorks
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
        IRepositoryBaseQuery<Catalogo> RepositoryCatalogoQuery();
        IRepositoryBaseQuery<Descuento> RepositoryDescuentoQuery();
        IRepositoryBaseQuery<MultimediaCatalogo> RepositoryMultimediaCatalogoQuery();
        IRepositoryBaseQuery<MultimediaProducto> RepositoryMultimediaProductoQuery();
        IRepositoryBaseQuery<Producto> RepositoryProductoQuery();
        //IRepositoryBaseQuery<TIPO_MULTIMEDIA> RepositoryTIPO_MULTIMEDIAQuery();
        IRepositoryBaseQuery<Usuario> RepositoryUsuarioQuery();


        //Log
        IRepositoryBaseQuery<_AuditoriaCatalogos> RepositoryLogQuery();


        //Utils
        IUtils Utils();
    }


}
