using Ordenes.Dominio.Modelo.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordenes.Dominio.IServices.Queries
{
    public interface IOrdenesServiceQuery
    {
        OrdenCompraQuery ConsultarOrdenCompraPorUsuario(int idUsuario);

    }
}
