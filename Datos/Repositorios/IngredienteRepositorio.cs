using Datos.Entidades;
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
    public class IngredienteRepositorio
    {
        private Helpers.QueryHelper _queryHelper = new Helpers.QueryHelper();

        private static string UNIDAD_PREFIX = "uni";
        
        private UnidadMedidaRepositorio unidadMedidaRepositorio = new UnidadMedidaRepositorio();
        public string IngredienteSelect(string prefixTable = "", string prefixColumn = "")
        {
            return $@"
{prefixTable}INGREDIENTES.id_ingrediente as '{prefixColumn}id_ingrediente',
{prefixTable}INGREDIENTES.nombre as '{prefixColumn}nombre',
{prefixTable}INGREDIENTES.cantidad as '{prefixColumn}cantidad',
{prefixTable}INGREDIENTES.costo as '{prefixColumn}costo',
{prefixTable}INGREDIENTES.proveedor as '{prefixColumn}proveedor',
{unidadMedidaRepositorio.GetSelect(prefixColumn + UNIDAD_PREFIX)}
";
        }

        public static string IngredienteJoin(string prefix = "")
        {
            string aliasUnidades = prefix + UNIDAD_PREFIX + "_UNIDADES_MEDIDA";
            return $@"
INNER JOIN UNIDADES_MEDIDA as {aliasUnidades} ON {prefix}INGREDIENTES.id_unidad = {aliasUnidades}.id_unidad
";
        }

        public IngredienteEntidad IngredienteReader(DataRow row, string prefixColumn = "")
        {
            IngredienteEntidad entidad = new IngredienteEntidad();

            entidad.id_ingrediente = (int)row[$"{prefixColumn}id_ingrediente"];
            entidad.nombre = (string)row[$"{prefixColumn}nombre"];
            entidad.cantidad = (double)row[$"{prefixColumn}cantidad"];
            entidad.costo = (decimal)row[$"{prefixColumn}costo"];
            entidad.proveedor = (string)row[$"{prefixColumn}proveedor"];

            entidad.unidad = unidadMedidaRepositorio.GetEntity(row, prefixColumn + UNIDAD_PREFIX);
            return entidad;
        }

        public string GetSelect(string prefix = "")
        {
            return _queryHelper.BuildSelect(prefix, IngredienteSelect);
        }

        public string GetJoin(string prefix = "")
        {
            return _queryHelper.BuildJoin(prefix, IngredienteJoin);
        }

        public Entidades.IngredienteEntidad GetEntity(DataRow row, string prefix = "")
        {
            return _queryHelper.BuildEntityFromReader(row, prefix, IngredienteReader);
        }

        private void ParametrizarEntidad(IngredienteEntidad entidad, AccesoDatos datos)
        {
            datos.SetearParametro("@id_ingrediente", entidad.id_ingrediente);
            datos.SetearParametro("@nombre", entidad.nombre);
            datos.SetearParametro("@cantidad", entidad.cantidad);
            datos.SetearParametro("@id_unidad", entidad.unidad.id_unidad);
            datos.SetearParametro("@costo", entidad.costo);
            datos.SetearParametro("@proveedor", entidad.proveedor);
        }

            public List<IngredienteModelo> Listar()
        {
            List<IngredienteModelo> ingredientes = new List<IngredienteModelo>();
            AccesoDatos datos = new AccesoDatos();
            SqlCommand cmd = new SqlCommand($@"
SELECT
{GetSelect()}
FROM dbo.[INGREDIENTES]
{GetJoin()}
");
            try
            {

                DataTable response = datos.ExecuteQuery(cmd);

                foreach (DataRow row in response.Rows)
                {
                    ingredientes.Add(Mappers.IngredienteMapper.EntidadAModelo(GetEntity(row)));
                }

                return ingredientes;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IngredienteModelo ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            SqlCommand cmd = new SqlCommand($@"
Select
{GetSelect()}
FROM [dbo].[INGREDIENTES]
{GetJoin()}
WHERE id_ingrediente = @id
");
            try
            {
                cmd.Parameters.AddWithValue("@id", id);

                DataTable response = datos.ExecuteQuery(cmd);

                if (response.Rows.Count == 0)
                {
                    return null;
                }
                DataRow row = response.Rows[0];
                return Mappers.IngredienteMapper.EntidadAModelo(GetEntity(row));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Agregar(IngredienteModelo ingrediente)
        {
            AccesoDatos datos = new AccesoDatos();
            SqlCommand cmd = new SqlCommand($@"
INSERT INTO [dbo].[INGREDIENTES]
(nombre, cantidad, id_unidad, costo, proveedor)
VALUES
(@nombre, @cantidad, @id_unidad, @costo, @proveedor)
");
            try
            {

                IngredienteEntidad entidad = Mappers.IngredienteMapper.ModeloAEntidad(ingrediente);
                cmd.Parameters.AddWithValue("@nombre", entidad.nombre);
                cmd.Parameters.AddWithValue("@cantidad", entidad.cantidad);
                cmd.Parameters.AddWithValue("@id_unidad", entidad.unidad.id_unidad);
                cmd.Parameters.AddWithValue("@costo", entidad.costo);
                cmd.Parameters.AddWithValue("@proveedor", entidad.proveedor);

                datos.ExecuteNonQuery(cmd);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Modificar(IngredienteModelo ingrediente)
        {
            AccesoDatos datos = new AccesoDatos();
            SqlCommand cmd = new SqlCommand($@"
UPDATE [dbo].[INGREDIENTES]
SET
nombre = @nombre,
cantidad = @cantidad,
id_unidad = @id_unidad,
costo = @costo,
proveedor = @proveedor
WHERE id_ingrediente = @id_ingrediente
");
            try
            {
                IngredienteEntidad entidad = Mappers.IngredienteMapper.ModeloAEntidad(ingrediente);
                
                cmd.Parameters.AddWithValue("@id_ingrediente", entidad.id_ingrediente);
                cmd.Parameters.AddWithValue("@nombre", entidad.nombre);
                cmd.Parameters.AddWithValue("@cantidad", entidad.cantidad);
                cmd.Parameters.AddWithValue("@id_unidad", entidad.unidad.id_unidad);
                cmd.Parameters.AddWithValue("@costo", entidad.costo);
                cmd.Parameters.AddWithValue("@proveedor", entidad.proveedor);

                datos.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            SqlCommand cmd = new SqlCommand($@"
DELETE FROM [dbo].[INGREDIENTES]
WHERE id_ingrediente = @id_ingrediente");
            try
            {
                cmd.Parameters.AddWithValue("@id_ingrediente", id);
                datos.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
