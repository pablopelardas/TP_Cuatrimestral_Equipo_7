using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Servicios
{
    public class UnidadMedidaServicio
    {
        private Datos.Repositorios.UnidadMedidaRepositorio unidadMedidaRepositorio;

        public UnidadMedidaServicio()
        {
            unidadMedidaRepositorio = new Datos.Repositorios.UnidadMedidaRepositorio();
        }

        public List<Dominio.Modelos.UnidadMedidaModelo> Listar()
        {
            return unidadMedidaRepositorio.Listar();
        }

        public Dominio.Modelos.UnidadMedidaModelo ObtenerPorId(int id)
        {
            return unidadMedidaRepositorio.ObtenerPorId(id);
        }

        public void Agregar(Dominio.Modelos.UnidadMedidaModelo unidad)
        {
            unidadMedidaRepositorio.Agregar(unidad);
        }

        public void Modificar(Dominio.Modelos.UnidadMedidaModelo unidad)
        {
            unidadMedidaRepositorio.Modificar(unidad);
        }

        public void Eliminar(int id)
        {
            unidadMedidaRepositorio.Eliminar(id);
        }
    }
}
