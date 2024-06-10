using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class ProductoRepositorio
    {

        public List<Dominio.Modelos.ProductoModelo> Listar()
        {
            Entities db = new Entities();
            List<Dominio.Modelos.ProductoModelo> productos = new List<Dominio.Modelos.ProductoModelo>();
            try
            {
                var query = from p in db.PRODUCTOS
                            select new
                            {
                                Producto = p
                            };

                foreach (var row in query)
                {
                    productos.Add(Mappers.ProductoMapper.EntidadAModelo(row.Producto));
                }

                return productos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dominio.Modelos.ProductoModelo ObtenerPorId(int id)
        {
            Entities db = new Entities();
            try
            {
                var query = from p in db.PRODUCTOS
                            where p.id_producto == id
                            select new
                            {
                                Producto = p
                            };

                var row = query.FirstOrDefault();
                if (row != null)
                {
                    return Mappers.ProductoMapper.EntidadAModelo(row.Producto);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       public void Agregar(ProductoModelo producto)
        {
            Entities db = new Entities();
            try
            {
                db.PRODUCTOS.Add(Mappers.ProductoMapper.ModeloAEntidad(producto));
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(ProductoModelo modelo)
        {
            Entities db = new Entities();
            try
            {
                PRODUCTO p = db.PRODUCTOS.Find(modelo.IdProducto);
                Mappers.ProductoMapper.ActualizarEntidad(ref p, modelo);
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }
    }
}
