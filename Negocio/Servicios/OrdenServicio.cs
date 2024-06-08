using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Servicios
{
    public class OrdenServicio
    {
        private Datos.Repositorios.OrdenRepositorio ordenRepositorio;

        public OrdenServicio()
        {
            ordenRepositorio = new Datos.Repositorios.OrdenRepositorio();
        }

        public List<Dominio.Modelos.OrdenModelo> Listar()
        {
            return ordenRepositorio.Listar();
        }

        public Dominio.Modelos.OrdenModelo ObtenerPorId(int id)
        {
            return ordenRepositorio.ObtenerPorId(id);
        }

        public void GuardarOrden(Dominio.Modelos.OrdenModelo orden)
        {
            ordenRepositorio.GuardarOrdenTx(orden);
        }

        public void Eliminar(int id)
        {
            ordenRepositorio.Eliminar(id);
        }

        

    }
}
