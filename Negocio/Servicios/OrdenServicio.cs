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
        
        public int GetTotalCount()
        {
            return ordenRepositorio.GetTotalCount();
        }
        
        public int GetFilteredTotalCount(int semanas = 0, int estado = 0)
        {
            return ordenRepositorio.GetFilteredTotalCount(semanas, estado);
        }
        
        public List<Dominio.Modelos.OrdenModelo> GetFilteredPage(int pageNumber, int pageSize, int semanas = 0, int estado = 0)
        {
            return ordenRepositorio.GetFilteredPage(pageNumber, pageSize, semanas, estado);
        }

        public List<Dominio.Modelos.OrdenModelo> GetPage(int pageNumber, int pageSize)
        {
            return ordenRepositorio.GetPage(pageNumber, pageSize);
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
