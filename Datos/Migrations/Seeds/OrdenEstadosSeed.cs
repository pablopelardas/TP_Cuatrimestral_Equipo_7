using Datos.EF;
using System.Collections.Generic;

public static class OrdenEstadosSeed
{
    public static List<ORDENESTADO> getOrdenEstados()
    {
        return new List<ORDENESTADO>
        {
            new ORDENESTADO
            {
                nombre = "Pendiente"
            },
            new ORDENESTADO
            {
                nombre = "En preparación"
            },
            new ORDENESTADO
            {
                nombre = "Finalizada"
            },
            new ORDENESTADO
            {
                nombre = "Entregada"
            },
            new ORDENESTADO
            {
                nombre = "Cancelada"
            }
        };
    }
}