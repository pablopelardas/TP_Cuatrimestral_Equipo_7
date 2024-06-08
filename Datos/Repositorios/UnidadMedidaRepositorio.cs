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
        public UnidadMedidaEntidad UnidadMedidaReader(DataRow row, string prefixColumn = "")
        {
            UnidadMedidaEntidad entidad = new UnidadMedidaEntidad();
            entidad.id_unidad = (int)row[$"{prefixColumn}id_unidad"];
            entidad.nombre = (string)row[$"{prefixColumn}nombre"];
            entidad.abreviatura = (string)row[$"{prefixColumn}abreviatura"];
            return entidad;
        }

        public string GetSelect(string prefix = "")

        {
            return _queryHelper.BuildSelect(prefix, UnidadMedidaSelect);
        }
        public Entidades.UnidadMedidaEntidad GetEntity(DataRow row, string prefix = "")
        {
            return _queryHelper.BuildEntityFromReader(row, prefix, UnidadMedidaReader);
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
            SqlCommand cmd = new SqlCommand($@"
SELECT
{GetSelect()}
FROM UNIDADES_MEDIDA
");
            try
            {

                DataTable response = datos.ExecuteQuery(cmd);
                if (response == null) {
                    return unidades;
                }
                foreach (DataRow row in response.Rows)
                {
                    unidades.Add(Mappers.UnidadMedidaMapper.EntidadAModelo(GetEntity(row)));
                }
                return unidades;
            }
            catch (Exception ex)            
            {
                throw ex;
            }
        }

        public UnidadMedidaModelo ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            SqlCommand cmd = new SqlCommand($@"
SELECT
{GetSelect()}
FROM [dbo].[UNIDADES_MEDIDA]
WHERE id_unidad = @id
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
                return Mappers.UnidadMedidaMapper.EntidadAModelo(GetEntity(row));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Agregar(UnidadMedidaModelo unidad)
        {
            AccesoDatos datos = new AccesoDatos();
            SqlCommand cmd = new SqlCommand($@"
INSERT INTO [dbo].[UNIDADES_MEDIDA]
(id_unidad, nombre, abreviatura)
VALUES
(@id_unidad, @nombre, @abreviatura)
");
            try
            {
                UnidadMedidaEntidad entidad = Mappers.UnidadMedidaMapper.ModeloAEntidad(unidad);

                cmd.Parameters.AddWithValue("@id_unidad", entidad.id_unidad);
                cmd.Parameters.AddWithValue("@nombre", entidad.nombre);
                cmd.Parameters.AddWithValue("@abreviatura", entidad.abreviatura);

                datos.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Modificar(UnidadMedidaModelo unidad)
        {
            AccesoDatos datos = new AccesoDatos();
            SqlCommand cmd = new SqlCommand($@"
UPDATE [dbo].[UNIDADES_MEDIDA]
SET
id_unidad = @id_unidad,
nombre = @nombre,
abreviatura = @abreviatura
WHERE id_unidad = @id_unidad");
            try
            {
                UnidadMedidaEntidad entidad = Mappers.UnidadMedidaMapper.ModeloAEntidad(unidad);

                cmd.Parameters.AddWithValue("@id_unidad", entidad.id_unidad);
                cmd.Parameters.AddWithValue("@nombre", entidad.nombre);
                cmd.Parameters.AddWithValue("@abreviatura", entidad.abreviatura);

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
DELETE FROM [dbo].[UNIDADES_MEDIDA]
WHERE id_unidad = @id_unidad");
            try
            {
                cmd.Parameters.AddWithValue("@id_unidad", id);
                datos.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
