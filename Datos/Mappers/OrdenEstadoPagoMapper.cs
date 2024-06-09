using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    internal class OrdenEstadoPagoMapper
    {
        internal static Dominio.Modelos.OrdenEstadoPagoModelo EntidadAModelo(ORDENES_PAGO_ESTADOS ordenEstadoPagoEntidad)
        {
            return new Dominio.Modelos.OrdenEstadoPagoModelo
            {
                IdOrdenPagoEstado = ordenEstadoPagoEntidad.id_orden_pago_estado,
                Nombre = ordenEstadoPagoEntidad.nombre
            };
        }

        internal static ORDENES_PAGO_ESTADOS ModeloAEntidad(Dominio.Modelos.OrdenEstadoPagoModelo ordenEstadoPago)
        {
            return new ORDENES_PAGO_ESTADOS
            {
                id_orden_pago_estado = ordenEstadoPago.IdOrdenPagoEstado,
                nombre = ordenEstadoPago.Nombre
            };
        }
    }
}
