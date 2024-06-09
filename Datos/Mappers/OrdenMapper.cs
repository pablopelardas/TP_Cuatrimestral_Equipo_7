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
        private Entities db = new Entities();

        internal static Dominio.Modelos.OrdenModelo EntidadAModelo(ORDENES orden)
        {
            Dominio.Modelos.OrdenModelo modelo = new Dominio.Modelos.OrdenModelo
            {
                // ATRIBUTOS DE ENTIDAD
                IdOrden = orden.id_orden,
                TipoEntrega = orden.tipo_entrega,
                DescuentoPorcentaje = orden.descuento_porcentaje ?? 0,
                CostoEnvio = orden.costo_envio ?? 0,
                Descripcion = orden.descripcion,
                HoraEntrega = orden.hora_entrega.ToString() ?? "",
                DireccionEntrega = orden.direccion_entrega,
            };

            if (orden.CONTACTOS != null)
            {
                modelo.Cliente = ContactoMapper.EntidadAModelo(orden.CONTACTOS);
            }

            if (orden.ORDENES_ESTADOS != null)
            {
                modelo.Estado = OrdenEstadoMapper.EntidadAModelo(orden.ORDENES_ESTADOS);
            }

            if (orden.ORDENES_PAGO_ESTADOS != null)
            {
                modelo.EstadoPago = OrdenEstadoPagoMapper.EntidadAModelo(orden.ORDENES_PAGO_ESTADOS);
            }

            if (orden.EVENTOS != null)
            {
                modelo.Evento = EventoMapper.EntidadAModelo(orden.EVENTOS.First());
            }
            if (orden.DETALLE_ORDENES != null)
            {
                foreach (var detalle in orden.DETALLE_ORDENES)
                {
                    modelo.DetalleProductos.Add(ProductoDetalleOrdenMapper.EntidadAModelo(detalle));
                }
            }

            return modelo;
        }

        internal static ORDENES ModeloAEntidad(Dominio.Modelos.OrdenModelo ordenModelo)
        {
            ORDENES entidad = new ORDENES
            {
                // ATRIBUTOS DE MODELO
                id_orden = ordenModelo.IdOrden,
                tipo_entrega = ordenModelo.TipoEntrega,
                descuento_porcentaje = ordenModelo.DescuentoPorcentaje,
                costo_envio = ordenModelo.CostoEnvio,
                descripcion = ordenModelo.Descripcion,
                hora_entrega = TimeSpan.Parse(ordenModelo.HoraEntrega),
                direccion_entrega = ordenModelo.DireccionEntrega,
                id_cliente = ordenModelo.Cliente.Id,
                id_orden_estado = ordenModelo.Estado != null ? ordenModelo.Estado.IdOrdenEstado : 0,
                id_orden_pago_estado = ordenModelo.EstadoPago != null ? ordenModelo.EstadoPago.IdOrdenPagoEstado : 0
            };

            return entidad;
        }

        internal static void ActualizarEntidad(ref ORDENES entidad, Dominio.Modelos.OrdenModelo modelo)
        {
            entidad.id_orden = modelo.IdOrden;
            entidad.tipo_entrega = modelo.TipoEntrega;
            entidad.descuento_porcentaje = modelo.DescuentoPorcentaje;
            entidad.costo_envio = modelo.CostoEnvio;
            entidad.descripcion = modelo.Descripcion;
            entidad.hora_entrega = TimeSpan.Parse(modelo.HoraEntrega);
            entidad.direccion_entrega = modelo.DireccionEntrega;
            entidad.id_cliente = modelo.Cliente.Id;
            entidad.id_orden_estado = modelo.Estado != null ? modelo.Estado.IdOrdenEstado : 0;
            entidad.id_orden_pago_estado = modelo.EstadoPago != null ? modelo.EstadoPago.IdOrdenPagoEstado : 0;
        }
    }
}
