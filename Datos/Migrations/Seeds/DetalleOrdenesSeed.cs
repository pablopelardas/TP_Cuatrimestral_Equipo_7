using Datos.EF;
using System;
using System.Collections.Generic;
using System.Linq;

public static class DetalleOrdenesSeed
{
    private static Random random = new Random();
    public static List<DETALLEORDEN> getDetalleOrdenes(Datos.EF.Entities context)
    {
        List<PRODUCTO> productosContext = context.PRODUCTOS.ToList();
        List<ORDEN> ordenesContext = context.ORDENES.ToList();

        DETALLEORDEN getRandomDetalleOrden(Guid id_orden)
        {
           PRODUCTO producto = productosContext[random.Next(0, productosContext.Count)];

           decimal producto_costo = producto.DETALLE_PRODUCTOS.Sum(x =>
           {
               if (x.id_receta == null)
               {
                   return (decimal)x.SUMINISTRO.costo * x.cantidad;
               }
               else
               {
                   return x.RECETA.DETALLE_RECETAS.Sum(y => (decimal)y.INGREDIENTE.costo * (decimal)y.cantidad);
               }
           });
           // 30% de ganancia (MARGEN)
           decimal producto_precio = producto_costo + (producto_costo * 30 / 100);


           return new DETALLEORDEN()
            {
                id_orden = id_orden,
                id_producto = producto.id_producto,
                cantidad = random.Next(1, 10),
                producto_costo = producto_costo,
                producto_porciones = producto.porciones,
                producto_precio = producto_precio

            };

        }
        List<DETALLEORDEN> detallesOrden = new List<DETALLEORDEN>();

        foreach (ORDEN orden in ordenesContext)
        {
            List<Guid> productosEnOrden = new List<Guid>();
            int cantidadDetalle = random.Next(1, 10);
            for (int i = 0; i < cantidadDetalle; i++)
            {
                DETALLEORDEN detalleOrden = getRandomDetalleOrden(orden.id_orden);
                if (productosEnOrden.Contains(detalleOrden.id_producto))
                {
                    continue;
                }
                productosEnOrden.Add(detalleOrden.id_producto);
                detallesOrden.Add(detalleOrden);
            }
        }

        return detallesOrden;
    }
}