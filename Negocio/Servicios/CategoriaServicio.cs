using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Servicios
{
    public class CategoriaServicio
    {
        private Datos.Repositorios.CategoriaRepositorio categoriaRepositorio;

        public CategoriaServicio()
        {
            categoriaRepositorio = new Datos.Repositorios.CategoriaRepositorio();
        }

        public List<Dominio.Modelos.CategoriaModelo> Listar(string tipo)
        {
            return categoriaRepositorio.Listar(tipo);
        }

        public Dominio.Modelos.CategoriaModelo ObtenerPorId(Guid id)
        {
            return categoriaRepositorio.ObtenerPorId(id);
        }

        public void Agregar(Dominio.Modelos.CategoriaModelo categoria)
        {
            categoriaRepositorio.Agregar(categoria);
        }

        public void Modificar(Dominio.Modelos.CategoriaModelo categoria)
        {
            categoriaRepositorio.Modificar(categoria);
        }

        public void Eliminar(int id)
        {
            categoriaRepositorio.Eliminar(id);
        }
    }
}
