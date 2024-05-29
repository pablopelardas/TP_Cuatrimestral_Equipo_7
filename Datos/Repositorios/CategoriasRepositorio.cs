using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class CategoriasRepositorio
    {
        public static Entidades.CategoriaEntidad getEntidadFromReader(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            Entidades.CategoriaEntidad entidad = new Entidades.CategoriaEntidad();
            entidad.id_categoria = (int)reader[$"{prefix}id_categoria"];
            entidad.nombre = (string)reader[$"{prefix}nombre"];
            entidad.tipo = (string)reader[$"{prefix}tipo"];
            return entidad;
        }
    }
}
