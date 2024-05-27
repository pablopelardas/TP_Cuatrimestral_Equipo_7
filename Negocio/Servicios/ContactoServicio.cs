using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ContactoServicio
    {
        private Datos.Repositorios.ContactoRepositorio contactosRepositorio;

        public ContactoServicio()
        {
            contactosRepositorio = new Datos.Repositorios.ContactoRepositorio();
        }

        public List<Dominio.Modelos.ContactoModelo> Listar()
        {
            return contactosRepositorio.Listar();
        }

        public void Agregar(Dominio.Modelos.ContactoModelo contacto)
        {
            contactosRepositorio.Agregar(contacto);
        }

        public void Actualizar(Dominio.Modelos.ContactoModelo contacto)
        {
            contactosRepositorio.Actualizar(contacto);
        }

        public void Eliminar(int id)
        {
            contactosRepositorio.Eliminar(id);
        }

    }
}
