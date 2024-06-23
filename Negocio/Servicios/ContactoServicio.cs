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
        
        public List<Dominio.Modelos.ContactoModelo> ListarContactos(string tipo, string filtro, int paginaActual, int contactosPorPagina, out int totalContactos, out int totalPaginas)
        {
            totalContactos = contactosRepositorio.GetFilteredTotalCount(tipo, filtro);
            totalPaginas = (int)Math.Ceiling((double)totalContactos / contactosPorPagina);
            return contactosRepositorio.GetFilteredPage(paginaActual, contactosPorPagina, tipo, filtro);
        }
        
        public List<Dominio.Modelos.ContactoModelo> ObtenerPorTipo(string tipo)
        {
            List<string> tipos = new List<string>
            {
                "Cliente",
                "Proveedor"
            };
            if (tipos.Contains(tipo))
            {
                return contactosRepositorio.ListarPorTipo(tipo);
            }
            return contactosRepositorio.Listar();
        }

        public Dominio.Modelos.ContactoModelo ObtenerPorId(Guid id)
        {
            return contactosRepositorio.ObtenerPorId(id);
        }

        public void Agregar(Dominio.Modelos.ContactoModelo contacto)
        {
            contactosRepositorio.Agregar(contacto);
        }

        public void Modificar(Dominio.Modelos.ContactoModelo contacto)
        {
            //contactosRepositorio.Modificar(contacto);
        }

        public void Eliminar(int id)
        {
            contactosRepositorio.Eliminar(id);
        }

        

    }
}
