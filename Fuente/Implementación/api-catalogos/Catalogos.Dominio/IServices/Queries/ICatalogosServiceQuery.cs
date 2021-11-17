using Catalogos.Dominio.Modelo.Queries;
using System.Collections.Generic;

namespace Catalogos.Dominio.IServices.Queries
{
    /// <summary>
    /// Interfaz que contiene las firmas de los métodos de consulta
    /// que acceden a los repositórios de catálogos
    /// </summary>
    public interface ICatalogosServiceQuery
    {
        IEnumerable<CatalogoQuery> VerCatalogos();
        CatalogoQuery verCatalogoPorCodigo(string codigo);
        IEnumerable<CatalogoQuery> verPaginacion(int skip, int take);
    }
}
