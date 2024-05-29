using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Servicios
{
    public class ProductoServicio
    {
        private Datos.Repositorios.ProductoRepositorio productosRepositorio;

        public ProductoServicio()
        {
            productosRepositorio = new Datos.Repositorios.ProductoRepositorio();
        }

        public List<Dominio.Modelos.ProductoModelo> Listar()
        {
            return productosRepositorio.Listar();
        }

        public Dominio.Modelos.ProductoModelo ObtenerPorId(int id)
        {
            return productosRepositorio.ObtenerPorId(id);
        }

        public void Agregar(Dominio.Modelos.ProductoModelo producto)
        {
            productosRepositorio.Agregar(producto);
        }

        public void Modificar(Dominio.Modelos.ProductoModelo producto)
        {
            productosRepositorio.Modificar(producto);
        }

        public void Eliminar(int id)
        {
            productosRepositorio.Eliminar(id);
        }
    }
}
