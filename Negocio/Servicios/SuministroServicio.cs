using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Servicios
{
    public class SuministroServicio
    {
        private Datos.Repositorios.SuministroRepositorio suministroRepositorio;

        public SuministroServicio()
        {
            suministroRepositorio = new Datos.Repositorios.SuministroRepositorio();
        }

        public List<Dominio.Modelos.SuministroModelo> Listar()
        {
            return suministroRepositorio.Listar();
        }

        public Dominio.Modelos.SuministroModelo ObtenerPorId(Guid id)
        {
            return suministroRepositorio.ObtenerPorId(id);
        }

        public void Agregar(Dominio.Modelos.SuministroModelo receta)
        {
            suministroRepositorio.Agregar(receta);
        }

        public void Modificar(Dominio.Modelos.SuministroModelo receta)
        {
            suministroRepositorio.Modificar(receta);
        }

        public void Eliminar(Guid id)
        {
            suministroRepositorio.Eliminar(id);
        }
    }
}
