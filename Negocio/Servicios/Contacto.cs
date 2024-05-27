using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Contacto
    {
        private Datos.Repositorios.Contacto contactosRepositorio;

        public Contacto()
        {
            contactosRepositorio = new Datos.Repositorios.Contacto();
        }

        public List<Dominio.Modelos.Contacto> Listar()
        {
            var contactosEntidad = contactosRepositorio.Listar();
            var contactos = new List<Dominio.Modelos.Contacto>();

            foreach (var contactoEntidad in contactosEntidad)
            {
                var contacto = Mappers.Contacto.EntidadAModelo(contactoEntidad);
                contactos.Add(contacto);
            }

            return contactos;
        }

        public void Agregar(Dominio.Modelos.Contacto contacto)
        {
            var contactoEntidad = Mappers.Contacto.ModeloAEntidad(contacto);
            contactosRepositorio.Agregar(contactoEntidad);
        }

        public void Actualizar(Dominio.Modelos.Contacto contacto)
        {
            var contactoEntidad = Mappers.Contacto.ModeloAEntidad(contacto);
            contactosRepositorio.Actualizar(contactoEntidad);
        }

        public void Eliminar(int id)
        {
            contactosRepositorio.Eliminar(id);
        }

    }
}
