using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    internal class OrdenEstadoMapper
    {
        internal static Dominio.Modelos.OrdenEstadoModelo EntidadAModelo(ORDEN_ESTADO ordenEstadoEntidad)
        {
            return new Dominio.Modelos.OrdenEstadoModelo
            {
                IdOrdenEstado = ordenEstadoEntidad.id_orden_estado,
                Nombre = ordenEstadoEntidad.nombre
            };
        }

        internal static ORDEN_ESTADO ModeloAEntidad(Dominio.Modelos.OrdenEstadoModelo ordenEstado)
        {
            return new ORDEN_ESTADO
            {
                id_orden_estado = ordenEstado.IdOrdenEstado,
                nombre = ordenEstado.Nombre
            };
        }
    }
}
