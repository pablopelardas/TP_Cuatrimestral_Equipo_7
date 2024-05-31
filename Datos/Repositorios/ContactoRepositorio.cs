using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class ContactoRepositorio
    {
        private Helpers.QueryHelper _QueryHelper = new Helpers.QueryHelper();

        private string ContactoSelect(string prefixTable, string prefixColumn)
        {
            return $@"
{prefixTable}CONTACTOS.id_contacto as '{prefixColumn}id_contacto',
{prefixTable}CONTACTOS.nombre_apellido as '{prefixColumn}nombre_apellido',
{prefixTable}CONTACTOS.tipo as '{prefixColumn}tipo',
{prefixTable}CONTACTOS.correo as '{prefixColumn}correo',
{prefixTable}CONTACTOS.telefono as '{prefixColumn}telefono',
{prefixTable}CONTACTOS.fuente as '{prefixColumn}fuente',
{prefixTable}CONTACTOS.direccion as '{prefixColumn}direccion',
{prefixTable}CONTACTOS.producto_que_provee as '{prefixColumn}producto_que_provee',
{prefixTable}CONTACTOS.desea_recibir_correos as '{prefixColumn}desea_recibir_correos',
{prefixTable}CONTACTOS.desea_recibir_whatsapp as '{prefixColumn}desea_recibir_whatsapp',
{prefixTable}CONTACTOS.informacion_personal as '{prefixColumn}informacion_personal'";
        }

        private Entidades.ContactoEntidad ContactoReader (System.Data.SqlClient.SqlDataReader reader, string prefixColumn = "")
        {
            Entidades.ContactoEntidad entidad = new Entidades.ContactoEntidad();
            // OBLIGATORIOS
            entidad.id_contacto = (int)reader[$"{prefixColumn}id_contacto"];
            entidad.nombre_apellido = (string)reader[$"{prefixColumn}nombre_apellido"];
            entidad.tipo = (string)reader[$"{prefixColumn}tipo"];
            entidad.telefono = (string)reader[$"{prefixColumn}telefono"];
            entidad.correo = (string)reader[$"{prefixColumn}correo"];
            entidad.direccion = (string)reader[$"{prefixColumn}direccion"];
            entidad.fuente = (string)reader[$"{prefixColumn}fuente"];
            entidad.desea_recibir_correos = (bool)reader[$"{prefixColumn}desea_recibir_correos"];
            entidad.desea_recibir_whatsapp = (bool)reader[$"{prefixColumn}desea_recibir_whatsapp"];

            // OPCIONALES
            entidad.producto_que_provee = reader[$"{prefixColumn}producto_que_provee"] == DBNull.Value ? null : (string)reader[$"{prefixColumn}producto_que_provee"];
            entidad.informacion_personal = reader[$"{prefixColumn}informacion_personal"] == DBNull.Value ? null : (string)reader[$"{prefixColumn}informacion_personal"];
            return entidad;
        }

        public string GetSelect(string prefix = "")
        {
            return _QueryHelper.BuildSelect(prefix, ContactoSelect);
        }

        public Entidades.ContactoEntidad GetEntity(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            return _QueryHelper.BuildEntityFromReader(reader, prefix, ContactoReader);
        }

        private void ParametrizarEntidad(Entidades.ContactoEntidad entidad, AccesoDatos datos)
        {
            datos.SetearParametro("@id_contacto", entidad.id_contacto);
            datos.SetearParametro("@nombre_apellido", entidad.nombre_apellido);
            datos.SetearParametro("@tipo", entidad.tipo);
            datos.SetearParametro("@telefono", entidad.telefono);
            datos.SetearParametro("@correo", entidad.correo);
            datos.SetearParametro("@direccion", entidad.direccion);
            datos.SetearParametro("@fuente", entidad.fuente);
            // validar si es nulo el producto que provee
            if (entidad.producto_que_provee == null)
            {
                datos.SetearParametro("@producto_que_provee", DBNull.Value);
            }
            else
            {
                datos.SetearParametro("@producto_que_provee", entidad.producto_que_provee);
            }
            datos.SetearParametro("@desea_recibir_correos", entidad.desea_recibir_correos);
            datos.SetearParametro("@desea_recibir_whatsapp", entidad.desea_recibir_whatsapp);
            // validar informacion personal
            if (entidad.informacion_personal == null)
            {
                datos.SetearParametro("@informacion_personal", DBNull.Value);
            }
            else
            {
                datos.SetearParametro("@informacion_personal", entidad.informacion_personal);
            }
        }
        public List<Dominio.Modelos.ContactoModelo> Listar()
        {
            List<Dominio.Modelos.ContactoModelo > contactos = new List<Dominio.Modelos.ContactoModelo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string cmd = $@"
SELECT
    {GetSelect()}
FROM CONTACTOS
";
                datos.SetearConsulta(cmd);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Entidades.ContactoEntidad entidad = GetEntity(datos.Lector);
                    contactos.Add(Mappers.ContactoMapper.EntidadAModelo(entidad));
                }
                return contactos;
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

        public Dominio.Modelos.ContactoModelo ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string cmd = $@"
SELECT
    {GetSelect()}
FROM CONTACTOS
WHERE id_contacto = @id
";
                datos.SetearParametro("@id", id);
                datos.SetearConsulta(cmd);
                datos.EjecutarLectura();
                datos.Lector.Read();
                Entidades.ContactoEntidad entidad = GetEntity(datos.Lector);

                return Mappers.ContactoMapper.EntidadAModelo(entidad);
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

        public void Agregar(Dominio.Modelos.ContactoModelo contacto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                Entidades.ContactoEntidad entidad = Mappers.ContactoMapper.ModeloAEntidad(contacto);
                datos.SetearConsulta("INSERT INTO [dbo].[CONTACTOS] (nombre_apellido, tipo, telefono, correo, direccion, fuente, producto_que_provee, desea_recibir_correos, desea_recibir_whatsapp, informacion_personal) VALUES (@nombre_apellido, @tipo, @telefono, @correo, @direccion, @fuente, @producto_que_provee, @desea_recibir_correos, @desea_recibir_whatsapp, @informacion_personal)");
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

        public void Modificar(Dominio.Modelos.ContactoModelo contacto)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                Entidades.ContactoEntidad entidad = Mappers.ContactoMapper.ModeloAEntidad(contacto);
                datos.SetearConsulta("UPDATE [dbo].[CONTACTOS] SET nombre_apellido = @nombre_apellido, tipo = @tipo, telefono = @telefono, correo = @correo, direccion = @direccion, fuente = @fuente, producto_que_provee = @producto_que_provee, desea_recibir_correos = @desea_recibir_correos, desea_recibir_whatsapp = @desea_recibir_whatsapp, informacion_personal = @informacion_personal WHERE id_contacto = @id_contacto");
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
            throw new NotImplementedException();
        }


    }
}
