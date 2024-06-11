using Datos.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    internal class OrdenEstadoMapper
    {
        internal static Dominio.Modelos.OrdenEstadoModelo EntidadAModelo(ORDENESTADO ordenEstadoEntidad)
        {
            return new Dominio.Modelos.OrdenEstadoModelo
            {
                IdOrdenEstado = ordenEstadoEntidad.id_orden_estado,
                Nombre = ordenEstadoEntidad.nombre
            };
        }

        internal static ORDENESTADO ModeloAEntidad(Dominio.Modelos.OrdenEstadoModelo ordenEstado)
        {
            return new ORDENESTADO
            {
                id_orden_estado = ordenEstado.IdOrdenEstado,
                nombre = ordenEstado.Nombre
            };
        }
    }
}
