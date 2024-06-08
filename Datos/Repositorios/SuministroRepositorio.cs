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
    public class SuministroRepositorio
    {
        private Helpers.QueryHelper _QueryHelper = new Helpers.QueryHelper();
        private string CATEGORIA_PREFIX = "cat";

        private CategoriasRepositorio categoriasRepositorio = new CategoriasRepositorio();

        private string SuministroSelect(string prefixTable = "", string prefixColumn = "")
        {
            return $@"
{prefixTable}SUMINISTROS.id_suministro as '{prefixColumn}id_producto',
{prefixTable}SUMINISTROS.nombre as '{prefixColumn}nombre',
{prefixTable}SUMINISTROS.proveedor as '{prefixColumn}proveedor',
{prefixTable}SUMINISTROS.costo as '{prefixColumn}costo',
{prefixTable}SUMINISTROS.cantidad as '{prefixColumn}cantidad',
{categoriasRepositorio.GetSelect(prefixColumn + CATEGORIA_PREFIX)}
";
        }

        private string SuministroJoin(string prefixTable)
        {
            string categoriaAlias = prefixTable + CATEGORIA_PREFIX + "_CATEGORIA";
            return $@"
LEFT JOIN CATEGORIAS AS {categoriaAlias} ON {prefixTable}RECETA.id_categoria = {categoriaAlias}.id_categoria
";
        }

        private Entidades.SuministroEntidad SuministroReader(DataRow row, string prefixColum = "")
        {
            SuministroEntidad entidad = new SuministroEntidad();

            entidad.id_suministro = (int)row[$"{prefixColum}id_suminstro"];
            entidad.nombre = (string)row[$"{prefixColum}nombre"];
            entidad.proveedor = (string)row[$"{prefixColum}proveedor"];
            entidad.costo = (decimal)row[$"{prefixColum}costo"];
            entidad.cantidad = (double)row[$"{prefixColum}cantidad"];
            entidad.categoria = categoriasRepositorio.GetEntity(row, prefixColum + CATEGORIA_PREFIX);

            return entidad;
        }

        public string GetSelect(string prefix = "")
        {
            return _QueryHelper.BuildSelect(prefix, SuministroSelect);
        }

        public string GetJoin(string prefix = "")
        {
            return _QueryHelper.BuildJoin(prefix, SuministroJoin);
        }

        public SuministroEntidad GetEntity(DataRow row, string prefix = "")
        {
            return _QueryHelper.BuildEntityFromReader(row, prefix, SuministroReader);
        }

        private void ParametrizarEntidad(SuministroEntidad entidad, AccesoDatos datos)
        {
            datos.SetearParametro("@id_suministro", entidad.id_suministro);
            datos.SetearParametro("@nombre", entidad.nombre);
            datos.SetearParametro("@proveedor", entidad.proveedor);
            datos.SetearParametro("@costo", entidad.costo);
            datos.SetearParametro("@cantidad", entidad.cantidad);
            datos.SetearParametro("@id_categoria", entidad.categoria.id_categoria);
        }

        public List<SuministroModelo> Listar()
        {
            List<SuministroModelo> suministros = new List<SuministroModelo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                SqlCommand cmd = new SqlCommand($@"
SELECT
{GetSelect()}
FROM SUMINISTROS
{GetJoin()}
");

                DataTable response = datos.ExecuteQuery(cmd);
                if (response.Rows.Count == 0) return suministros;
                
                foreach (DataRow row in response.Rows)
                {
                    suministros.Add(Mappers.SuministroMapper.EntidadAModelo(GetEntity(row)));
                }

                return suministros;
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

        public SuministroModelo ObtenerPorId(int id)
        {
            SuministroModelo suministro = new SuministroModelo();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                SqlCommand cmd = new SqlCommand($@"
SELECT
{GetSelect()}
FROM SUMINISTROS
{GetJoin()}
WHERE SUMINISTROS.id_suministro = @id
");
                cmd.Parameters.AddWithValue("@id", id);

                DataTable response = datos.ExecuteQuery(cmd);
                if (response.Rows.Count == 0) return null;

                DataRow row = response.Rows[0];
                suministro = Mappers.SuministroMapper.EntidadAModelo(GetEntity(row));

                return suministro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
