using Datos.Entidades;
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
    public class RecetaRepositorio
    {
        private Helpers.QueryHelper _QueryHelper = new Helpers.QueryHelper();
        private string CATEGORIA_PREFIX = "cat";

        private CategoriasRepositorio categoriasRepositorio = new CategoriasRepositorio();

        private string RecetaSelect(string prefixTable = "", string prefixColumn = "")
        {
            return $@"
{prefixTable}RECETAS.id_receta as '{prefixColumn}id_producto',
{prefixTable}RECETAS.nombre as '{prefixColumn}nombre',
{prefixTable}RECETAS.descripcion as '{prefixColumn}descripcion',
{prefixTable}RECETAS.precio_personalizado as '{prefixColumn}precio_personalizado',
{categoriasRepositorio.GetSelect(prefixColumn + CATEGORIA_PREFIX)}
";
        }

        private string RecetaJoin(string prefixTable)
        {
            string categoriaAlias = prefixTable + CATEGORIA_PREFIX + "_CATEGORIA";
            return $@"
LEFT JOIN CATEGORIAS AS {categoriaAlias} ON {prefixTable}RECETA.id_categoria = {categoriaAlias}.id_categoria
";
        }

        private Entidades.RecetaEntidad RecetaReader(DataRow row, string prefixColum = "")
        {
            RecetaEntidad entidad = new RecetaEntidad();

            entidad.id_receta = (int)row[$"{prefixColum}id_receta"];
            entidad.nombre = (string)row[$"{prefixColum}nombre"];
            entidad.descripcion = (string)row[$"{prefixColum}descripcion"];
            entidad.precio_personalizado = (decimal)row[$"{prefixColum}precio_personalizado"];
            entidad.categoria = categoriasRepositorio.GetEntity(row, prefixColum + CATEGORIA_PREFIX);

            return entidad;
        }

        public string GetSelect(string prefix = "")
        {
            return _QueryHelper.BuildSelect(prefix, RecetaSelect);
        }

        public string GetJoin(string prefix = "")
        {
            return _QueryHelper.BuildJoin(prefix, RecetaJoin);
        }

        public RecetaEntidad GetEntity(DataRow row, string prefix = "")
        {
            return _QueryHelper.BuildEntityFromReader(row, prefix, RecetaReader);
        }

        private void ParametrizarEntidad(RecetaEntidad entidad, AccesoDatos datos)
        {
            datos.SetearParametro("@id_receta", entidad.id_receta);
            datos.SetearParametro("@nombre", entidad.nombre);
            datos.SetearParametro("@descripcion", entidad.descripcion);
            datos.SetearParametro("@precio_personalizado", entidad.precio_personalizado);
            datos.SetearParametro("@id_categoria", entidad.categoria.id_categoria);
        }

        public List<RecetaModelo> Listar()
        {
            List<RecetaModelo> recetas = new List<RecetaModelo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                SqlCommand cmd = new SqlCommand($@"
SELECT
{GetSelect()}
FROM RECETAS
{GetJoin()}
");

                DataTable response = datos.ExecuteQuery(cmd);
                if (response != null) {
                    foreach (DataRow row in response.Rows)
                    {
                        recetas.Add(Mappers.RecetaMapper.EntidadAModelo(GetEntity(row)));
                    }
                }
                return recetas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RecetaModelo ObtenerPorId(int id)
        {
            RecetaModelo receta = new RecetaModelo();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                SqlCommand cmd = new SqlCommand($@"
SELECT
{GetSelect()}
FROM RECETAS
{GetJoin()}
WHERE RECETAS.id_receta = @id
");

                cmd.Parameters.AddWithValue("@id", id);
                DataTable response = datos.ExecuteQuery(cmd);
                if (response.Rows.Count == 0)
                {
                    return null;
                }
                DataRow row = response.Rows[0];
                receta = Mappers.RecetaMapper.EntidadAModelo(GetEntity(row));
                return receta;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
