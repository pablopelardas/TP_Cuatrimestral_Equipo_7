using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class CategoriasRepositorio
    {
        private Helpers.QueryHelper _queryHelper = new Helpers.QueryHelper();

        private string CategoriaSelect(string prefixTable, string prefixColumn)
        {
            return $@"
{prefixTable}CATEGORIAS.id_categoria as '{prefixColumn}id_categoria',
{prefixTable}CATEGORIAS.nombre as '{prefixColumn}nombre',
{prefixTable}CATEGORIAS.tipo as '{prefixColumn}tipo'";
        }

        private Entidades.CategoriaEntidad CategoriaReader(System.Data.SqlClient.SqlDataReader reader, string prefixColumn = "")
        {
            Entidades.CategoriaEntidad entidad = new Entidades.CategoriaEntidad();
            entidad.id_categoria = (int)reader[$"{prefixColumn}id_categoria"];
            entidad.nombre = (string)reader[$"{prefixColumn}nombre"];
            entidad.tipo = (string)reader[$"{prefixColumn}tipo"];
            return entidad;
        }

        public string GetSelect(string prefix = "")
        
        {
            return _queryHelper.BuildSelect(prefix, CategoriaSelect);
        }
        public Entidades.CategoriaEntidad GetEntity(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            return _queryHelper.BuildEntityFromReader(reader, prefix, CategoriaReader);
        }
    }
}
