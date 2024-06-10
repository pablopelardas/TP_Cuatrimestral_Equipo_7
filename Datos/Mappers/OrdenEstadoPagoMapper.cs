using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    internal class OrdenEstadoPagoMapper
    {
        internal static Dominio.Modelos.OrdenEstadoPagoModelo EntidadAModelo(ORDEN_PAGO_ESTADO ordenEstadoPagoEntidad)
        {
            return new Dominio.Modelos.OrdenEstadoPagoModelo
            {
                IdOrdenPagoEstado = ordenEstadoPagoEntidad.id_orden_pago_estado,
                Nombre = ordenEstadoPagoEntidad.nombre
            };
        }

        internal static ORDEN_PAGO_ESTADO ModeloAEntidad(Dominio.Modelos.OrdenEstadoPagoModelo ordenEstadoPago)
        {
            return new ORDEN_PAGO_ESTADO
            {
                id_orden_pago_estado = ordenEstadoPago.IdOrdenPagoEstado,
                nombre = ordenEstadoPago.Nombre
            };
        }
    }
}
