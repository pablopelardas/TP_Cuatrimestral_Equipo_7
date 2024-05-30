using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    internal class IngredienteDetalleRecetaRepositorio
    {
        public static string GetSelectIngredienteDetalleReceta(string prefix = "")
        {
            return $@"
    DETALLE_RECETAS.cantidad AS '{prefix}cantidad',
    {IngredienteRepositorio.GetSelectIngredientes("ingrediente.")}
";
        }

        public static string GetJoinIngredienteDetalleReceta()
        {
            return $@"
INNER JOIN PRODUCTOS ON DETALLE_RECETAS.ID_INGREDIENTE = INGREDIENTE.ID_INGREDIENTE
{IngredienteRepositorio.GetJoinIngredientes()}
";
        }

        public static Entidades.ProductoDetalleOrdenEntidad GetEntidadFromReader(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            Entidades.ProductoDetalleOrdenEntidad entidad = new Entidades.ProductoDetalleOrdenEntidad();
            entidad.cantidad = (int)reader[$"{prefix}cantidad"];
            entidad.producto_porciones = (int)reader[$"{prefix}producto_porciones"];
            entidad.producto_costo = (decimal)reader[$"{prefix}producto_costo"];
            entidad.producto_precio = (decimal)reader[$"{prefix}producto_precio"];

            entidad.producto = ProductoRepositorio.GetEntidadFromReader(reader, "producto.");
            return entidad;
        }

        public List<ProductoDetalleOrdenModelo> ObtenerDetallePorReceta(int id)
        {
            List<ProductoDetalleOrdenModelo> productos = new List<ProductoDetalleOrdenModelo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string cmd = $@"
SELECT 
{GetSelectIngredienteDetalleReceta()}
FROM DETALLE_RECETAS
{GetJoinIngredienteDetalleReceta()}
WHERE DETALLE_RECETAS.ID_RECETA = @id
                    ";
                datos.SetearConsulta(cmd);
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Entidades.IngredienteDetalleRecetaEntidad entidad = new Entidades.IngredienteDetalleRecetaEntidad();
                    entidad.ingrediente = IngredienteRepositorio.GetEntidadFromReader(datos.Lector, "ingrediente.");
                    entidad.cantidad = (int)datos.Lector["cantidad"];
                    //productos.Add(Mappers.ProductoDetalleOrdenMapper.EntidadAModelo(entidad));
                }
                return productos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

    }
}
