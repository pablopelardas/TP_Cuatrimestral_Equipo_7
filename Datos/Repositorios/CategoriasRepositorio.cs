using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class CategoriasRepositorio
    {

        public static string GetSelectCategorias(string prefix = "")
        {
            string prefixTable = prefix.Length > 0 ? prefix.Replace(".", "_") + '_' : "";
            prefix = prefix.Length > 0 ? prefix + "." : "";
            return $@"
{prefixTable}CATEGORIAS.id_categoria as '{prefix}id_categoria',
{prefixTable}CATEGORIAS.nombre as '{prefix}nombre',
{prefixTable}CATEGORIAS.tipo as '{prefix}tipo'
";
        }
        public static Entidades.CategoriaEntidad GetEntidadFromReader(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            prefix = prefix.Length > 0 ? prefix + "." : "";
            Entidades.CategoriaEntidad entidad = new Entidades.CategoriaEntidad();
            // producto.categoria.id_categoria
            entidad.id_categoria = (int)reader[$"{prefix}id_categoria"];
            entidad.nombre = (string)reader[$"{prefix}nombre"];
            entidad.tipo = (string)reader[$"{prefix}tipo"];
            return entidad;
        }
    }
}
