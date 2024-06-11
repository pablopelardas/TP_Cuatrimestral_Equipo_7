using System;
using Datos.EF;
using System.Collections.Generic;

public static class OrdenesSeed
{
    public static List<ORDEN> getOrdenes()
    {
        return new List<ORDEN>
        {
            new ORDEN
            {
                id_orden = 1,
                id_cliente = 1,
                costo_envio = 250,
                descripcion = "Tarta de frutillas",
                descuento_porcentaje = 0,
                id_evento = 1,
                hora_entrega = new TimeSpan(10, 0, 0),
                direccion_entrega = "Av. Siempre Viva 123",
                tipo_entrega = "Delivery"
            },
            new ORDEN
            {
                id_orden = 2,
                id_cliente = 2,
                costo_envio = 250,
                descripcion = "Tarta de chocolate",
                descuento_porcentaje = 0,
                id_evento = 2,
                hora_entrega = new TimeSpan(10, 0, 0),
                direccion_entrega = "Calle Falsa 123",
                tipo_entrega = "Delivery"
            },
            new ORDEN
            {
                id_orden = 3,
                id_cliente = 1,
                costo_envio = 250,
                descripcion = "Tarta de dulce de leche",
                descuento_porcentaje = 0,
                id_evento = 3,
                hora_entrega = new TimeSpan(10, 0, 0),
                direccion_entrega = "Av. Siempre Viva 123",
                tipo_entrega = "Delivery"
            },
            new ORDEN
            {
                id_orden = 4,
                id_cliente = 2,
                costo_envio = 250,
                descripcion = "Galletas de vainilla",
                descuento_porcentaje = 0,
                id_evento = 4,
                hora_entrega = new TimeSpan(10, 0, 0),
                direccion_entrega = "Calle Falsa 123",
                tipo_entrega = "Delivery"
            },
            new ORDEN
            {id_orden = 5,
                id_cliente = 1,
                costo_envio = 250,
                descripcion = "Galletas de chocolate",
                descuento_porcentaje = 0,
                id_evento = 5,
                hora_entrega = new TimeSpan(10, 0, 0),
                direccion_entrega = "Av. Siempre Viva 123",
                tipo_entrega = "Delivery"
            },
        };
    }
}