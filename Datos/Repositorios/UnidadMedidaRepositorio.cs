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
        public static string GetSelect(string prefix = "")
        {
            string prefixTable = prefix.Length > 0 ? prefix.Replace(".", "_") + "_" : "";
            prefix = prefix.Length > 0 ? prefix + "." : "";
            return $@"
{prefixTable}UNIDADES_MEDIDA.id_unidad as '{prefix}id_unidad',
{prefixTable}UNIDADES_MEDIDA.nombre as '{prefix}nombre',
{prefixTable}UNIDADES_MEDIDA.abreviatura as '{prefix}abreviatura'
";
        }
        public static UnidadMedidaEntidad GetEntidadFromReader(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            prefix = prefix.Length > 0 ? prefix + "." : "";
            UnidadMedidaEntidad entidad = new UnidadMedidaEntidad();
            entidad.id_unidad = (int)reader[$"{prefix}id_unidad"];
            entidad.nombre = (string)reader[$"{prefix}nombre"];
            entidad.abreviatura = (string)reader[$"{prefix}abreviatura"];
            return entidad;
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
FROM UNIDEADES_MEDIDA
";
            try
            {

                datos.SetearConsulta(cmd);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    unidades.Add(Mappers.UnidadMedidaMapper.EntidadAModelo(GetEntidadFromReader(datos.Lector)));
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
                return Mappers.UnidadMedidaMapper.EntidadAModelo(GetEntidadFromReader(datos.Lector));
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
UPDATE [dbo].[Unidades_Medida]
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
DELETE FROM [dbo].[Unidades_Medida]
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
