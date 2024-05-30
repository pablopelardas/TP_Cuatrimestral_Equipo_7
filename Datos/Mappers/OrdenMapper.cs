using Datos.Repositorios;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    public class OrdenMapper
    {
        public static Dominio.Modelos.OrdenModelo EntidadAModelo(Entidades.OrdenEntidad ordenEntidad, bool incluyeDetalle = false)
        {
            Repositorios.ProductoDetalleOrdenRepositorio productoDetalleOrdenRepositorio = new Repositorios.ProductoDetalleOrdenRepositorio();

            Dominio.Modelos.OrdenModelo ordenModelo = new Dominio.Modelos.OrdenModelo
            {
                // ATRIBUTOS DE ENTIDAD
                IdOrden = ordenEntidad.id_orden,
                Fecha = ordenEntidad.fecha,
                TipoEvento = ordenEntidad.tipo_evento,
                TipoEntrega = ordenEntidad.tipo_entrega,
                DescuentoPorcentaje = ordenEntidad.descuento_porcentaje,
                CostoEnvio = ordenEntidad.costo_envio,
                Descripcion = ordenEntidad.descripcion,
                Subtotal = ordenEntidad.subtotal,
                Cliente = ContactoMapper.EntidadAModelo(ordenEntidad.cliente),
            };

            // ATRIBUTOS DE OTRAS ENTIDADES
            if (incluyeDetalle)
            {
                ordenModelo.DetalleProductos = productoDetalleOrdenRepositorio.ObtenerDetallePorOrden(ordenEntidad.id_orden);
            }

            return ordenModelo;

            
        }

        public static Entidades.OrdenEntidad ModeloAEntidad(Dominio.Modelos.OrdenModelo ordenModelo)
        {
            return new Entidades.OrdenEntidad
            {
                // ATRIBUTOS DE MODELO
                cliente = ContactoMapper.ModeloAEntidad(ordenModelo.Cliente),
                id_orden = ordenModelo.IdOrden,
                fecha = ordenModelo.Fecha,
                tipo_evento = ordenModelo.TipoEvento,
                tipo_entrega = ordenModelo.TipoEntrega,
                descuento_porcentaje = ordenModelo.DescuentoPorcentaje,
                costo_envio = ordenModelo.CostoEnvio,
                descripcion = ordenModelo.Descripcion,
            };
        }
    }
}
