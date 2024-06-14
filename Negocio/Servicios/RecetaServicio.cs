using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Servicios
{
    public class RecetaServicio
    {
        private Datos.Repositorios.RecetaRepositorio recetaRepositorio;

        public RecetaServicio()
        {
            recetaRepositorio = new Datos.Repositorios.RecetaRepositorio();
        }

        public List<Dominio.Modelos.RecetaModelo> Listar()
        {
            return recetaRepositorio.Listar();
        }

        public Dominio.Modelos.RecetaModelo ObtenerPorId(Guid id)
        {
            return recetaRepositorio.ObtenerPorId(id);
        }

        public void Agregar(Dominio.Modelos.RecetaModelo receta)
        {
            recetaRepositorio.Agregar(receta);
        }

        public void Modificar(Dominio.Modelos.RecetaModelo receta)
        {
            recetaRepositorio.Modificar(receta);
        }

        public void Eliminar(Guid id)
        {
            recetaRepositorio.Eliminar(id);
        }
    }
}
