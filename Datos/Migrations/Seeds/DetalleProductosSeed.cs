using Datos.EF;
using System.Collections.Generic;

public static class DetalleProductosSeed
{
    public static List<DETALLEPRODUCTO> getDetalleProductos()
    {
        return new List<DETALLEPRODUCTO>
        {
            new DETALLEPRODUCTO
            {
                id_producto = 1,
                id_receta = 1,
                id_suministro = null,
                cantidad = 1
            },
            new DETALLEPRODUCTO
            {
                id_producto = 2,
                id_receta = 2,
                id_suministro = null,
                cantidad = 1
            },
            new DETALLEPRODUCTO
            {
                id_producto = 3,
                id_receta = 3,
                id_suministro = null,
                cantidad = 1
            },
            new DETALLEPRODUCTO
            {
                id_producto = 4,
                id_receta = 4,
                id_suministro = null,
                cantidad = 1
            },
            new DETALLEPRODUCTO
            {
                id_producto = 5,
                id_receta = 5,
                id_suministro = null,
                cantidad = 1
            },
            new DETALLEPRODUCTO
            {
                id_producto = 6,
                id_receta = null,
                id_suministro = 1,
                cantidad = 1
            },
            new DETALLEPRODUCTO
            {
                id_producto = 3,
                id_receta = null,
                id_suministro = 4,
                cantidad = 1
            },
            new DETALLEPRODUCTO
            {
                id_producto = 1,
                id_receta = null,
                id_suministro = 7,
                cantidad = 1
            }
        };
    }

}