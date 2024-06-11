using Datos.EF;
using System.Collections.Generic;

public static class OrdenPagoEstadosSeed
{
    public static List<ORDENPAGOESTADO> getOrdenPagoEstados()
    {
        return new List<ORDENPAGOESTADO>
        {
            new ORDENPAGOESTADO
            {
                nombre = "Sin Pagos"
            },
            new ORDENPAGOESTADO
            {
                nombre = "Parcialmente Pagado"
            },
            new ORDENPAGOESTADO
            {
                nombre = "Pagado"
            },
            new ORDENPAGOESTADO
            {
                nombre = "Cancelado"
            }
        };
    }
}