using Catalogos.Dominio.Modelo.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogos.Dominio.IServices.Queries
{
    /// <summary>
    /// Interfaz que contiene las firmas de los métodos de consulta
    /// que acceden a los repositorios que consultan información de productos
    /// </summary>
    public interface IProductosServiceQuery
    {
        IEnumerable<ProductoQuery> verPaginacion(int skip, int take);

        IEnumerable<ProductoQuery> verRankingFullText(string texto, int skip, int take);

        IEnumerable<ProductoQuery> verRankingCatalogo(int codigoCatalogo, int skip, int take);

        IEnumerable<ProductoQuery> verRankingMarca(string marca, int skip, int take);

        ProductoQuery verProductoPorCodigo(int id);

        ProductoQuery verProductoPorSKU(string codigo);

    }
}
