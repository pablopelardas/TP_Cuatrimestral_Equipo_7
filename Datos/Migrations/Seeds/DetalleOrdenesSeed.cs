using Datos.EF;
using System.Collections.Generic;

public static class DetalleOrdenesSeed
{
    public static List<DETALLEORDEN> getDetalleOrdenes()
    {
        return new List<DETALLEORDEN>
        {
            new DETALLEORDEN
            {
                id_orden = 1,
                id_producto = 1,
                cantidad = 1
            },
            new DETALLEORDEN
            {
                id_orden = 2,
                id_producto = 2,
                cantidad = 1
            },
        };
    }
}