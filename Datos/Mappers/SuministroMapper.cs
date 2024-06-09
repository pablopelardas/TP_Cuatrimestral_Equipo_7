using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    internal class SuministroMapper
    {
        internal static Dominio.Modelos.SuministroModelo EntidadAModelo(SUMINISTROS suministroEntidad)
        {
            Dominio.Modelos.SuministroModelo suministro = new Dominio.Modelos.SuministroModelo
            {
                Cantidad = suministroEntidad.cantidad,
                Costo = suministroEntidad.costo,
                IdSuministro = suministroEntidad.id_suministro,
                Nombre = suministroEntidad.nombre,
                Proveedor = suministroEntidad.proveedor
};
            if (suministroEntidad.CATEGORIAS != null)
            {
                suministro.Categoria = CategoriaMapper.EntidadAModelo(suministroEntidad.CATEGORIAS);
            }

            return suministro;
        }

        internal static SUMINISTROS ModeloAEntidad(Dominio.Modelos.SuministroModelo suministroModelo)
        {
            SUMINISTROS entidad = new SUMINISTROS
            {
                cantidad = suministroModelo.Cantidad,
                costo = suministroModelo.Costo,
                id_suministro = suministroModelo.IdSuministro,
                nombre = suministroModelo.Nombre,
                proveedor = suministroModelo.Proveedor
            };
            if (suministroModelo.Categoria != null)
            {
                entidad.CATEGORIAS = CategoriaMapper.ModeloAEntidad(suministroModelo.Categoria);
            }

            return entidad;
        }
    }
}
