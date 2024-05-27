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
                    entidad.Id = (int)datos.Lector["Id"];
                    entidad.NombreApellido = (string)datos.Lector["NombreApellido"];
                    entidad.Tipo = (string)datos.Lector["Tipo"];
                    entidad.Telefono = (string)datos.Lector["Telefono"];
                    entidad.Correo = (string)datos.Lector["Correo"];
                    entidad.Direccion = (string)datos.Lector["Direccion"];
                    entidad.Fuente = (string)datos.Lector["Fuente"];
                    entidad.ProductoQueProvee = (string)datos.Lector["ProductoQueProvee"];
                    entidad.DeseaRecibirCorreos = (bool)datos.Lector["DeseaRecibirCorreos"];
                    entidad.DeseaRecibirWhatsapp = (bool)datos.Lector["DeseaRecibirWhatsapp"];

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
