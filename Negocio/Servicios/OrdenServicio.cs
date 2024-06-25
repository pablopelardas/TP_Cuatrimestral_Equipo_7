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
        
        public List<Dominio.Modelos.OrdenModelo> ListarOrdenes(int semanas, int estado, int paginaActual, int contactosPorPagina, out int totalPaginas, ContactoModelo contacto = null)
        {
            int totalOrdenes = ordenRepositorio.GetFilteredTotalCount(semanas, estado, contacto);
            totalPaginas = (int)Math.Ceiling((double)totalOrdenes / contactosPorPagina);
            return ordenRepositorio.GetFilteredPage(paginaActual, contactosPorPagina, semanas, estado, contacto);
        }

        public Dominio.Modelos.OrdenModelo ObtenerPorId(Guid id)
        {
            return ordenRepositorio.ObtenerPorId(id);
        }

        public OrdenModelo GuardarOrden(Dominio.Modelos.OrdenModelo orden, bool guardarDireccionEnCliente = false)
        {
           return ordenRepositorio.GuardarOrdenTx(orden, guardarDireccionEnCliente);
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
