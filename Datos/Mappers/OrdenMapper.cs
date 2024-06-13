using Datos.EF;
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
        internal static Dominio.Modelos.OrdenModelo EntidadAModelo(ORDEN orden)
        {
            Dominio.Modelos.OrdenModelo modelo = new Dominio.Modelos.OrdenModelo
            {
                // ATRIBUTOS DE ENTIDAD
                IdOrden = orden.id_orden,
                TipoEntrega = orden.tipo_entrega,
                DescuentoPorcentaje = decimal.Round(orden.descuento_porcentaje ?? 0, 2),
                CostoEnvio = decimal.Round(orden.costo_envio ?? 0, 2),
                Descripcion = orden.descripcion,
                HoraEntrega = orden.hora_entrega,
            };

            if (orden.CLIENTE != null)
            {
                modelo.Cliente = ContactoMapper.EntidadAModelo(orden.CLIENTE);
            }

            if (orden.ESTADO != null)
            {
                modelo.Estado = OrdenEstadoMapper.EntidadAModelo(orden.ESTADO);
            }

            if (orden.ESTADO_PAGO != null)
            {
                modelo.EstadoPago = OrdenEstadoPagoMapper.EntidadAModelo(orden.ESTADO_PAGO);
            }

            if (orden.EVENTO != null)
            {
                modelo.Evento = EventoMapper.EntidadAModelo(orden.EVENTO, false);
            }
            if (orden.DETALLE_ORDENES != null)
            {
                foreach (var detalle in orden.DETALLE_ORDENES)
                {
                    modelo.DetalleProductos.Add(ProductoDetalleOrdenMapper.EntidadAModelo(detalle));
                }
            }

            if (orden.DIRECCION != null)
            {
                modelo.DireccionEntrega = DireccionMapper.EntidadAModelo(orden.DIRECCION);
            }

            return modelo;
        }

        internal static List<Dominio.Modelos.OrdenModelo> EntidadesAModelos(List<ORDEN> ordenes)
        {
            List<Dominio.Modelos.OrdenModelo> modelos = new List<Dominio.Modelos.OrdenModelo>();

            foreach (var orden in ordenes)
            {
                modelos.Add(EntidadAModelo(orden));
            }

            return modelos;
        }

        internal static ORDEN ModeloAEntidad(Dominio.Modelos.OrdenModelo ordenModelo)
        {
            ORDEN entidad = new ORDEN();
            ActualizarEntidad(ref entidad, ordenModelo);
            return entidad;
        }

        internal static void ActualizarEntidad(ref ORDEN entidad, Dominio.Modelos.OrdenModelo modelo)
        {
            entidad.id_orden = modelo.IdOrden;
            entidad.tipo_entrega = modelo.TipoEntrega;
            entidad.descuento_porcentaje = modelo.DescuentoPorcentaje;
            entidad.costo_envio = modelo.CostoEnvio;
            entidad.descripcion = modelo.Descripcion;
            entidad.hora_entrega = modelo.HoraEntrega;
            entidad.id_cliente = modelo.Cliente.Id;
            entidad.id_orden_estado = modelo.Estado?.IdOrdenEstado ?? 1;
            entidad.id_orden_pago_estado = modelo.EstadoPago?.IdOrdenPagoEstado ?? 1;
            entidad.id_direccion = modelo.DireccionEntrega.IdDireccion != Guid.Empty ? modelo.DireccionEntrega.IdDireccion : (Guid?)null;
        }
    }
}
