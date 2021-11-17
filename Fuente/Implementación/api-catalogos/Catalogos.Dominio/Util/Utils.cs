using Catalogos.Dominio.Modelo.Queries;
using Catalogos.Infraestructura.Entities;
using System.Collections.Generic;

namespace Catalogos.Dominio.Util
{
    public class Utils : IUtils
    {

        //          [Catálogos]     -------------------------

        #region Catálogos

        
        public IEnumerable<CatalogoQuery> ConvertList_Catalogo_To_Query(IEnumerable<Catalogo> catalogos)
        {
            List<CatalogoQuery> q = new List<CatalogoQuery>();

            foreach (Catalogo c in catalogos)
            {
                q.Add(this.Convert_Catalogo_To_Query(c));
            }

            return q;
        }


        public CatalogoQuery Convert_Catalogo_To_Query(Catalogo catalogo)
        {
            CatalogoQuery q = new CatalogoQuery();

            q.Id = catalogo.idCatalogo;
            q.CodigoCatalogo = catalogo.CodigoCatalogo;
            q.Nombre = catalogo.Nombre;
            q.Multimedia = (q.Multimedia!=null)? this.Convert_Multimedia_To_MultimediaCatalogoQuery(catalogo.Multimedia) : null;
            q.Descripcion = catalogo.Descripcion;
            q.FechaFin = catalogo.FechaFin;
            q.Calificacion = catalogo.Calificacion;
            

            return q;
        }

        #endregion



        //          [Productos]     -------------------------

        #region Productos

        

        public IEnumerable<ProductoQuery> ConvertList_Producto_To_Query(IEnumerable<Producto> productos)
        {
            List<ProductoQuery> q = new List<ProductoQuery>();

            foreach (Producto p in productos)
            {
                q.Add(this.Convert_Producto_To_Query(p));
            }

            return q;
        }


        public ProductoQuery Convert_Producto_To_Query(Producto producto)
        {
            ProductoQuery p = new ProductoQuery();

            p.Id = producto.idProducto;
            p.Nombre = producto.Nombre;
            p.Descripcion = producto.Descripcion;
            p.sku = producto.SKU;
            p.iva = producto.iva;
            p.PesoKg = producto.PesoKg;
            p.ValorUnitario = producto.ValorUnitario;
            p.NivelInventario = producto.NivelInventario;
            p.Descuento = this.Convert_Descuento_To_DescuentoQuery(producto.Descuento);
            p.Multimedia = (p.Multimedia!=null) ? this.Convert_Multimedia_To_MultimediaProductoQuery(producto.Multimedia) : null;
            p.Marca = producto.Marca;
            p.EnAlmacen = producto.EnAlmacen;
            p.Calificacion = producto.Calificacion;



            
            return p;
        }

        #endregion



        //          [Multimedia]     -------------------------

        #region Multimedia


        public IEnumerable<MultimediaQuery> Convert_Multimedia_To_MultimediaCatalogoQuery(IEnumerable<MultimediaCatalogo> multimedias)
        {
            List<MultimediaQuery> dto = new List<MultimediaQuery>();

            foreach (MultimediaCatalogo multimedia in multimedias)
            {
                dto.Add(this.Convert_Multimedia_To_MultimediaCatalogoQuery(multimedia));
            }

            return dto;
        }


        public MultimediaQuery Convert_Multimedia_To_MultimediaCatalogoQuery(MultimediaCatalogo multimedia)
        {
            MultimediaQuery m = null;
            
            if (multimedia != null) { 
                m = new MultimediaQuery();
                m.Nombre = multimedia.Nombre;
                m.Descripcion = multimedia.Descripcion;
                m.Tipo = multimedia.Tipo;
                m.NombreTipo = multimedia.Tipo.ToString();
                m.url = multimedia.url;
            }

            return m;
        }


        public IEnumerable<MultimediaQuery> Convert_Multimedia_To_MultimediaProductoQuery(IEnumerable<MultimediaProducto> multimedias)
        {
            List<MultimediaQuery> dto = new List<MultimediaQuery>();

            foreach (MultimediaProducto multimedia in multimedias)
            {
                dto.Add(this.Convert_Multimedia_To_MultimediaProductoQuery(multimedia));
            }
          
            return dto;
        }



        public MultimediaQuery Convert_Multimedia_To_MultimediaProductoQuery(MultimediaProducto multimedia)
        {
            MultimediaQuery m = null;

            if (multimedia != null)
            {
                m = new MultimediaQuery();
                m.Nombre = multimedia.Nombre;
                m.Descripcion = multimedia.Descripcion;
                m.Tipo = multimedia.Tipo;
                m.NombreTipo = multimedia.Tipo.ToString();
                m.url = multimedia.url;
            }

            return m;
        }


        #endregion







        //          [Descuento]     -------------------------

        #region Descuento

        public DescuentoQuery Convert_Descuento_To_DescuentoQuery(Descuento descuento)
        {
            DescuentoQuery d = null;

            if (descuento != null)
            {
                d = new DescuentoQuery();
                d.idDescuento = descuento.idDescuento;
                d.idProducto = descuento.idProducto;
                d.Nombre = descuento.Nombre;
                d.Descripcion = descuento.Descripcion;
                d.Porcentaje = descuento.Porcentaje;
            }

            return d;
        }


        #endregion

    }
}
