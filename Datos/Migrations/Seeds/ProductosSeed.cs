using Datos.EF;
using System.Collections.Generic;

public static class ProductosSeed
{
    public static List<PRODUCTO> getProductos()
    {
        return new List<PRODUCTO>
        {
            new PRODUCTO
            {
                id_producto = 1,
                nombre = "Tarta de Frutillas",
                id_categoria = 4,
                descripcion = "Tarta de frutillas con crema",
                horas_trabajo = 3,
                porciones = 8,
                tipo_precio = "Margen",
                valor_precio = 30,
            },
            new PRODUCTO
            {
                id_producto = 2,
                nombre = "Tarta de Chocolate",
                id_categoria = 4,
                descripcion = "Tarta de chocolate con crema",
                horas_trabajo = 3,
                porciones = 8,
                tipo_precio = "Margen",
                valor_precio = 30,
            },
            new PRODUCTO
            {
                id_producto = 3,
                nombre = "Tarta de Dulce de Leche",
                id_categoria = 4,
                descripcion = "Tarta de dulce de leche con crema",
                horas_trabajo = 3,
                porciones = 8,
                tipo_precio = "Margen",
                valor_precio = 30,
            },
            new PRODUCTO
            {
                id_producto = 4,
                nombre = "Galletas de Vainilla",
                id_categoria = 4,
                descripcion = "Galletas de vainilla con azucar",
                horas_trabajo = 1,
                porciones = 12,
                tipo_precio = "Margen",
                valor_precio = 20,
            },
            new PRODUCTO
            {
                id_producto = 5,
                nombre = "Galletas de Chocolate",
                id_categoria = 4,
                descripcion = "Galletas de chocolate con azucar",
                horas_trabajo = 1,
                porciones = 12,
                tipo_precio = "Margen",
                valor_precio = 20,
            },
            new PRODUCTO
            {
                id_producto = 6,
                nombre = "Galletas de Limon",
                id_categoria = 4,
                descripcion = "Galletas de limon con azucar",
                horas_trabajo = 1,
                porciones = 12,
                tipo_precio = "Margen",
                valor_precio = 20,
            }
        };
    }
}