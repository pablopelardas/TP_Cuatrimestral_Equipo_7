using Datos.Entidades;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class UnidadMedidaRepositorio
    {
        private Helpers.QueryHelper _queryHelper = new Helpers.QueryHelper();

        public string UnidadMedidaSelect(string prefixTable, string prefixColumn)
        {
            return $@"
{prefixTable}UNIDADES_MEDIDA.id_unidad as '{prefixColumn}id_unidad',
{prefixTable}UNIDADES_MEDIDA.nombre as '{prefixColumn}nombre',
{prefixTable}UNIDADES_MEDIDA.abreviatura as '{prefixColumn}abreviatura'
";
        }
        public UnidadMedidaEntidad UnidadMedidaReader(System.Data.SqlClient.SqlDataReader reader, string prefixColumn = "")
        {
            UnidadMedidaEntidad entidad = new UnidadMedidaEntidad();
            entidad.id_unidad = (int)reader[$"{prefixColumn}id_unidad"];
            entidad.nombre = (string)reader[$"{prefixColumn}nombre"];
            entidad.abreviatura = (string)reader[$"{prefixColumn}abreviatura"];
            return entidad;
        }

        public string GetSelect(string prefix = "")

        {
            return _queryHelper.BuildSelect(prefix, UnidadMedidaSelect);
        }
        public Entidades.UnidadMedidaEntidad GetEntity(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            return _queryHelper.BuildEntityFromReader(reader, prefix, UnidadMedidaReader);
        }

        private void ParametrizarEntidad(UnidadMedidaEntidad entidad, AccesoDatos datos)
        {
            datos.SetearParametro("@id_ingrediente", entidad.id_unidad);
            datos.SetearParametro("@nombre", entidad.nombre);
            datos.SetearParametro("@cantidad", entidad.abreviatura);
        }

        public List<UnidadMedidaModelo> Listar()
        {
            List<UnidadMedidaModelo> unidades = new List<UnidadMedidaModelo>();
            AccesoDatos datos = new AccesoDatos();
            string cmd = $@"
SELECT
{GetSelect()}
FROM UNIDADES_MEDIDA
";
            try
            {

                datos.SetearConsulta(cmd);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    unidades.Add(Mappers.UnidadMedidaMapper.EntidadAModelo(GetEntity(datos.Lector)));
                }
                return unidades;
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

        public UnidadMedidaModelo ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            string cmd = $@"
SELECT
{GetSelect()}
FROM [dbo].[UNIDADES_MEDIDA]
WHERE id_unidad = @id
";
            try
            {
                datos.SetearConsulta(cmd);
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();
                datos.Lector.Read();
                return Mappers.UnidadMedidaMapper.EntidadAModelo(GetEntity(datos.Lector));
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

        public void Agregar(UnidadMedidaModelo unidad)
        {
            AccesoDatos datos = new AccesoDatos();
            string cmd = $@"
INSERT INTO [dbo].[UNIDADES_MEDIDA]
(id_unidad, nombre, abreviatura)
VALUES
(@id_unidad, @nombre, @abreviatura)
";
            try
            {
                UnidadMedidaEntidad entidad = Mappers.UnidadMedidaMapper.ModeloAEntidad(unidad);
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
        public void Modificar(UnidadMedidaModelo unidad)
        {
            AccesoDatos datos = new AccesoDatos();
            string cmd = $@"
UPDATE [dbo].[UNIDADES_MEDIDA]
SET
id_unidad = @id_unidad,
nombre = @nombre,
abreviatura = @abreviatura
WHERE id_unidad = @id_unidad";
            try
            {
                UnidadMedidaEntidad entidad = Mappers.UnidadMedidaMapper.ModeloAEntidad(unidad);
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
DELETE FROM [dbo].[UNIDADES_MEDIDA]
WHERE id_unidad = @id_unidad";
            try
            {
                datos.SetearConsulta(cmd);
                datos.SetearParametro("@id_unidad", id);
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
