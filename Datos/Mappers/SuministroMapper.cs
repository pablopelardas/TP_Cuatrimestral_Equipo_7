using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    internal class SuministroMapper
    {
        internal static Dominio.Modelos.SuministroModelo EntidadAModelo(Entidades.SuministroEntidad suministroEntidad, bool incluyeDetalle)
        {
            Dominio.Modelos.SuministroModelo suministro = new Dominio.Modelos.SuministroModelo();

            suministro.IdSuministro = suministroEntidad.id_suministro;
            suministro.Nombre = suministroEntidad.nombre;
            suministro.Proveedor = suministroEntidad.proveedor;
            suministro.Costo = suministroEntidad.costo;
            suministro.Cantidad = suministroEntidad.cantidad;
            suministro.Categoria = CategoriaMapper.EntidadAModelo(suministroEntidad.categoria);

            return suministro;
        }

        internal static Entidades.SuministroEntidad ModeloAEntidad(Dominio.Modelos.SuministroModelo suministroModelo)
        {
            return new Entidades.SuministroEntidad
            {
                id_suministro = suministroModelo.IdSuministro,
                nombre = suministroModelo.Nombre,
                proveedor = suministroModelo.Proveedor,
                costo = suministroModelo.Costo,
                cantidad = suministroModelo.Cantidad,
                categoria = CategoriaMapper.ModeloAEntidad(suministroModelo.Categoria)
            };
        }
    }
}
