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

        private Entidades.RecetaEntidad RecetaReader(SqlDataReader reader, string prefixColum = "")
        {
            RecetaEntidad entidad = new RecetaEntidad();

            entidad.id_receta = (int)reader[$"{prefixColum}id_receta"];
            entidad.nombre = (string)reader[$"{prefixColum}nombre"];
            entidad.descripcion = (string)reader[$"{prefixColum}descripcion"];
            entidad.precio_personalizado = (decimal)reader[$"{prefixColum}precio_personalizado"];
            entidad.categoria = categoriasRepositorio.GetEntity(reader, prefixColum + CATEGORIA_PREFIX);

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

        public RecetaEntidad GetEntity(SqlDataReader reader, string prefix = "")
        {
            return _QueryHelper.BuildEntityFromReader(reader, prefix, RecetaReader);
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
                string cmd = $@"
SELECT
{GetSelect()}
FROM RECETAS
{GetJoin()}
";
                datos.SetearConsulta(cmd);
                datos.EjecutarLectura();

                while (datos.Lector.Read()) recetas.Add(Mappers.RecetaMapper.EntidadAModelo(GetEntity(datos.Lector), false));
                
                return recetas;
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

        public RecetaModelo ObtenerPorId(int id)
        {
            RecetaModelo receta = new RecetaModelo();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string cmd = $@"
SELECT
{GetSelect()}
FROM RECETAS
{GetJoin()}
WHERE RECETAS.id_receta = @id
";
                datos.SetearConsulta(cmd);
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();
                datos.Lector.Read();
                receta= Mappers.RecetaMapper.EntidadAModelo(GetEntity(datos.Lector), true);
                
                return receta;
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
