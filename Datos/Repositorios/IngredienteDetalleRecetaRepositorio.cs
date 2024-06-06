using Datos.Helpers;
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
        private QueryHelper _QueryHelper = new QueryHelper();
        private string INGREDIENTE_PREFIX = "ing";
        private IngredienteRepositorio ingredienteRepositorio = new IngredienteRepositorio();

        public string IngredienteDetalleRecetaSelect(string prefixTable, string prefixColumn)
        {
            return $@"
    {prefixTable}DETALLE_RECETA.cantidad AS '{prefixColumn}cantidad',
    {ingredienteRepositorio.GetSelect(prefixColumn + INGREDIENTE_PREFIX)}
";
        }

        public string IngredienteDetalleRecetaJoin(string prefixTable)
        {
            string aliasProducto = prefixTable + INGREDIENTE_PREFIX + "_INGREDIENTES";
            return $@"
INNER JOIN INGREDIENTES as {aliasProducto} ON {prefixTable}ID_INGREDIENTE = {aliasProducto}.ID_INGREDIENTE
{ingredienteRepositorio.GetJoin(prefixTable + INGREDIENTE_PREFIX)}
";
        }

        public Entidades.IngredienteDetalleRecetaEntidad IngredienteDetalleRecetaReader(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            Entidades.IngredienteDetalleRecetaEntidad entidad = new Entidades.IngredienteDetalleRecetaEntidad();
            entidad.cantidad = (int)reader[$"{prefix}cantidad"];

            entidad.ingrediente = ingredienteRepositorio.GetEntity(reader, "producto.");
            return entidad;
        }

        public string GetSelect(string prefix = "")
        {
            return _QueryHelper.BuildSelect(prefix, IngredienteDetalleRecetaSelect);
        }

        public string GetJoin(string prefix = "")
        {
            return _QueryHelper.BuildJoin(prefix, IngredienteDetalleRecetaJoin);
        }

        public Entidades.IngredienteDetalleRecetaEntidad GetEntity(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            return _QueryHelper.BuildEntityFromReader(reader, prefix, IngredienteDetalleRecetaReader);
        }

        public List<IngredienteDetalleRecetaModelo> ObtenerDetallePorReceta(int id)
        {
            List<IngredienteDetalleRecetaModelo> ingredientes = new List<IngredienteDetalleRecetaModelo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string cmd = $@"
SELECT 
{GetSelect()}
FROM DETALLE_RECETAS
{GetJoin()}
WHERE DETALLE_RECETAS.ID_RECETA = @id
                    ";
                datos.SetearConsulta(cmd);
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();
                while (datos.Lector.Read()) ingredientes.Add(Mappers.IngredienteDetalleRecetaMapper.EntidadAModelo(GetEntity(datos.Lector)));
                return ingredientes;
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
