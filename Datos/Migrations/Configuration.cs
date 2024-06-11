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
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Datos.EF.Entities context)
        { 
           ContactosSeed.GetContactos().ForEach(c => context.CONTACTOS.Add(c));
           UnidadesMedidaSeed.getUnidadMedidas().ForEach(u => context.UNIDADES_MEDIDA.Add(u));
           context.SaveChanges();
           CategoriasSeed.getCategorias().ForEach(c => context.CATEGORIAS.Add(c));
           IngredientesSeed.getIngredientes(context).ForEach(i => context.INGREDIENTES.Add(i));
           context.SaveChanges();
           RecetasSeed.getRecetas(context).ForEach(r => context.RECETAS.Add(r));
           context.SaveChanges();
           DetalleRecetasSeed.getDetalleRecetas(context).ForEach(dr => context.DETALLE_RECETAS.Add(dr));
           SuministrosSeed.getSuministros(context).ForEach(s => context.SUMINISTROS.Add(s));
           context.SaveChanges();
            ProductosSeed.getProductos(context).ForEach(p => context.PRODUCTOS.Add(p));
            context.SaveChanges();
            DetalleProductosSeed.getDetalleProductos(context).ForEach(dp => context.DETALLE_PRODUCTOS.Add(dp));
            OrdenEstadosSeed.getOrdenEstados().ForEach(oe => context.ORDENES_ESTADOS.Add(oe));
            OrdenPagoEstadosSeed.getOrdenPagoEstados().ForEach(ope => context.ORDENES_PAGO_ESTADOS.Add(ope));
            TipoEventosSeed.getTipoEventos().ForEach(te => context.TIPOS_EVENTOS.Add(te));
            context.SaveChanges();
            EventosSeed.getEventos(context).ForEach(e => context.EVENTOS.Add(e));
            context.SaveChanges();
            OrdenesSeed.getOrdenes(context).ForEach(o => context.ORDENES.Add(o));
            context.SaveChanges();
            DetalleOrdenesSeed.getDetalleOrdenes(context).ForEach(deo => context.DETALLE_ORDENES.Add(deo));
            //PagosSeed.getPagos().ForEach(p => context.PAGOS.Add(p));

            context.SaveChanges();
        }

    }
}
