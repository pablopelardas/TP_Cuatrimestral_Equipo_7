using Dominio.Modelos;
using System;
using System.Collections.Generic;
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

        private Entidades.EventoEntidad EventoReader(System.Data.SqlClient.SqlDataReader reader, string prefixColumn = "")
        {
            Entidades.EventoEntidad entidad = new Entidades.EventoEntidad();
            entidad.id_evento = (int)reader[$"{prefixColumn}id_evento"];
            entidad.fecha = (DateTime)reader[$"{prefixColumn}fecha"];
            // evento.tipo_evento.
            entidad.tipo_evento = tipoEventoRepositorio.GetEntity(reader, prefixColumn + TIPO_EVENTO_PREFIX);
            entidad.orden = ordenRepositorio.GetEntity(reader, prefixColumn + ORDEN_PREFIX);
            entidad.cliente = contactoRepositorio.GetEntity(reader, prefixColumn + CONTACTO_PREFIX);
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

        public Entidades.EventoEntidad GetEntity(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            return _QueryHelper.BuildEntityFromReader(reader, prefix, EventoReader);
        }

        public List<Dominio.Modelos.EventoModelo> Listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Dominio.Modelos.EventoModelo> eventos = new List<Dominio.Modelos.EventoModelo>();
            try
            {
                string cmd = $@"
SELECT
{GetSelect()}
FROM EVENTOS
{GetJoin()}
";
                datos.SetearConsulta(cmd);
                datos.EjecutarLectura();
                while (datos.Lector.Read()) eventos.Add(Mappers.EventoMapper.EntidadAModelo(GetEntity(datos.Lector)));
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
                string cmd = $@"
SELECT
{GetSelect()}
FROM EVENTOS
{GetJoin()}
WHERE {CONTACTO_PREFIX + "_CONTACTOS"}.ID_CONTACTO = @idUsuario
";
                datos.SetearConsulta(cmd);
                datos.SetearParametro("@idUsuario", idUsuario);
                datos.EjecutarLectura();
                while (datos.Lector.Read()) eventos.Add(Mappers.EventoMapper.EntidadAModelo(GetEntity(datos.Lector)));
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
                string cmd = $@"
SELECT
{GetSelect()}
FROM EVENTOS
{GetJoin()}
WHERE {ORDEN_PREFIX + "_ORDENES"}.ID_ORDEN = @idOrden
";
                datos.SetearConsulta(cmd);
                datos.SetearParametro("@idOrden", idOrden);
                datos.EjecutarLectura();
                datos.Lector.Read();
                return Mappers.EventoMapper.EntidadAModelo(GetEntity(datos.Lector));

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

        public void GuardarEventoDeOrden(EventoModelo eventoModelo, SqlCommand dbCtx = null)
        {

            if (dbCtx == null)
            {
                AccesoDatos datos = new AccesoDatos();
                try
                {
                    string cmd = $@"
INSERT INTO EVENTOS (fecha, id_cliente, id_orden, id_tipo_evento)
VALUES (@ev_fecha, @ev_id_cliente, @ev_id_orden, @ev_id_tipo_evento)
";
                    datos.SetearConsulta(cmd);
                    datos.SetearParametro("@ev_fecha", eventoModelo.Fecha);
                    datos.SetearParametro("@ev_id_cliente", eventoModelo.Cliente.Id);
                    datos.SetearParametro("@ev_id_orden", eventoModelo.Orden.IdOrden);
                    datos.SetearParametro("@ev_id_tipo_evento",eventoModelo.TipoEvento.IdTipoEvento);
                    datos.EjecutarAccion();

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
            else
            {
                string cmd = $@"
INSERT INTO EVENTOS (fecha, id_cliente, id_orden, id_tipo_evento)
VALUES (@ev_fecha, @ev_id_cliente, @ev_id_orden, @ev_id_tipo_evento)
";
                dbCtx.CommandText = cmd;
                dbCtx.Parameters.AddWithValue("@ev_fecha", eventoModelo.Fecha);
                dbCtx.Parameters.AddWithValue("@ev_id_cliente", eventoModelo.Cliente.Id);
                dbCtx.Parameters.AddWithValue("@ev_id_orden", eventoModelo.Orden.IdOrden);
                dbCtx.Parameters.AddWithValue("@ev_id_tipo_evento", eventoModelo.TipoEvento.IdTipoEvento);
                dbCtx.ExecuteNonQuery();
            }
        }

        public void EliminarEventoDeOrden(int idOrden, SqlCommand dbCtx = null)
        {
            if (dbCtx == null)
            {
                AccesoDatos datos = new AccesoDatos();
                try
                {
                    string cmd = $@"DELETE FROM EVENTOS WHERE id_orden = {idOrden}";
                    datos.SetearConsulta(cmd);
                    datos.EjecutarAccion();
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
            else
            {
                string cmd = $@"DELETE FROM EVENTOS WHERE id_orden = {idOrden}";
                dbCtx.CommandText = cmd;
                dbCtx.ExecuteNonQuery();
            }
        }
       
    }
}
