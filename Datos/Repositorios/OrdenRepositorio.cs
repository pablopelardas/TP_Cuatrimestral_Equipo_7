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
        private Helpers.QueryHelper _QueryHelper = new Helpers.QueryHelper();

        private string CLIENTE_PREFIX = "cli";
        private string ESTADO_PREFIX = "est";
        private string ESTADO_PAGO_PREFIX = "estp";

        private ContactoRepositorio contactoRepositorio = new ContactoRepositorio();
        private OrdenEstadoRepositorio ordenEstadoRepositorio = new OrdenEstadoRepositorio();
        private OrdenEstadoPagoRepositorio ordenEstadoPagoRepositorio = new OrdenEstadoPagoRepositorio();

        private string OrdenSelect(string prefixTable, string prefixColumn)
        {
            return $@"
{prefixTable}ORDENES.id_orden as '{prefixColumn}id_orden',
{prefixTable}ORDENES.fecha as '{prefixColumn}fecha',
{prefixTable}ORDENES.tipo_evento as '{prefixColumn}tipo_evento',
{prefixTable}ORDENES.tipo_entrega as '{prefixColumn}tipo_entrega',
{prefixTable}ORDENES.hora_entrega as '{prefixColumn}hora_entrega',
{prefixTable}ORDENES.descripcion as '{prefixColumn}descripcion',
(SELECT SUM (DO.cantidad * DO.producto_precio) FROM DETALLE_ORDENES DO WHERE DO.id_orden = ORDENES.id_orden) as '{prefixColumn}subtotal',
{prefixTable}ORDENES.descuento_porcentaje as '{prefixColumn}descuento_porcentaje',
{prefixTable}ORDENES.costo_envio as '{prefixColumn}costo_envio',
{prefixTable}ORDENES.direccion_entrega as '{prefixColumn}direccion_entrega',
{contactoRepositorio.GetSelect(prefixColumn + CLIENTE_PREFIX)},
{ordenEstadoRepositorio.GetSelect(prefixColumn + ESTADO_PREFIX)},
{ordenEstadoPagoRepositorio.GetSelect(prefixColumn + ESTADO_PAGO_PREFIX)}
";
        }

        private string OrdenJoin(string prefixTable)
        {
            string clienteAlias = CLIENTE_PREFIX + "_CONTACTOS";
            string estadoAlias = ESTADO_PREFIX + "_ORDENES_ESTADOS";
            string estadoPagoAlias = ESTADO_PAGO_PREFIX + "_ORDENES_PAGO_ESTADOS";

            return $@"
INNER JOIN CONTACTOS AS {clienteAlias} ON {prefixTable}ORDENES.ID_CLIENTE = {clienteAlias}.ID_CONTACTO
INNER JOIN ORDENES_ESTADOS as {estadoAlias} ON {prefixTable}ORDENES.id_orden_estado = {estadoAlias}.id_orden_estado
INNER JOIN ORDENES_PAGO_ESTADOS as {estadoPagoAlias} ON {prefixTable}ORDENES.id_orden_pago_estado = {estadoPagoAlias}.id_orden_pago_estado
";
        }

        private Entidades.OrdenEntidad OrdenReader(SqlDataReader reader, string prefixColumn = "")
        {
            OrdenEntidad entidad = new OrdenEntidad();
            // OBLIGATORIOS
            entidad.id_orden = (int)reader[$"{prefixColumn}id_orden"];
            entidad.fecha = (DateTime)reader[$"{prefixColumn}fecha"];
            entidad.tipo_evento = (string)reader[$"{prefixColumn}tipo_evento"];
            entidad.tipo_entrega = (string)reader[$"{prefixColumn}tipo_entrega"];
            entidad.descripcion = (string)reader[$"{prefixColumn}descripcion"];
            entidad.hora_entrega = reader[$"{prefixColumn}hora_entrega"].ToString();
            entidad.subtotal = (decimal)reader[$"{prefixColumn}subtotal"];

            // OPCIONALES
            entidad.descuento_porcentaje = reader[$"{prefixColumn}descuento_porcentaje"] == DBNull.Value ? 0 : (decimal)reader[$"{prefixColumn}descuento_porcentaje"];
            entidad.costo_envio = reader[$"{prefixColumn}costo_envio"] == DBNull.Value ? 0 : (decimal)reader[$"{prefixColumn}costo_envio"];
            entidad.direccion_entrega = reader[$"{prefixColumn}direccion_entrega"] == DBNull.Value ? "" : (string)reader[$"{prefixColumn}direccion_entrega"];

            // OTRAS ENTIDADES
            // orden.
            entidad.cliente = contactoRepositorio.GetEntity(reader, prefixColumn + CLIENTE_PREFIX);
            entidad.estado = ordenEstadoRepositorio.GetEntity(reader, prefixColumn + ESTADO_PREFIX);
            entidad.estado_pago = ordenEstadoPagoRepositorio.GetEntity(reader, prefixColumn + ESTADO_PAGO_PREFIX);

            return entidad;
        }

            public string GetSelect(string prefix = "")
        {
            return _QueryHelper.BuildSelect(prefix, OrdenSelect);
        }

        public string GetJoin(string prefix = "")
        {
            return _QueryHelper.BuildJoin(prefix, OrdenJoin);
        }

        private OrdenEntidad GetEntity(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            return _QueryHelper.BuildEntityFromReader(reader, prefix, OrdenReader);
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
{GetSelect()}
FROM ORDENES
{GetJoin()}
";
                datos.SetearConsulta(cmd);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    ordenes.Add(Mappers.OrdenMapper.EntidadAModelo(GetEntity(datos.Lector)));
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
{GetSelect()}
FROM ORDENES
{GetJoin()}
WHERE ORDENES.id_orden = @id
";
                datos.SetearConsulta(cmd);
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();
                datos.Lector.Read();
                orden = Mappers.OrdenMapper.EntidadAModelo(GetEntity(datos.Lector), true);
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
