
using Catalogos.Dominio.Modelo.Queries;
using Catalogos.Infraestructura.Entities;
using System.Collections.Generic;

namespace Catalogos.Dominio.Util
{
    public interface IUtils
    {

        //          [Catálogos]     -------------------------
        #region Catálogos

       
        IEnumerable<CatalogoQuery> ConvertList_Catalogo_To_Query(IEnumerable<Catalogo> catalogos);


        CatalogoQuery Convert_Catalogo_To_Query(Catalogo catalogo);

        #endregion



        //          [Productos]     -------------------------

        #region Productos

       
        IEnumerable<ProductoQuery> ConvertList_Producto_To_Query(IEnumerable<Producto> productos);


        ProductoQuery Convert_Producto_To_Query(Producto producto);

        #endregion


        //          [Multimedia]     -------------------------

        #region Multimedia

        MultimediaQuery Convert_Multimedia_To_MultimediaCatalogoQuery(MultimediaCatalogo multimedia);


     
        #endregion


        
        

        //          [Descuento]     -------------------------

        #region Descuento

        DescuentoQuery Convert_Descuento_To_DescuentoQuery(Descuento descuento);

        
        #endregion


    }
}
