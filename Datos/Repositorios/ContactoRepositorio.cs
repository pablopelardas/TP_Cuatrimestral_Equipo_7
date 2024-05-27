using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class ContactoRepositorio
    {
        public List<Dominio.Modelos.ContactoModelo> Listar()
        {
            List<Dominio.Modelos.ContactoModelo > contactos = new List<Dominio.Modelos.ContactoModelo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT * FROM [dbo].[CONTACTOS]");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Entidades.ContactoEntidad entidad = new Entidades.ContactoEntidad();
                    entidad.id_contacto = (int)datos.Lector["id_contacto"];
                    entidad.nombre_apellido = (string)datos.Lector["nombre_apellido"];
                    entidad.tipo = (string)datos.Lector["tipo"];
                    entidad.telefono = (string)datos.Lector["telefono"];
                    entidad.correo = (string)datos.Lector["correo"];
                    entidad.direccion = (string)datos.Lector["direccion"];
                    entidad.fuente = (string)datos.Lector["fuente"];
                    entidad.producto_que_provee = (string)datos.Lector["producto_que_provee"];
                    entidad.desea_recibir_correos= (bool)datos.Lector["desea_recibir_correos"];
                    entidad.desea_recibir_whatsapp= (bool)datos.Lector["desea"];

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
                datos.SetearParametro("@id", id);
                datos.SetearConsulta(datos.Comando.CommandText = "SELECT * FROM [dbo].[CONTACTOS] WHERE id_contacto = @id");
                datos.EjecutarLectura();
                datos.Lector.Read();
                Entidades.ContactoEntidad entidad = new Entidades.ContactoEntidad();
                entidad.id_contacto = (int)datos.Lector["id_contacto"];
                entidad.nombre_apellido = (string)datos.Lector["nombre_apellido"];
                entidad.tipo = (string)datos.Lector["tipo"];
                entidad.telefono = (string)datos.Lector["telefono"];
                entidad.correo = (string)datos.Lector["correo"];
                entidad.direccion = (string)datos.Lector["direccion"];
                entidad.fuente = (string)datos.Lector["fuente"];
                entidad.producto_que_provee = datos.Lector["producto_que_provee"] == DBNull.Value ? null : (string)datos.Lector["producto_que_provee"];
                entidad.desea_recibir_correos = (bool)datos.Lector["desea_recibir_correos"];
                entidad.desea_recibir_whatsapp = (bool)datos.Lector["desea_recibir_whatsapp"];

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
            throw new NotImplementedException();
        }

        public void Modificar(Dominio.Modelos.ContactoModelo contacto)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(Dominio.Modelos.ContactoModelo contacto)
        {
            throw new NotImplementedException();
        }


    }
}
