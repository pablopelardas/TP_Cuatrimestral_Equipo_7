using Datos.Entidades;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class OrdenRepositorio
    {

        public static string GetSelectOrdenes(string prefix = "")
        {
            return $@"
    ORDENES.id_orden as '{prefix}id_orden',
    ORDENES.fecha as '{prefix}fecha',
    ORDENES.tipo_evento as '{prefix}tipo_evento',
    ORDENES.tipo_entrega as '{prefix}tipo_entrega',
    ORDENES.hora_entrega as '{prefix}hora_entrega',
    ORDENES.descripcion as '{prefix}descripcion',
    (SELECT SUM (DO.cantidad * DO.producto_precio) FROM DETALLE_ORDENES DO WHERE DO.id_orden = ORDENES.id_orden) as '{prefix}subtotal',
    ORDENES.descuento_porcentaje as '{prefix}descuento_porcentaje',
    ORDENES.costo_envio as '{prefix}costo_envio',
    ORDENES.direccion_entrega as '{prefix}direccion_entrega',
    {ContactoRepositorio.GetSelectContactos(prefix + "cliente.")},
    {OrdenEstadoRepositorio.GetSelectOrdenesEstados(prefix + "estado.")},
    {OrdenEstadoPagoRepositorio.GetSelectOrdenesEstadosPago(prefix + "estado_pago.")}
";
        }

        public static string GetJoinOrdenes(string prefix = "")
        {
            return $@"
INNER JOIN CONTACTOS ON ORDENES.ID_CLIENTE = CONTACTOS.ID_CONTACTO
INNER JOIN ORDENES_ESTADOS ON ORDENES.id_orden_estado = ORDENES_ESTADOS.id_orden_estado
INNER JOIN ORDENES_PAGO_ESTADOS ON ORDENES.id_orden_pago_estado = ORDENES_PAGO_ESTADOS.id_orden_pago_estado
";
        }

        private OrdenEntidad GetEntidadFromReader(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            OrdenEntidad entidad = new OrdenEntidad();
            // OBLIGATORIOS
            entidad.id_orden = (int)reader[$"{prefix}id_orden"];
            entidad.fecha = (DateTime)reader[$"{prefix}fecha"];
            entidad.tipo_evento = (string)reader[$"{prefix}tipo_evento"];
            entidad.tipo_entrega = (string)reader[$"{prefix}tipo_entrega"];
            entidad.descripcion = (string)reader[$"{prefix}descripcion"];
            entidad.hora_entrega = reader[$"{prefix}hora_entrega"].ToString();
            entidad.subtotal = (decimal)reader[$"{prefix}subtotal"];

            // OPCIONALES
            entidad.descuento_porcentaje = reader[$"{prefix}descuento_porcentaje"] == DBNull.Value ? 0 : (decimal)reader[$"{prefix}descuento_porcentaje"];
            entidad.costo_envio = reader[$"{prefix}costo_envio"] == DBNull.Value ? 0 : (decimal)reader[$"{prefix}costo_envio"];
            entidad.direccion_entrega = reader[$"{prefix}direccion_entrega"] == DBNull.Value ? "" : (string)reader[$"{prefix}direccion_entrega"];

            // OTRAS ENTIDADES
            // orden.
            entidad.cliente = ContactoRepositorio.GetEntidadFromReader(reader, prefix + "cliente.");
            entidad.estado = OrdenEstadoRepositorio.GetEntidadFromReader(reader, prefix + "estado.");
            entidad.estado_pago = OrdenEstadoPagoRepositorio.GetEntidadFromReader(reader, prefix + "estado_pago.");

            return entidad;
        }

        private void ParametrizarEntidad(OrdenEntidad entidad, AccesoDatos datos)
        {
            datos.SetearParametro("@id_orden", entidad.id_orden);
            datos.SetearParametro("@fecha", entidad.fecha);
            datos.SetearParametro("@tipo_evento", entidad.tipo_evento);
            datos.SetearParametro("@tipo_entrega", entidad.tipo_entrega);
            datos.SetearParametro("@descripcion", entidad.descripcion);
            datos.SetearParametro("@hora_entrega", entidad.hora_entrega);
            datos.SetearParametro("@subtotal", entidad.subtotal);

            datos.SetearParametro("@descuento_porcentaje", entidad.descuento_porcentaje);
            datos.SetearParametro("@costo_envio", entidad.costo_envio);
            datos.SetearParametro("@direccion_entrega", entidad.direccion_entrega);

            datos.SetearParametro("@id_cliente", entidad.cliente.id_contacto);
        }
        public List<Dominio.Modelos.OrdenModelo> Listar()
        {
            List<Dominio.Modelos.OrdenModelo> ordenes = new List<Dominio.Modelos.OrdenModelo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string cmd = $@"
SELECT
{GetSelectOrdenes()}
FROM ORDENES
{GetJoinOrdenes()}
";
                datos.SetearConsulta(cmd);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    ordenes.Add(Mappers.OrdenMapper.EntidadAModelo(GetEntidadFromReader(datos.Lector)));
                }
                return ordenes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public OrdenModelo ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            OrdenModelo orden = new OrdenModelo();
            try
            {
                string cmd = $@"
SELECT
{GetSelectOrdenes()}
FROM ORDENES
{GetJoinOrdenes()}
WHERE ORDENES.id_orden = @id
";
                datos.SetearConsulta(cmd);
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();
                datos.Lector.Read();
                orden = Mappers.OrdenMapper.EntidadAModelo(GetEntidadFromReader(datos.Lector), true);
                return orden;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public void Agregar(OrdenModelo orden)
        {
            throw new NotImplementedException();
        }

        public void Modificar(OrdenModelo orden)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

    }
}
