namespace Datos.Migrations
{
    using Datos.EF;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Datos.EF.Entities>
    {

        public static Random GlobalRandom = new Random("seed".GetHashCode());
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Datos.EF.Entities context)
        { 
            
            // This method will be called after migrating to the latest version.
            // You can use the DbSet<T>.AddOrUpdate() helper extension method
            // to avoid creating duplicate seed data.
            
           ContactosSeed.GetContactos().ForEach(c => context.CONTACTOS.AddOrUpdate(c));
           UnidadesMedidaSeed.getUnidadMedidas().ForEach(u => context.UNIDADES_MEDIDA.AddOrUpdate(u));
           context.SaveChanges();
           CategoriasSeed.getCategorias().ForEach(c => context.CATEGORIAS.AddOrUpdate(c));
           IngredientesSeed.getIngredientes(context).ForEach(i => context.INGREDIENTES.AddOrUpdate(i));
           context.SaveChanges();
           RecetasSeed.getRecetas(context).ForEach(r => context.RECETAS.AddOrUpdate(r));
           context.SaveChanges();
           DetalleRecetasSeed.getDetalleRecetas(context).ForEach(dr => context.DETALLE_RECETAS.AddOrUpdate(dr));
           SuministrosSeed.getSuministros(context).ForEach(s => context.SUMINISTROS.AddOrUpdate(s));
           context.SaveChanges();
            ProductosSeed.getProductos(context).ForEach(p => context.PRODUCTOS.AddOrUpdate(p));
            context.SaveChanges();
            DetalleProductosSeed.getDetalleProductos(context).ForEach(dp => context.DETALLE_PRODUCTOS.AddOrUpdate(dp));
            OrdenEstadosSeed.getOrdenEstados().ForEach(oe => context.ORDENES_ESTADOS.AddOrUpdate(oe));
            OrdenPagoEstadosSeed.getOrdenPagoEstados().ForEach(ope => context.ORDENES_PAGO_ESTADOS.AddOrUpdate(ope));
            TipoEventosSeed.getTipoEventos().ForEach(te => context.TIPOS_EVENTOS.AddOrUpdate(te));
            context.SaveChanges();
            EventosSeed.getEventos(context).ForEach(e => context.EVENTOS.AddOrUpdate(e));
            context.SaveChanges();
            DireccionesSeed.getDirecciones().ForEach(d => context.DIRECCIONES.AddOrUpdate(d));
            context.SaveChanges();
            OrdenesSeed.getOrdenes(context).ForEach(o => context.ORDENES.AddOrUpdate(o));
            context.SaveChanges();
            DetalleOrdenesSeed.getDetalleOrdenes(context).ForEach(deo => context.DETALLE_ORDENES.AddOrUpdate(deo));
            context.SaveChanges();
        }

    }
}
