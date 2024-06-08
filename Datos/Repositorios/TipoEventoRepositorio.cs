using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        private Entidades.TipoEventoEntidad TipoEventoReader(DataRow row, string prefixColumn = "")
        {
            Entidades.TipoEventoEntidad entidad = new Entidades.TipoEventoEntidad();
            entidad.id_tipo_evento = (int)row[$"{prefixColumn}id_tipo_evento"];
            entidad.nombre = (string)row[$"{prefixColumn}nombre"];
            return entidad;
        }

        public string GetSelect(string prefix = "")
        {
            return _QueryHelper.BuildSelect(prefix, TipoEventoSelect);
        }
        public Entidades.TipoEventoEntidad GetEntity(DataRow row, string prefix = "")
        {
            return _QueryHelper.BuildEntityFromReader(row, prefix, TipoEventoReader);
        }

        public List<Dominio.Modelos.TipoEventoModelo> Listar()
        {
            AccesoDatos datos = new AccesoDatos();
            SqlCommand query = new SqlCommand($@"
SELECT
{GetSelect()}
FROM TIPOS_EVENTOS");
            try
            {
                List<Dominio.Modelos.TipoEventoModelo> lista = new List<Dominio.Modelos.TipoEventoModelo>();
                DataTable response = datos.ExecuteQuery(query);

                foreach (DataRow row in response.Rows)
                {
                    lista.Add(Mappers.TipoEventoMapper.EntidadAModelo(GetEntity(row)));
                }
                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
