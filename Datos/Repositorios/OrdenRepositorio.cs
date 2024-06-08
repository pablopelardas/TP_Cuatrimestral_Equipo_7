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
        private string EVENTO_PREFIX = "eve";

        private ContactoRepositorio contactoRepositorio = new ContactoRepositorio();
        private OrdenEstadoRepositorio ordenEstadoRepositorio = new OrdenEstadoRepositorio();
        private OrdenEstadoPagoRepositorio ordenEstadoPagoRepositorio = new OrdenEstadoPagoRepositorio();

        private string OrdenSelect(string prefixTable, string prefixColumn)
        {
            return $@"
{prefixTable}ORDENES.id_orden as '{prefixColumn}id_orden',
{prefixTable}ORDENES.tipo_entrega as '{prefixColumn}tipo_entrega',
{prefixTable}ORDENES.hora_entrega as '{prefixColumn}hora_entrega',
{prefixTable}ORDENES.descripcion as '{prefixColumn}descripcion',
(SELECT SUM (DO.cantidad * DO.producto_precio) FROM DETALLE_ORDENES DO WHERE DO.id_orden = {prefixTable}ORDENES.id_orden) as '{prefixColumn}subtotal',
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
            string clienteAlias = prefixTable + CLIENTE_PREFIX + "_CONTACTOS";
            string estadoAlias = prefixTable + ESTADO_PREFIX + "_ORDENES_ESTADOS";
            string estadoPagoAlias = prefixTable + ESTADO_PAGO_PREFIX + "_ORDENES_PAGO_ESTADOS";
            string eventoAlias = prefixTable + EVENTO_PREFIX + "_EVENTOS";

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
            entidad.tipo_entrega = (string)reader[$"{prefixColumn}tipo_entrega"];
            entidad.descripcion = (string)reader[$"{prefixColumn}descripcion"];
            entidad.hora_entrega = reader[$"{prefixColumn}hora_entrega"].ToString();

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

        public OrdenEntidad GetEntity(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            return _QueryHelper.BuildEntityFromReader(reader, prefix, OrdenReader);
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

        public void GuardarOrdenTx(OrdenModelo orden)
        {
            Entidades.OrdenEntidad entidad = Mappers.OrdenMapper.ModeloAEntidad(orden);
            List<Entidades.ProductoDetalleOrdenEntidad> listaDetalle = orden.DetalleProductos.Select(x => Mappers.ProductoDetalleOrdenMapper.ModeloAEntidad(x)).ToList();
            ProductoDetalleOrdenRepositorio productoDetalleOrdenRepositorio = new ProductoDetalleOrdenRepositorio();

            using (SqlConnection conexion = new SqlConnection("Data Source=localhost,15000;Initial Catalog=tp-cuatrimestral-grupo-7;User Id=sa;Password=Pablo2846!;TrustServerCertificate=True"))
            {
                conexion.Open();
                using (SqlCommand comando = conexion.CreateCommand())
                {
                    using (SqlTransaction transaccion = conexion.BeginTransaction())
                    {
                        comando.Transaction = transaccion;
                        try
                        {
                            string cmd = orden.IdOrden == 0
                                ? $@"
INSERT INTO ORDENES (tipo_entrega, hora_entrega, descripcion, descuento_porcentaje, costo_envio, direccion_entrega, id_cliente)
VALUES (@tipo_entrega, @hora_entrega, @descripcion, @descuento_porcentaje, @costo_envio, @direccion_entrega, @id_cliente)
SELECT CAST(SCOPE_IDENTITY() AS INT) "
                                : $@"
UPDATE ORDENES
SET
    tipo_entrega = @tipo_entrega,
    hora_entrega = @hora_entrega,
    descripcion = @descripcion,
    descuento_porcentaje = @descuento_porcentaje,
    costo_envio = @costo_envio,
    direccion_entrega = @direccion_entrega,
    id_cliente = @id_cliente
WHERE id_orden = @id_orden
";
                            comando.CommandText = cmd;
                            
                            // set parameter

                            comando.Parameters.AddWithValue("@tipo_entrega", entidad.tipo_entrega);
                            comando.Parameters.AddWithValue("@hora_entrega", entidad.hora_entrega);
                            comando.Parameters.AddWithValue("@descripcion", entidad.descripcion);
                            comando.Parameters.AddWithValue("@descuento_porcentaje", entidad.descuento_porcentaje);
                            comando.Parameters.AddWithValue("@costo_envio", entidad.costo_envio);
                            comando.Parameters.AddWithValue("@direccion_entrega", entidad.direccion_entrega);
                            comando.Parameters.AddWithValue("@id_cliente", entidad.cliente.id_contacto);
                            int ordenID;
                            if (orden.IdOrden != 0)
                            {
                                comando.Parameters.AddWithValue("@id_orden", entidad.id_orden);
                                ordenID = entidad.id_orden;
                                comando.ExecuteNonQuery();
                            }
                            else
                            {
                                ordenID = (int)comando.ExecuteScalar();
                            }

                            // Guardar detalle
                            productoDetalleOrdenRepositorio.EliminarDetalle(ordenID, comando);
                            productoDetalleOrdenRepositorio.AgregarListaDetalle(ordenID, listaDetalle, comando);
                            // Guardar evento
                            if (orden.Evento != null)
                            {
                                EventoModelo evento = new EventoModelo
                                {
                                    Cliente = orden.Cliente,
                                    Orden = orden,
                                    Fecha = orden.Evento.Fecha,
                                    TipoEvento = orden.Evento.TipoEvento
                                };

                                EventoRepositorio eventoRepositorio = new EventoRepositorio();
                                eventoRepositorio.EliminarEventoDeOrden(ordenID, comando);
                                eventoRepositorio.GuardarEventoDeOrden(evento, comando);
                            }
                            transaccion.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaccion.Rollback();
                            throw ex;
                        }
                    }
                }
            }

        }



        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

    }
}
