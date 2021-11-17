using Catalogos.Infraestructura.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogos.Infraestructura.SettingsDB
{
    public class ContextoDB : DbContext
    {
        public ContextoDB(DbContextOptions<ContextoDB> options)
            : base(options)
        {
        }


        public DbSet<Catalogo> Catalogos { get; set; }
        public DbSet<Descuento> Descuentos { get; set; }
        public DbSet<MultimediaCatalogo> MultimediasCatalogo { get; set; }
        public DbSet<MultimediaProducto> MultimediasProductos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Ignore<Usuario>();

        }


    }
}
