using Datos.Entidades;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class EventoRepositorio
    {
        private Helpers.QueryHelper _QueryHelper = new Helpers.QueryHelper();
        private TipoEventoRepositorio tipoEventoRepositorio = new TipoEventoRepositorio();
        private OrdenRepositorio ordenRepositorio = new OrdenRepositorio();
        private ContactoRepositorio contactoRepositorio = new ContactoRepositorio();
        private string TIPO_EVENTO_PREFIX = "tpe";
        private string ORDEN_PREFIX = "ord";
        private string CONTACTO_PREFIX = "con";

        private string EventoSelect(string prefixTable = "", string prefixColumn = "")
        {
            return $@"
{prefixTable}EVENTOS.id_evento as '{prefixColumn}id_evento',
{prefixTable}EVENTOS.fecha as '{prefixColumn}fecha',
{prefixTable}EVENTOS.id_cliente as '{prefixColumn}id_cliente',
{prefixTable}EVENTOS.id_orden as '{prefixColumn}id_orden',
{contactoRepositorio.GetSelect(prefixColumn + CONTACTO_PREFIX)},
{tipoEventoRepositorio.GetSelect(prefixColumn + TIPO_EVENTO_PREFIX)},
{ordenRepositorio.GetSelect(prefixColumn + ORDEN_PREFIX)}
";
        }
        private string EventoJoin(string prefixTable = "")
        {
            string aliasTipoEvento = prefixTable + TIPO_EVENTO_PREFIX + "_TIPOS_EVENTOS";
            string aliasOrden = prefixTable + ORDEN_PREFIX + "_ORDENES";
            string aliasContacto = prefixTable + CONTACTO_PREFIX + "_CONTACTOS";
            return $@"
INNER JOIN TIPOS_EVENTOS as {aliasTipoEvento} ON {prefixTable}EVENTOS.ID_TIPO_EVENTO = {aliasTipoEvento}.ID_TIPO_EVENTO
INNER JOIN ORDENES as {aliasOrden} ON {prefixTable}EVENTOS.ID_ORDEN = {aliasOrden}.ID_ORDEN
INNER JOIN CONTACTOS as {aliasContacto} ON {prefixTable}EVENTOS.ID_CLIENTE = {aliasContacto}.ID_CONTACTO
{ordenRepositorio.GetJoin(ORDEN_PREFIX)}
";
        }

        private Entidades.EventoEntidad EventoReader(DataRow row, string prefixColumn = "")
        {
            Entidades.EventoEntidad entidad = new Entidades.EventoEntidad();
            entidad.id_evento = (int)row[$"{prefixColumn}id_evento"];
            entidad.fecha = (DateTime)row[$"{prefixColumn}fecha"];
            // evento.tipo_evento.
            entidad.tipo_evento = tipoEventoRepositorio.GetEntity(row, prefixColumn + TIPO_EVENTO_PREFIX);
            entidad.orden = ordenRepositorio.GetEntity(row, prefixColumn + ORDEN_PREFIX);
            entidad.cliente = contactoRepositorio.GetEntity(row, prefixColumn + CONTACTO_PREFIX);
            return entidad;
        }

        public string GetSelect(string prefix = "")
        {
            return _QueryHelper.BuildSelect(prefix, EventoSelect);
        }

        public string GetJoin(string prefix = "")
        {
            return _QueryHelper.BuildJoin(prefix, EventoJoin);
        }

        public Entidades.EventoEntidad GetEntity(DataRow row, string prefix = "")
        {
            return _QueryHelper.BuildEntityFromReader(row, prefix, EventoReader);
        }

        public List<Dominio.Modelos.EventoModelo> Listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Dominio.Modelos.EventoModelo> eventos = new List<Dominio.Modelos.EventoModelo>();
            try
            {
                SqlCommand cmd = new SqlCommand($@"
SELECT
{GetSelect()}
FROM EVENTOS
{GetJoin()}
");
                DataTable response = datos.ExecuteQuery(cmd);
                foreach (DataRow row in response.Rows)
                {
                    eventos.Add(Mappers.EventoMapper.EntidadAModelo(GetEntity(row)));
                }
                return eventos;

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

        public List<Dominio.Modelos.EventoModelo> ObtenerPorUsuario(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Dominio.Modelos.EventoModelo> eventos = new List<Dominio.Modelos.EventoModelo>();
            try
            {
                SqlCommand cmd = new SqlCommand($@"
SELECT
{GetSelect()}
FROM EVENTOS
{GetJoin()}
WHERE {CONTACTO_PREFIX + "_CONTACTOS"}.ID_CONTACTO = @idUsuario
");
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                DataTable response = datos.ExecuteQuery(cmd);
                foreach (DataRow row in response.Rows)
                {
                    eventos.Add(Mappers.EventoMapper.EntidadAModelo(GetEntity(row)));
                }
                return eventos;

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

        public Dominio.Modelos.EventoModelo ObtenerPorOrden(int idOrden)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Dominio.Modelos.EventoModelo> eventos = new List<Dominio.Modelos.EventoModelo>();
            try
            {
                SqlCommand cmd = new SqlCommand($@"
SELECT
{GetSelect()}
FROM EVENTOS
{GetJoin()}
WHERE {ORDEN_PREFIX + "_ORDENES"}.ID_ORDEN = @idOrden
");
                cmd.Parameters.AddWithValue("@idOrden", idOrden);
                DataTable response = datos.ExecuteQuery(cmd);
                if (response.Rows.Count == 0)
                {
                    return null;
                }
                DataRow row = response.Rows[0];
                return Mappers.EventoMapper.EntidadAModelo(GetEntity(row));

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public SqlCommand GuardarEventoDeOrdenCommand(EventoEntidad eventoModelo)
        {
            SqlCommand cmd = new SqlCommand($@"
INSERT INTO EVENTOS (fecha, id_cliente, id_orden, id_tipo_evento)
VALUES (@fecha, @id_cliente, @id_orden, @id_tipo_evento)
");
            cmd.Parameters.AddWithValue("@fecha", eventoModelo.fecha);
            cmd.Parameters.AddWithValue("@id_cliente", eventoModelo.cliente.id_contacto);
            cmd.Parameters.AddWithValue("@id_orden", eventoModelo.orden.id_orden);
            cmd.Parameters.AddWithValue("@id_tipo_evento", eventoModelo.tipo_evento.id_tipo_evento);
            return cmd;
        }

        public SqlCommand EliminarEventoDeOrdenCommand(int idOrden)
        {
            SqlCommand cmd = new SqlCommand($@"DELETE FROM EVENTOS WHERE id_orden = {idOrden}");
            return cmd;
        }
    }
}
