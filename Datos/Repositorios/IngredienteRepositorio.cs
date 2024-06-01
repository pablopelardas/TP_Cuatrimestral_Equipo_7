using Datos.Entidades;
using Datos.Helpers;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
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

        public IngredienteEntidad IngredienteReader(System.Data.SqlClient.SqlDataReader reader, string prefixColumn = "")
        {
            IngredienteEntidad entidad = new IngredienteEntidad();

            entidad.id_ingrediente = (int)reader[$"{prefixColumn}id_ingrediente"];
            entidad.nombre = (string)reader[$"{prefixColumn}nombre"];
            entidad.cantidad = (double)reader[$"{prefixColumn}cantidad"];
            entidad.costo = (decimal)reader[$"{prefixColumn}costo"];
            entidad.proveedor = (string)reader[$"{prefixColumn}proveedor"];

            entidad.unidad = unidadMedidaRepositorio.GetEntity(reader, prefixColumn + UNIDAD_PREFIX);
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

        public Entidades.IngredienteEntidad GetEntity(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            return _queryHelper.BuildEntityFromReader(reader, prefix, IngredienteReader);
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
            string cmd = $@"
SELECT
{GetSelect()}
FROM dbo.[INGREDIENTES]
{GetJoin()}
";
            try
            {
                datos.SetearConsulta(cmd);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    ingredientes.Add(Mappers.IngredienteMapper.EntidadAModelo(GetEntity(datos.Lector)));
                }
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

        public IngredienteModelo ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            string cmd = $@"
Select
{GetSelect()}
FROM [dbo].[INGREDIENTES]
{GetJoin()}
WHERE id_ingrediente = @id
";
            try
            {
                datos.SetearConsulta(cmd);
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();
                datos.Lector.Read();
                return Mappers.IngredienteMapper.EntidadAModelo(GetEntity(datos.Lector));
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

        public void Agregar(IngredienteModelo ingrediente)
        {
            AccesoDatos datos = new AccesoDatos();
            string cmd = $@"
INSERT INTO [dbo].[INGREDIENTES]
(id_ingrediente, nombre, cantidad, id_unidad, costo, proveedor)
VALUES
(@id_ingrediente, @nombre, @cantidad, @id_unidad, @costo, @proveedor)
";
            try
            {
                IngredienteEntidad entidad = Mappers.IngredienteMapper.ModeloAEntidad(ingrediente);
                datos.SetearConsulta(cmd);
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
        public void Modificar(IngredienteModelo ingrediente)
        {
            AccesoDatos datos = new AccesoDatos();
            string cmd = $@"
UPDATE [dbo].[INGREDIENTES]
SET
id_ingrediente = @id_ingrediente,
nombre = @nombre,
cantidad = @cantidad,
id_unidad = @id_unidad,
costo = @costo,
proveedor = @proveedor
WHERE id_ingrediente = @id_ingrediente
";
            try
            {
                IngredienteEntidad entidad = Mappers.IngredienteMapper.ModeloAEntidad(ingrediente);
                datos.SetearConsulta(cmd);
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
        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            string cmd = $@"
DELETE FROM [dbo].[INGREDIENTES]
WHERE id_ingrediente = @id_ingrediente";
            try
            {
                datos.SetearConsulta(cmd);
                datos.SetearParametro("@id_ingrediente", id);
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
    }
}
