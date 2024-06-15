using Datos.EF;

namespace Datos.Mappers
{
    public class ItemDetalleProductoMapper
    {
        internal static Dominio.Modelos.ItemDetalleProductoModelo EntidadAModelo(DETALLEPRODUCTO entidad)
        {
            Dominio.Modelos.ItemDetalleProductoModelo modelo = new Dominio.Modelos.ItemDetalleProductoModelo()
            {
                Cantidad = entidad.cantidad,
            };
            
            if (entidad.RECETA != null)
            {
                modelo.Receta = RecetaMapper.EntidadAModelo(entidad.RECETA);
            }
            
            if (entidad.SUMINISTRO != null)
            {
                modelo.Suministro = SuministroMapper.EntidadAModelo(entidad.SUMINISTRO);
            }
            return modelo;
        }
    }
}