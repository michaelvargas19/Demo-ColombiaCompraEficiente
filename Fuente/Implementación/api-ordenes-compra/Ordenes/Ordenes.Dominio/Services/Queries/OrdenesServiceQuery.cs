using Ordenes.Dominio.IServices.Queries;
using Ordenes.Dominio.IUnitOfWorks;
using Ordenes.Dominio.Modelo.Queries;
using Ordenes.Dominio.Util;
using Ordenes.Infraestructura.Entities;
using Ordenes.Infraestructura.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ordenes.Dominio.Services.Queries
{
    public class OrdenesServiceQuery : IOrdenesServiceQuery
    {

        private readonly IUnitOfWork _ufw;
        private readonly IUtils _utils;

        public OrdenesServiceQuery(IUnitOfWork ufw,
                                     IUtils utils)
        {
            this._ufw = ufw;
            this._utils = utils;
        }


        public OrdenCompraQuery ConsultarOrdenCompraPorUsuario(int idUsuario)
        {

            OrdenCompraQuery query = null;

            try
            {
                OrdenCompra orden = _ufw.RepositoryOrdenCompraQuery().Find(new OrdenCompraSpecification(idUsuario)).FirstOrDefault();
                
                if(orden == null)
                {
                    throw new Exception("No se ha encontrado carrito de compras");
                }

                query = this._utils.ConvertList_Orden_To_Query(orden);

                return query;

            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
