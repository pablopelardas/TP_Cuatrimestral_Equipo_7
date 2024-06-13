using System;
using Datos.EF;
using System.Collections.Generic;
using System.Linq;
using Datos.Migrations;

public static class OrdenesSeed
{

    public static List<ORDEN> getOrdenes(Datos.EF.Entities context)
    {
        List<DIRECCION> direccionesContext = context.DIRECCIONES.ToList();
        List<EVENTO> eventosContext = context.EVENTOS.ToList();

        List<string> Descripciones = new List<string>
        {
            "Tarta de frutillas",
            "Tarta de chocolate",
            "Tarta de dulce de leche",
            "Galletas de vainilla",
            "Galletas de chocolate",
        };

        ORDEN getRandomOrder(int index)
        {
            EVENTO evento =  eventosContext[index];
            return new ORDEN
            {
                id_cliente = evento.id_cliente,
                costo_envio = Configuration.GlobalRandom.Next(100, 500),
                descripcion = Configuration.GlobalRandom.Next(0, 5) < 4 ? Descripciones[Configuration.GlobalRandom.Next(0, Descripciones.Count)] : null,
                descuento_porcentaje = Configuration.GlobalRandom.Next(0, 5) < 4 ? Configuration.GlobalRandom.Next(0, 10) : 0,
                id_evento = evento.id_evento,
                hora_entrega = new TimeSpan(Configuration.GlobalRandom.Next(0, 24), Configuration.GlobalRandom.Next(0, 60),0),
                tipo_entrega = Configuration.GlobalRandom.Next(0, 5) < 4 ? "Delivery" : "Retiro",
                id_orden_pago_estado = 1,
                id_orden_estado = 1,
                id_direccion = direccionesContext[Configuration.GlobalRandom.Next(0, direccionesContext.Count)].id_direccion,
            };
        }

        List<ORDEN> ordenes = new List<ORDEN>();

        for (int i = 0; i < 3; i++)
        {
            ordenes.Add(getRandomOrder(i));
        }

        return ordenes;
    }
}