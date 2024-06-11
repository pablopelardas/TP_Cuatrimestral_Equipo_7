using Datos.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    internal class SuministroMapper
    {
        internal static Dominio.Modelos.SuministroModelo EntidadAModelo(SUMINISTRO suministroEntidad)
        {
            Dominio.Modelos.SuministroModelo suministro = new Dominio.Modelos.SuministroModelo
            {
                Cantidad = suministroEntidad.cantidad,
                Costo = suministroEntidad.costo,
                IdSuministro = suministroEntidad.id_suministro,
                Nombre = suministroEntidad.nombre,
                Proveedor = suministroEntidad.proveedor
};
            if (suministroEntidad.CATEGORIA != null)
            {
                suministro.Categoria = CategoriaMapper.EntidadAModelo(suministroEntidad.CATEGORIA);
            }

            return suministro;
        }

        internal static SUMINISTRO ModeloAEntidad(Dominio.Modelos.SuministroModelo suministroModelo)
        {
            SUMINISTRO entidad = new SUMINISTRO
            {
                cantidad = suministroModelo.Cantidad,
                costo = suministroModelo.Costo,
                id_suministro = suministroModelo.IdSuministro,
                nombre = suministroModelo.Nombre,
                proveedor = suministroModelo.Proveedor
            };
            if (suministroModelo.Categoria != null)
            {
                entidad.id_categoria = suministroModelo.Categoria.Id;
            }

            return entidad;
        }
    }
}
