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
                nombre = "Sin pagos"
            },
            new ORDENPAGOESTADO
            {
                nombre = "Parcialmente pagado"
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