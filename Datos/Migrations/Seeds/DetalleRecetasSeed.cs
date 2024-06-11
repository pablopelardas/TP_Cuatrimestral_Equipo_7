using Datos.EF;
using System.Collections.Generic;

public static class DetalleRecetasSeed
{
    public static List<DETALLERECETA> getDetalleRecetas()
    {
        return new List<DETALLERECETA>
            {
                new DETALLERECETA
                {
                    id_receta = 1,
                    id_ingrediente = 1,
                    cantidad = 500
                },
                new DETALLERECETA
                {
                    id_receta = 1,
                    id_ingrediente = 2,
                    cantidad = 300
                },
                new DETALLERECETA
                {
                    id_receta = 1,
                    id_ingrediente = 3,
                    cantidad = 4
                },
                new DETALLERECETA
                {
                    id_receta = 1,
                    id_ingrediente = 4,
                    cantidad = 200
                },
                new DETALLERECETA
                {
                    id_receta = 1,
                    id_ingrediente = 5,
                    cantidad = 200
                },
                new DETALLERECETA
                {
                    id_receta = 1,
                    id_ingrediente = 6,
                    cantidad = 10
                },
                new DETALLERECETA
                {
                    id_receta = 1,
                    id_ingrediente = 7,
                    cantidad = 10
                },
                new DETALLERECETA
                {
                    id_receta = 2,
                    id_ingrediente = 1,
                    cantidad = 500
                },
                new DETALLERECETA
                {
                    id_receta = 2,
                    id_ingrediente = 2,
                    cantidad = 300
                },
                new DETALLERECETA
                {
                    id_receta = 2,
                    id_ingrediente = 3,
                    cantidad = 4
                },
                new DETALLERECETA
                {
                    id_receta = 2,
                    id_ingrediente = 4,
                    cantidad = 200
                },
                new DETALLERECETA
                {
                    id_receta = 2,
                    id_ingrediente = 5,
                    cantidad = 200
                },
                new DETALLERECETA
                {
                    id_receta = 2,
                    id_ingrediente = 6,
                    cantidad = 10
                }
            };
    }

}