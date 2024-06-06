using Datos.Entidades;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
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

        private Entidades.SuministroEntidad SuministroReader(SqlDataReader reader, string prefixColum = "")
        {
            SuministroEntidad entidad = new SuministroEntidad();

            entidad.id_suministro = (int)reader[$"{prefixColum}id_suminstro"];
            entidad.nombre = (string)reader[$"{prefixColum}nombre"];
            entidad.proveedor = (string)reader[$"{prefixColum}proveedor"];
            entidad.costo = (decimal)reader[$"{prefixColum}costo"];
            entidad.cantidad = (double)reader[$"{prefixColum}cantidad"];
            entidad.categoria = categoriasRepositorio.GetEntity(reader, prefixColum + CATEGORIA_PREFIX);

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

        public SuministroEntidad GetEntity(SqlDataReader reader, string prefix = "")
        {
            return _QueryHelper.BuildEntityFromReader(reader, prefix, SuministroReader);
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
                string cmd = $@"
SELECT
{GetSelect()}
FROM SUMINISTROS
{GetJoin()}
";
                datos.SetearConsulta(cmd);
                datos.EjecutarLectura();

                while (datos.Lector.Read()) suministros.Add(Mappers.SuministroMapper.EntidadAModelo(GetEntity(datos.Lector), false));

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
                string cmd = $@"
SELECT
{GetSelect()}
FROM SUMINISTROS
{GetJoin()}
WHERE SUMINISTROS.id_suministro = @id
";
                datos.SetearConsulta(cmd);
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();
                datos.Lector.Read();
                suministro = Mappers.SuministroMapper.EntidadAModelo(GetEntity(datos.Lector), true);

                return suministro;
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
