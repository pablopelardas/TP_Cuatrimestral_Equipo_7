using Datos.Entidades;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
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

        private Entidades.OrdenEntidad OrdenReader(DataRow row, string prefixColumn = "")
        {
            OrdenEntidad entidad = new OrdenEntidad();
            // OBLIGATORIOS
            entidad.id_orden = (int)row[$"{prefixColumn}id_orden"];
            entidad.tipo_entrega = (string)row[$"{prefixColumn}tipo_entrega"];
            entidad.descripcion = (string)row[$"{prefixColumn}descripcion"];
            entidad.hora_entrega = row[$"{prefixColumn}hora_entrega"].ToString();

            // OPCIONALES
            entidad.descuento_porcentaje = row[$"{prefixColumn}descuento_porcentaje"] == DBNull.Value ? 0 : (decimal)row[$"{prefixColumn}descuento_porcentaje"];
            entidad.costo_envio = row[$"{prefixColumn}costo_envio"] == DBNull.Value ? 0 : (decimal)row[$"{prefixColumn}costo_envio"];
            entidad.direccion_entrega = row[$"{prefixColumn}direccion_entrega"] == DBNull.Value ? "" : (string)row[$"{prefixColumn}direccion_entrega"];

            // OTRAS ENTIDADES
            // orden.
            entidad.cliente = contactoRepositorio.GetEntity(row, prefixColumn + CLIENTE_PREFIX);
            entidad.estado = ordenEstadoRepositorio.GetEntity(row, prefixColumn + ESTADO_PREFIX);
            entidad.estado_pago = ordenEstadoPagoRepositorio.GetEntity(row, prefixColumn + ESTADO_PAGO_PREFIX);

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

        public OrdenEntidad GetEntity(DataRow row, string prefix = "")
        {
            return _QueryHelper.BuildEntityFromReader(row, prefix, OrdenReader);
        }
        public List<Dominio.Modelos.OrdenModelo> Listar()
        {
            List<Dominio.Modelos.OrdenModelo> ordenes = new List<Dominio.Modelos.OrdenModelo>();
            AccesoDatos datos = new AccesoDatos();
            SqlCommand cmd = new SqlCommand($@"
SELECT
{GetSelect()}
FROM ORDENES
{GetJoin()}
");
            try
            {

                DataTable reader = datos.ExecuteQuery(cmd);


                foreach (DataRow row in reader.Rows)
                {
                    ordenes.Add(Mappers.OrdenMapper.EntidadAModelo(GetEntity(row)));
                }

                //while (reader.Read())
                //{
                //    ordenes.Add(Mappers.OrdenMapper.EntidadAModelo(GetEntity(reader)));
                //}
                return ordenes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OrdenModelo ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            OrdenModelo orden = new OrdenModelo();
            try
            {
                SqlCommand cmd = new SqlCommand($@"
SELECT
{GetSelect()}
FROM ORDENES
{GetJoin()}
WHERE ORDENES.id_orden = @id
");
            
                cmd.Parameters.AddWithValue("@id", id);
                DataTable response = datos.ExecuteQuery(cmd);
                if (response.Rows.Count > 0)
                {
                    orden = Mappers.OrdenMapper.EntidadAModelo(GetEntity(response.Rows[0]), incluyeDetalle: true);
                }
                return orden;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private SqlCommand InsertOrderCommand(Entidades.OrdenEntidad orden)
        {
            SqlCommand cmd = new SqlCommand($@"INSERT INTO ORDENES (tipo_entrega, hora_entrega, descripcion, descuento_porcentaje, costo_envio, direccion_entrega, id_cliente)
VALUES (
    @tipo_entrega,
    @hora_entrega,
    @descripcion,
    @descuento_porcentaje,
    @costo_envio,
    @direccion_entrega,
    @id_cliente
)
SELECT CAST(SCOPE_IDENTITY() AS INT)");
            
            cmd.Parameters.AddWithValue("@tipo_entrega", orden.tipo_entrega);
            cmd.Parameters.AddWithValue("@hora_entrega", orden.hora_entrega);
            cmd.Parameters.AddWithValue("@descripcion", orden.descripcion);
            cmd.Parameters.AddWithValue("@descuento_porcentaje", orden.descuento_porcentaje);
            cmd.Parameters.AddWithValue("@costo_envio", orden.costo_envio);
            cmd.Parameters.AddWithValue("@direccion_entrega", orden.direccion_entrega);
            cmd.Parameters.AddWithValue("@id_cliente", orden.cliente.id_contacto);
            return cmd;
        }

       private SqlCommand UpdateOrderCommand(OrdenEntidad orden)
        {
            SqlCommand cmd = new SqlCommand($@"UPDATE ORDENES
SET
    tipo_entrega = @tipo_entrega,
    hora_entrega = @hora_entrega,
    descripcion = @descripcion,
    descuento_porcentaje = @descuento_porcentaje,
    costo_envio = @costo_envio,
    direccion_entrega = @direccion_entrega,
    id_cliente = @id_cliente
WHERE id_orden = @id_orden");
            cmd.Parameters.AddWithValue("@tipo_entrega", orden.tipo_entrega);
            cmd.Parameters.AddWithValue("@hora_entrega", orden.hora_entrega);
            cmd.Parameters.AddWithValue("@descripcion", orden.descripcion);
            cmd.Parameters.AddWithValue("@descuento_porcentaje", orden.descuento_porcentaje);
            cmd.Parameters.AddWithValue("@costo_envio", orden.costo_envio);
            cmd.Parameters.AddWithValue("@direccion_entrega", orden.direccion_entrega);
            cmd.Parameters.AddWithValue("@id_cliente", orden.cliente.id_contacto);
            cmd.Parameters.AddWithValue("@id_orden", orden.id_orden);
            return cmd;
        }

        public void GuardarOrdenTx(OrdenModelo orden)
        {
            List<Entidades.ProductoDetalleOrdenEntidad> listaDetalle = orden.DetalleProductos.Select(x => Mappers.ProductoDetalleOrdenMapper.ModeloAEntidad(x)).ToList();
            ProductoDetalleOrdenRepositorio productoDetalleOrdenRepositorio = new ProductoDetalleOrdenRepositorio();
            EventoRepositorio eventoRepositorio = new EventoRepositorio();
            AccesoDatos datos = new AccesoDatos();

            AccesoDatos accesoDatos = new AccesoDatos();
            List<SqlCommand> commands = new List<SqlCommand>();

            if (orden.IdOrden != 0)
            {
                commands.Add(UpdateOrderCommand(Mappers.OrdenMapper.ModeloAEntidad(orden)));
                commands.Add(productoDetalleOrdenRepositorio.EliminarDetalleCommand(orden.IdOrden));
                commands.Add(eventoRepositorio.EliminarEventoDeOrdenCommand(orden.IdOrden));
            }
            else
            {
                orden.IdOrden = accesoDatos.ExecuteScalar(InsertOrderCommand(Mappers.OrdenMapper.ModeloAEntidad(orden)));
            }

            // Agregar detalles
            foreach (ProductoDetalleOrdenEntidad detalle in listaDetalle)
            {
                commands.Add(productoDetalleOrdenRepositorio.GuardarDetalleCommand(detalle, orden.IdOrden));
            }

            // Agregar evento
            if (orden.Evento != null)
            {
                orden.Evento = new EventoModelo
                {
                    Cliente = orden.Cliente,
                    Orden = orden,
                    Fecha = orden.Evento.Fecha,
                    TipoEvento = orden.Evento.TipoEvento
                };
                commands.Add(eventoRepositorio.GuardarEventoDeOrdenCommand(Mappers.EventoMapper.ModeloAEntidad(orden.Evento)));
            }

            try
            {
                DataTable result = accesoDatos.Transaction(commands);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

    }
}
