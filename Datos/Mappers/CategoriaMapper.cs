using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    internal class CategoriaMapper
    {
        internal static Dominio.Modelos.CategoriaModelo EntidadAModelo(CATEGORIAS entidad)
        {
            return new Dominio.Modelos.CategoriaModelo
            {
                Id = entidad.id_categoria,
                Nombre = entidad.nombre,
                Tipo = entidad.tipo
            };
        }

        internal static CATEGORIAS ModeloAEntidad(Dominio.Modelos.CategoriaModelo modelo)
        {
            return new CATEGORIAS
            {
                tipo = modelo.Tipo,
                id_categoria = modelo.Id,
                nombre = modelo.Nombre
            };
        }
    }
}
