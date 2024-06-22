using Datos.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using Datos.Migrations;

public static class DetalleProductosSeed
{
    public static List<DETALLEPRODUCTO> getDetalleProductos(Datos.EF.Entities context)
    {
        List<SUMINISTRO> suministrosContext = context.SUMINISTROS.ToList();
        List<RECETA> recetasContext = context.RECETAS.ToList();
        List<PRODUCTO> productosContext = context.PRODUCTOS.ToList();

        List<DETALLEPRODUCTO> detalleProductos = new List<DETALLEPRODUCTO>();
        
        // DetalleProducto de Producto Chocotorta
        detalleProductos.AddRange(new List<DETALLEPRODUCTO>
        {
            new DETALLEPRODUCTO
            {
                id_producto = productosContext.Find(p => p.nombre == "Chocotorta").id_producto,
                id_receta = recetasContext.Find(r => r.nombre == "Chocotorta").id_receta,
                cantidad = 1,
            },
            new DETALLEPRODUCTO
            {
                id_producto = productosContext.Find(p => p.nombre == "Chocotorta").id_producto,
                id_suministro = suministrosContext.Find(s => s.nombre == "Caja Torta 25x25x20 s/v").id_suministro,
                cantidad = 1
            }
        });
        
        // DetalleProducto de Producto Alfajores de Maicena x10
        detalleProductos.AddRange(new List<DETALLEPRODUCTO>
        {
            new DETALLEPRODUCTO
            {
                id_producto = productosContext.Find(p => p.nombre == "Alfajores de Maicena x10").id_producto,
                id_receta = recetasContext.Find(r => r.nombre == "Alfajores de maicena").id_receta,
                cantidad = 1,
            },
            new DETALLEPRODUCTO
            {
                id_producto = productosContext.Find(p => p.nombre == "Alfajores de Maicena x10").id_producto,
                id_suministro = suministrosContext.Find(s => s.nombre == "Caja de alfajores").id_suministro,
                cantidad = 1
            }
        });
           
        return detalleProductos;

    }

}