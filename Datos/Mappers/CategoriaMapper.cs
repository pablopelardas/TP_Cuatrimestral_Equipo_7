using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    internal class CategoriaMapper
    {
        internal static Dominio.Modelos.CategoriaModelo EntidadAModelo(Datos.Entidades.CategoriaEntidad entidad)
        {
            return new Dominio.Modelos.CategoriaModelo
            {
                Id = entidad.id_categoria,
                Nombre = entidad.nombre,
                Tipo = entidad.tipo
            };
        }

        internal static Datos.Entidades.CategoriaEntidad ModeloAEntidad(Dominio.Modelos.CategoriaModelo modelo)
        {
            return new Datos.Entidades.CategoriaEntidad
            {
                tipo = modelo.Tipo,
                id_categoria = modelo.Id,
                nombre = modelo.Nombre
            };
        }
    }
}
