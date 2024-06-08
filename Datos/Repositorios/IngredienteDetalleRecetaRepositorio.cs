using Datos.Helpers;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public Entidades.IngredienteDetalleRecetaEntidad IngredienteDetalleRecetaReader(DataRow row, string prefix = "")
        {
            Entidades.IngredienteDetalleRecetaEntidad entidad = new Entidades.IngredienteDetalleRecetaEntidad();
            entidad.cantidad = (int)row[$"{prefix}cantidad"];

            entidad.ingrediente = ingredienteRepositorio.GetEntity(row, "producto.");
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

        public Entidades.IngredienteDetalleRecetaEntidad GetEntity(DataRow row, string prefix = "")
        {
            return _QueryHelper.BuildEntityFromReader(row, prefix, IngredienteDetalleRecetaReader);
        }

        public List<IngredienteDetalleRecetaModelo> ObtenerDetallePorReceta(int id)
        {
            List<IngredienteDetalleRecetaModelo> ingredientes = new List<IngredienteDetalleRecetaModelo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                SqlCommand cmd = new SqlCommand($@"
SELECT 
{GetSelect()}
FROM DETALLE_RECETAS
{GetJoin()}
WHERE DETALLE_RECETAS.ID_RECETA = @id
                    ");

                cmd.Parameters.AddWithValue("@id", id);
                DataTable response = datos.ExecuteQuery(cmd);

                foreach (DataRow row in response.Rows)
                    ingredientes.Add(Mappers.IngredienteDetalleRecetaMapper.EntidadAModelo(GetEntity(row)));

                return ingredientes;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
