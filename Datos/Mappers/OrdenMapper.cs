using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    public class OrdenMapper
    {
        public static Dominio.Modelos.OrdenModelo EntidadAModelo(Entidades.OrdenEntidad ordenEntidad)
        {
            return new Dominio.Modelos.OrdenModelo
            {
                IdOrden = ordenEntidad.id_orden,
                Fecha = ordenEntidad.fecha,
                TipoEvento = ordenEntidad.tipo_evento,
                TipoEntrega = ordenEntidad.tipo_entrega,
                DescuentoPorcentaje = ordenEntidad.descuento_porcentaje,
                IncrementoPorcentaje = ordenEntidad.incremento_porcentaje,
                Descripcion = ordenEntidad.descripcion,

            };
        }

        public static Entidades.OrdenEntidad ModeloAEntidad(Dominio.Modelos.OrdenModelo ordenModelo)
        {
            return new Entidades.OrdenEntidad
            {
                id_cliente = ordenModelo.Cliente.Id,
                id_orden = ordenModelo.IdOrden,
                fecha = ordenModelo.Fecha,
                tipo_evento = ordenModelo.TipoEvento,
                tipo_entrega = ordenModelo.TipoEntrega,
                descuento_porcentaje = ordenModelo.DescuentoPorcentaje,
                incremento_porcentaje = ordenModelo.IncrementoPorcentaje,
                descripcion = ordenModelo.Descripcion,
            };
        }
    }
}
