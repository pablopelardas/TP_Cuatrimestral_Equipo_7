using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Modelos;

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

        public Dominio.Modelos.OrdenModelo ObtenerPorId(Guid id)
        {
            return ordenRepositorio.ObtenerPorId(id);
        }

        public void GuardarOrden(Dominio.Modelos.OrdenModelo orden)
        {
            ordenRepositorio.GuardarOrdenTx(orden);
        }

        public void Eliminar(int id)
        {
            //ordenRepositorio.Eliminar(id);
        }
        
        public OrdenModelo CambiarEstado(Guid idOrden, int idEstado)
        {
            return ordenRepositorio.CambiarEstado(idOrden, idEstado);
        }

        public List<OrdenEstadoModelo> ListarEstadosDeOrden()
        {
            return ordenRepositorio.ListarEstados();
        }
    }
}
