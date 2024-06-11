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
           CategoriasSeed.getCategorias().ForEach(c => context.CATEGORIAS.Add(c));
           IngredientesSeed.getIngredientes().ForEach(i => context.INGREDIENTES.Add(i));
           RecetasSeed.getRecetas().ForEach(r => context.RECETAS.Add(r));
           DetalleRecetasSeed.getDetalleRecetas().ForEach(dr => context.DETALLE_RECETAS.Add(dr));
           SuministrosSeed.getSuministros().ForEach(s => context.SUMINISTROS.Add(s));
           ProductosSeed.getProductos().ForEach(p => context.PRODUCTOS.Add(p));
           DetalleProductosSeed.getDetalleProductos().ForEach(dp => context.DETALLE_PRODUCTOS.Add(dp));
           OrdenEstadosSeed.getOrdenEstados().ForEach(oe => context.ORDENES_ESTADOS.Add(oe));
           OrdenPagoEstadosSeed.getOrdenPagoEstados().ForEach(ope => context.ORDENES_PAGO_ESTADOS.Add(ope));
           TipoEventosSeed.getTipoEventos().ForEach(te => context.TIPOS_EVENTOS.Add(te));
           EventosSeed.getEventos().ForEach(e => context.EVENTOS.Add(e));
           OrdenesSeed.getOrdenes().ForEach(o => context.ORDENES.Add(o));
           DetalleOrdenesSeed.getDetalleOrdenes().ForEach(deo => context.DETALLE_ORDENES.Add(deo));
           PagosSeed.getPagos().ForEach(p => context.PAGOS.Add(p));

           context.SaveChanges();
        }

    }
}
