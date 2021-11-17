using Microsoft.EntityFrameworkCore;
using Ordenes.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordenes.Infraestructura.SettingsDB
{
    public class ContextoDB : DbContext
    {
        public ContextoDB(DbContextOptions<ContextoDB> options)
            : base(options)
        {
        }


        public DbSet<Descuento> Descuentos { get; set; }
        public DbSet<OrdenCompra> OrdenesCompra { get; set; }
        
        public DbSet<OrdenCompraDetalle> OrdenCompraDetalle { get; set; }
        
        public DbSet<OrdenCompraEstado> OrdenCompraEstado { get; set; }

        public DbSet<Pago> Pagos { get; set; }

        public DbSet<Producto> Productos { get; set; }
        
        public DbSet<Usuario> Usuarios { get; set; }
        
        
        public DbSet<_AuditoriaOrdenes> _AuditoriaOrdenes { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }


    }
}
