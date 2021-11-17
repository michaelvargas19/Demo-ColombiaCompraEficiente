using Autenticacion.Dominio.Modelo.Queries;
using Autenticacion.Infraestructura.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Autenticacion.Dominio.Services.Command
{
    public class UtilsAplicationServiceCmd
    {

        #region Configuración
            public static List<TipoAutenticacionQuery> tiposdeAutenticacion(List<TipoAutenticacion> tiposAutenticacion)
            {
                List<TipoAutenticacionQuery> tiposAuth = new List<TipoAutenticacionQuery>();

                foreach (TipoAutenticacion ta in tiposAutenticacion)
                {
                    tiposAuth.Add( tipodeAutenticacion(ta) );
                }

                return tiposAuth;
            }

            public static TipoAutenticacionQuery tipodeAutenticacion(TipoAutenticacion tipoAutenticacion)
            {

                TipoAutenticacionQuery tipo = new TipoAutenticacionQuery();
                tipo.IdTipo = tipoAutenticacion.IdTipo;
                tipo.Autenticacion = tipoAutenticacion.Autenticacion;
                tipo.EsDirectorioActivo = tipoAutenticacion.EsDirectorioActivo;

                return tipo;

            }




            public static List<TipoDocumentoQuery> tiposDeDocumentos(List<TipoDocumento> tiposDocumentos)
            {
                List<TipoDocumentoQuery> tiposDoc = new List<TipoDocumentoQuery>();

                foreach (TipoDocumento td in tiposDocumentos)
                {
                    tiposDoc.Add(tipoDeDocumento(td) );
                }

                return tiposDoc;
            }

            public static TipoDocumentoQuery tipoDeDocumento(TipoDocumento tiposDocumento)
            {

                TipoDocumentoQuery tipo = new TipoDocumentoQuery();
                tipo.IdTipo = tiposDocumento.IdTipo;
                tipo.Codigo = tiposDocumento.Codigo;
                tipo.Nombre = tiposDocumento.Nombre;
                tipo.Descripcion = tiposDocumento.Descripcion;
                tipo.Orden = tiposDocumento.Orden;

                return tipo;

            }

        #endregion




    }
}
