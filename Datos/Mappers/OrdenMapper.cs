using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    public class OrdenMapper
    {
        public static Dominio.Modelos.OrdenModelo EntidadAModelo(Entidades.OrdenEntidad ordenEntidad,/* List<Entidades.DetalleEntidad> listaDetalle, List<Entidades.ProductoEntidad> listaProductos*/)
        {
            return new Dominio.Modelos.OrdenModelo
            {
                IdOrden = ordenEntidad.id_orden,
                IdCliente = ordenEntidad.id_cliente,
                Fecha = ordenEntidad.fecha,
                TipoEvento = ordenEntidad.tipo_evento,
                TipoEntrega = ordenEntidad.tipo_entrega,
                Total = ordenEntidad.total,
                Descuento = ordenEntidad.descuento,
                Incremento = ordenEntidad.incremento,
                Descripcion = ordenEntidad.descripcion,
              //  Productos = listaProductos,

            };
        }

        public static Entidades.OrdenEntidad ModeloAEntidad(Dominio.Modelos.OrdenModelo ordenModelo)
        {
            return new Entidades.OrdenEntidad
            {
                id_cliente = ordenModelo.IdCliente,
                id_orden = ordenModelo.IdOrden,
                fecha = ordenModelo.Fecha,
                tipo_evento = ordenModelo.TipoEvento,
                tipo_entrega = ordenModelo.TipoEntrega,
                total = ordenModelo.Total,
                descuento = ordenModelo.Descuento,
                incremento = ordenModelo.Incremento,
                descripcion = ordenModelo.Descripcion,
            };
        }
    }
}
