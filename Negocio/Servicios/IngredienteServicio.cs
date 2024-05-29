using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Servicios
{
    public class IngredienteServicio
    {
        private Datos.Repositorios.IngredienteRepositorio ingredienteRepositorio;

        public IngredienteServicio()
        {
            ingredienteRepositorio = new Datos.Repositorios.IngredienteRepositorio();
        }

        public List<Dominio.Modelos.IngredienteModelo> Listar()
        {
            return ingredienteRepositorio.Listar();
        }

        public Dominio.Modelos.IngredienteModelo ObtenerPorId(int id)
        {
            return ingredienteRepositorio.ObtenerPorId(id);
        }

        public void Agregar(Dominio.Modelos.IngredienteModelo ingrediente)
        {
            ingredienteRepositorio.Agregar(ingrediente);
        }

        public void Modificar(Dominio.Modelos.IngredienteModelo ingrediente)
        {
            ingredienteRepositorio.Modificar(ingrediente);
        }

        public void Eliminar(int id)
        {
            ingredienteRepositorio.Eliminar(id);
        }
    }
}
