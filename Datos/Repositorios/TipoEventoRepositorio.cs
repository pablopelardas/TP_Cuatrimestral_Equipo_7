using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class TipoEventoRepositorio
    {
        private Helpers.QueryHelper _QueryHelper = new Helpers.QueryHelper();

        private string TipoEventoSelect(string prefixTable = "", string prefixColumn = "")
        {
            return $@"
{prefixTable}TIPOS_EVENTOS.id_tipo_evento as '{prefixColumn}id_tipo_evento',
{prefixTable}TIPOS_EVENTOS.nombre as '{prefixColumn}nombre'";
        }

        private Entidades.TipoEventoEntidad TipoEventoReader(System.Data.SqlClient.SqlDataReader reader, string prefixColumn = "")
        {
            Entidades.TipoEventoEntidad entidad = new Entidades.TipoEventoEntidad();
            entidad.id_tipo_evento = (int)reader[$"{prefixColumn}id_tipo_evento"];
            entidad.nombre = (string)reader[$"{prefixColumn}nombre"];
            return entidad;
        }

        public string GetSelect(string prefix = "")
        {
            return _QueryHelper.BuildSelect(prefix, TipoEventoSelect);
        }
        public Entidades.TipoEventoEntidad GetEntity(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            return _QueryHelper.BuildEntityFromReader(reader, prefix, TipoEventoReader);
        }

        public List<Dominio.Modelos.TipoEventoModelo> Listar()
        {
            string query = $@"
SELECT
{GetSelect()}
FROM TIPOS_EVENTOS";
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(query);
                datos.EjecutarLectura();
                List<Dominio.Modelos.TipoEventoModelo> lista = new List<Dominio.Modelos.TipoEventoModelo>();
                while (datos.Lector.Read())
                {
                    lista.Add(Mappers.TipoEventoMapper.EntidadAModelo(GetEntity(datos.Lector)));
                }
                return lista;
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
    }
}
