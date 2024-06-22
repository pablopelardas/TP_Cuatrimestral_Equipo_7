using Datos.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using Datos.Migrations;

public static class ProductosSeed
{
    public static List<PRODUCTO> getProductos(Datos.EF.Entities context)
    {
        List<CATEGORIA> categorias = context.CATEGORIAS.Where(c => c.tipo == "Receta").ToList();

        Guid randomCategoryGuid()
        {
            int index = Configuration.GlobalRandom.Next(0, categorias.Count);
            return categorias[index].id_categoria;
        }
        return new List<PRODUCTO>
        {
            new PRODUCTO
            {
                nombre = "Chocotorta",
                id_categoria = categorias.Find(c => c.nombre == "Tortas").id_categoria,
                descripcion = "Torta chocotorta",
                horas_trabajo = 1,
                porciones = 8,
                tipo_precio = "Margen",
                valor_precio = 30,
            },
            new PRODUCTO
            {
                nombre = "Alfajores de Maicena x10",
                id_categoria = categorias.Find(c => c.nombre == "Alfajores").id_categoria,
                descripcion = "Alfajores de maicena x10",
                horas_trabajo = 2,
                porciones = 10,
                tipo_precio = "Margen",
                valor_precio = 50,
            },
        };
    }
}