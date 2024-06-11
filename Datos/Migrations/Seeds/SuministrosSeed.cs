using Datos.EF;
using System.Collections.Generic;

public static class SuministrosSeed
{
    public static List<SUMINISTRO> getSuministros()
    {
        return new List<SUMINISTRO>
        {
            new SUMINISTRO
            {
                id_suministro = 1,
                nombre = "Caja de Torta",
                id_categoria = 4,
                cantidad = 1,
                costo = 200,
                proveedor = "Cajas SRL"
            },
            new SUMINISTRO
            {
                id_suministro = 2,
                nombre = "Caja de Galletas",
                id_categoria = 4,
                cantidad = 1,
                costo = 100,
                proveedor = "Cajas SRL"
            },
            new SUMINISTRO
            {
                id_suministro = 3,
                nombre = "Caja de Pan",
                id_categoria = 4,
                cantidad = 1,
                costo = 150,
                proveedor = "Cajas SRL"
            },
            new SUMINISTRO
            {
                id_suministro = 4,
                nombre = "Bolsa de Torta",
                id_categoria = 4,
                cantidad = 1,
                costo = 50,
                proveedor = "Bolsas SRL"
            },
            new SUMINISTRO
            {
                id_suministro = 5,
                nombre = "Bolsa de Galletas",
                id_categoria = 4,
                cantidad = 1,
                costo = 30,
                proveedor = "Bolsas SRL"
            },
            new SUMINISTRO
            {
                id_suministro = 6,
                nombre = "Bolsa de Pan",
                id_categoria = 4,
                cantidad = 1,
                costo = 40,
                proveedor = "Bolsas SRL"
            },
            new SUMINISTRO
            {
                id_suministro = 7,
                nombre = "Tarjeta de Torta",
                id_categoria = 4,
                cantidad = 1,
                costo = 10,
                proveedor = "Tarjetas SRL"
            },
            new SUMINISTRO
            {
                id_suministro = 8,
                nombre = "Tarjeta de Galletas",
                id_categoria = 4,
                cantidad = 1,
                costo = 5,
                proveedor = "Tarjetas SRL"
            },
            new SUMINISTRO
            {
                id_suministro = 9,
                nombre = "Tarjeta de Pan",
                id_categoria = 4,
                cantidad = 1,
                costo = 7,
                proveedor = "Tarjetas SRL"
            },

        };
    }

}