using Datos.EF;
using System;
using System.Collections.Generic;
using System.Linq;

public static class DetalleProductosSeed
{
    private static Random random = new Random();
    public static List<DETALLEPRODUCTO> getDetalleProductos(Datos.EF.Entities context)
    {
        List<SUMINISTRO> suministrosContext = context.SUMINISTROS.ToList();
        List<RECETA> recetasContext = context.RECETAS.ToList();
        List<PRODUCTO> productosContext = context.PRODUCTOS.ToList();

        DETALLEPRODUCTO getRandomProductoDetalle(Guid id_producto, bool esReceta)
        {
            if (esReceta)
            {
                RECETA receta = recetasContext[random.Next(0, recetasContext.Count)];
                return new DETALLEPRODUCTO()
                {
                    id_producto = id_producto,
                    cantidad = random.Next(1, 10),
                    id_receta = receta.id_receta,
                    id_suministro = null
                };
            }
            else
            {
                SUMINISTRO suministro = suministrosContext[random.Next(0, suministrosContext.Count)];
                return new DETALLEPRODUCTO()
                {
                    id_producto = id_producto,
                    cantidad = random.Next(1, 10),
                    id_receta = null,
                    id_suministro = suministro.id_suministro
                };
            }

        }

        List<DETALLEPRODUCTO> detalleProductos = new List<DETALLEPRODUCTO>();

        foreach (PRODUCTO producto in productosContext)
        {
            List<Guid> suministrosEnProducto = new List<Guid>();
            List<Guid> recetasEnProducto = new List<Guid>();
            int cantidadDetalles = random.Next(1, 20);
            for (int i = 0; i < cantidadDetalles; i++)
            {
                DETALLEPRODUCTO detalleProducto = getRandomProductoDetalle(producto.id_producto, cantidadDetalles % 2 == 0);
                if (suministrosEnProducto.Contains(detalleProducto.id_suministro ?? Guid.Empty) || recetasEnProducto.Contains(detalleProducto.id_receta ?? Guid.Empty))
                {
                    continue;
                }
                if (detalleProducto.id_suministro != null)
                {
                    suministrosEnProducto.Add(detalleProducto.id_suministro ?? Guid.Empty);
                }
                else
                {
                    recetasEnProducto.Add(detalleProducto.id_receta ?? Guid.Empty);
                }
                detalleProductos.Add(detalleProducto);
            }
        }

        return detalleProductos;

    }

}