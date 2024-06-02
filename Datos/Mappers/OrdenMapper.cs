using Datos.Repositorios;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    internal class OrdenMapper
    {
        internal static Dominio.Modelos.OrdenModelo EntidadAModelo(Entidades.OrdenEntidad ordenEntidad, bool incluyeDetalle = false)
        {
            Repositorios.ProductoDetalleOrdenRepositorio productoDetalleOrdenRepositorio = new Repositorios.ProductoDetalleOrdenRepositorio();
            Repositorios.EventoRepositorio eventoRepositorio = new Repositorios.EventoRepositorio();

            Dominio.Modelos.OrdenModelo ordenModelo = new Dominio.Modelos.OrdenModelo
            {
                // ATRIBUTOS DE ENTIDAD
                IdOrden = ordenEntidad.id_orden,
                TipoEntrega = ordenEntidad.tipo_entrega,
                DescuentoPorcentaje = ordenEntidad.descuento_porcentaje,
                CostoEnvio = ordenEntidad.costo_envio,
                Descripcion = ordenEntidad.descripcion,
                Subtotal = ordenEntidad.subtotal,
                HoraEntrega = ordenEntidad.hora_entrega,
                Cliente = ContactoMapper.EntidadAModelo(ordenEntidad.cliente),
                Estado = OrdenEstadoMapper.EntidadAModelo(ordenEntidad.estado),
                EstadoPago = OrdenEstadoPagoMapper.EntidadAModelo(ordenEntidad.estado_pago),
            };

            // ATRIBUTOS DE OTRAS ENTIDADES
            if (incluyeDetalle)
            {
                ordenModelo.DetalleProductos = productoDetalleOrdenRepositorio.ObtenerDetallePorOrden(ordenEntidad.id_orden);
                ordenModelo.Evento = eventoRepositorio.ObtenerPorOrden(ordenEntidad.id_orden);
            }

            return ordenModelo;

            
        }

        internal static Entidades.OrdenEntidad ModeloAEntidad(Dominio.Modelos.OrdenModelo ordenModelo)
        {
            return new Entidades.OrdenEntidad
            {
                // ATRIBUTOS DE MODELO
                id_orden = ordenModelo.IdOrden,
                tipo_entrega = ordenModelo.TipoEntrega,
                descuento_porcentaje = ordenModelo.DescuentoPorcentaje,
                costo_envio = ordenModelo.CostoEnvio,
                descripcion = ordenModelo.Descripcion,
                subtotal = ordenModelo.Subtotal,
                hora_entrega = ordenModelo.HoraEntrega,
                cliente = ContactoMapper.ModeloAEntidad(ordenModelo.Cliente),
                estado = OrdenEstadoMapper.ModeloAEntidad(ordenModelo.Estado),
                estado_pago = OrdenEstadoPagoMapper.ModeloAEntidad(ordenModelo.EstadoPago),
            };
        }
    }
}
