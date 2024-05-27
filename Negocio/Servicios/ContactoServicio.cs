using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Servicios
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

        public Dominio.Modelos.ContactoModelo ObtenerPorId(int id)
        {
            return contactosRepositorio.ObtenerPorId(id);
        }

        public void Agregar(Dominio.Modelos.ContactoModelo contacto)
        {
            contactosRepositorio.Agregar(contacto);
        }

        public void Modificar(Dominio.Modelos.ContactoModelo contacto)
        {
            contactosRepositorio.Modificar(contacto);
        }

        public void Eliminar(int id)
        {
            contactosRepositorio.Eliminar(id);
        }

        

    }
}
