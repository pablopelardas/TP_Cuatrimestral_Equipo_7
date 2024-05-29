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
        public static Dominio.Modelos.OrdenModelo EntidadAModelo(Entidades.OrdenEntidad ordenEntidad)
        {
            Repositorios.ContactoRepositorio contactoRepositorio = new Repositorios.ContactoRepositorio();
            Repositorios.OrdenRepositorio ordenRepositorio = new Repositorios.OrdenRepositorio();
            Repositorios.ProductoDetalleOrdenRepositorio productoDetalleOrdenRepositorio = new Repositorios.ProductoDetalleOrdenRepositorio();

            return new Dominio.Modelos.OrdenModelo
            {
                IdOrden = ordenEntidad.id_orden,
                Fecha = ordenEntidad.fecha,
                TipoEvento = ordenEntidad.tipo_evento,
                TipoEntrega = ordenEntidad.tipo_entrega,
                DescuentoPorcentaje = ordenEntidad.descuento_porcentaje,
                CostoEnvio = ordenEntidad.costo_envio,
                Descripcion = ordenEntidad.descripcion,

                Cliente = contactoRepositorio.ObtenerPorId(ordenEntidad.id_cliente),
                Subtotal = ordenRepositorio.CalcularTotal(ordenEntidad.id_orden),
                DetalleProductos = productoDetalleOrdenRepositorio.ObtenerDetallePorOrden(ordenEntidad.id_orden),
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
                costo_envio = ordenModelo.CostoEnvio,
                descripcion = ordenModelo.Descripcion,
            };
        }
    }
}
