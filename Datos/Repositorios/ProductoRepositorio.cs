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
    public class ProductoRepositorio
    {
        private Helpers.QueryHelper _QueryHelper = new Helpers.QueryHelper();
        private string CATEGORIA_PREFIX = "cat";

        private CategoriasRepositorio categoriasRepositorio = new CategoriasRepositorio();

        private string ProductoSelect(string prefixTable = "", string prefixColumn = "")
        {
            return $@"
{prefixTable}PRODUCTOS.id_producto as '{prefixColumn}id_producto',
{prefixTable}PRODUCTOS.nombre as '{prefixColumn}nombre',
{prefixTable}PRODUCTOS.descripcion as '{prefixColumn}descripcion',
{prefixTable}PRODUCTOS.porciones as '{prefixColumn}porciones',
{prefixTable}PRODUCTOS.horas_trabajo as '{prefixColumn}horas_trabajo',
{prefixTable}PRODUCTOS.tipo_precio as '{prefixColumn}tipo_precio',
{prefixTable}PRODUCTOS.valor_precio as '{prefixColumn}valor_precio',
{categoriasRepositorio.GetSelect(prefixColumn + CATEGORIA_PREFIX)}
";
        }

        private string ProductoJoin(string prefixTable = "")
        {
            string aliasCategorias = prefixTable + CATEGORIA_PREFIX + "_CATEGORIAS";
            return $@"
INNER JOIN CATEGORIAS as {aliasCategorias} ON {prefixTable}PRODUCTOS.ID_CATEGORIA = {aliasCategorias}.ID_CATEGORIA
";
        }

        private Entidades.ProductoEntidad ProductoReader(DataRow row, string prefixColumn = "")
        {
            Entidades.ProductoEntidad entidad = new Entidades.ProductoEntidad();
            entidad.id_producto = (int)row[$"{prefixColumn}id_producto"];
            entidad.nombre = (string)row[$"{prefixColumn}nombre"];
            entidad.descripcion = (string)row[$"{prefixColumn}descripcion"];
            entidad.porciones = (int)row[$"{prefixColumn}porciones"];
            entidad.horas_trabajo = (decimal)row[$"{prefixColumn}horas_trabajo"];
            entidad.tipo_precio = (string)row[$"{prefixColumn}tipo_precio"];
            entidad.valor_precio = (decimal)row[$"{prefixColumn}valor_precio"];
            // producto.categoria.
            entidad.categoria = categoriasRepositorio.GetEntity(row, prefixColumn + CATEGORIA_PREFIX);
            return entidad;
        }
        public string GetSelect(string prefix = "")
        {
            return _QueryHelper.BuildSelect(prefix, ProductoSelect);
        }

        public string GetJoin(string prefix = "")
        {
            return _QueryHelper.BuildJoin(prefix, ProductoJoin);
        }

        public Entidades.ProductoEntidad GetEntity(DataRow row, string prefix = "")
        {
            return _QueryHelper.BuildEntityFromReader(row, prefix, ProductoReader);
        }

        private void ParametrizarEntidad(Entidades.ProductoEntidad entidad, AccesoDatos datos)
        {
            datos.SetearParametro("@id_producto", entidad.id_producto);
            datos.SetearParametro("@nombre", entidad.nombre);
            datos.SetearParametro("@descripcion", entidad.descripcion);
            datos.SetearParametro("@porciones", entidad .porciones);
            datos.SetearParametro("@horas_trabajo", entidad.horas_trabajo);
            datos.SetearParametro("@tipo_precio", entidad.tipo_precio);
            datos.SetearParametro("@valor_precio", entidad.valor_precio);
            datos.SetearParametro("@id_categoria", entidad.categoria.id_categoria);
        }

        public List<Dominio.Modelos.ProductoModelo> Listar()
        {
            List<Dominio.Modelos.ProductoModelo> productos = new List<Dominio.Modelos.ProductoModelo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                SqlCommand cmd = new SqlCommand($@"
SELECT
{GetSelect()}
FROM PRODUCTOS
{GetJoin()}
");
                DataTable response = datos.ExecuteQuery(cmd);
                foreach (DataRow row in response.Rows)
                {
                    productos.Add(Mappers.ProductoMapper.EntidadAModelo(GetEntity(row)));
                }
                return productos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dominio.Modelos.ProductoModelo ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                SqlCommand cmd = new SqlCommand($@"
SELECT
{GetSelect()}
FROM PRODUCTOS
{GetJoin()}
WHERE id_producto = @id
");

                cmd.Parameters.AddWithValue("@id", id);
                DataTable response = datos.ExecuteQuery(cmd);
                if (response.Rows.Count == 0)
                {
                    return null;
                }
                DataRow row = response.Rows[0];
                return Mappers.ProductoMapper.EntidadAModelo(GetEntity(row));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Agregar(Dominio.Modelos.ProductoModelo producto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                Entidades.ProductoEntidad entidad = Mappers.ProductoMapper.ModeloAEntidad(producto);
                datos.SetearConsulta("INSERT INTO [dbo].[PRODUCTOS] (id_producto, nombre, descripcion, prociones, horas_trabajo, tipo_precio, valor_precio, id_categoria) VALUES (@id_producto, @nombre, @descripcion, @porciones, @horas_trabajo, @tipo_precio, @valor_precio, @id_categoria)");
                ParametrizarEntidad(entidad, datos);
                datos.EjecutarAccion();
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

        public void Modificar(ProductoModelo orden)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }
    }
}
