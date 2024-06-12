using System;
using Datos.EF;
using System.Collections.Generic;
using System.Linq;

public static class OrdenesSeed
{
    private static Random random = new Random();

    public static List<ORDEN> getOrdenes(Datos.EF.Entities context)
    {

        List<EVENTO> eventosContext = context.EVENTOS.ToList();

        List<string> Descripciones = new List<string>
        {
            "Tarta de frutillas",
            "Tarta de chocolate",
            "Tarta de dulce de leche",
            "Galletas de vainilla",
            "Galletas de chocolate",
        };

        ORDEN getRandomOrder()
        {
            EVENTO evento = eventosContext[random.Next(0, eventosContext.Count)];
            return new ORDEN
            {
                id_cliente = evento.id_cliente,
                costo_envio = random.Next(100, 500),
                descripcion = random.Next(0, 5) < 4 ? Descripciones[random.Next(0, Descripciones.Count)] : null,
                descuento_porcentaje = random.Next(0, 5) < 4 ? random.Next(0, 10) : 0,
                id_evento = evento.id_evento,
                hora_entrega = new TimeSpan(random.Next(0, 24), random.Next(0, 60),0),
                direccion_entrega = random.Next(0, 5) < 4 ? "Calle Falsa 123" : "Av. Siempre Viva 123",
                tipo_entrega = random.Next(0, 5) < 4 ? "Delivery" : "Retiro",
                id_orden_pago_estado = 1,
                id_orden_estado = 1
            };
        }

        List<ORDEN> ordenes = new List<ORDEN>();

        for (int i = 0; i < 5; i++)
        {
            ordenes.Add(getRandomOrder());
        }

        return ordenes;
    }
}