using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class ProductoModelo
    {
        public Guid IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public int Porciones { get; set; }

        public decimal HorasTrabajo { get; set; }

        public string TipoPrecio { get; set; }

        public decimal ValorPrecio { get; set; }

        public CategoriaModelo Categoria { get; set; }

        public List<ItemDetalleProductoModelo> Items { get; set; }

        public ListaCompra ListaCompra { get; set; }

        public decimal Precio
        {
            get
            {
                return Items
                    .Sum(item => item.SubTotal);
                // TODO: Implementar lógica de cálculo de precio de producto en base a los items
                
                // si es por margen
                
                // si es fijo
                
                // si es por porcion
                
                return 200;
            }
        }

        public decimal Costo
        {
            get
            {
                return Items
                            .Where(item => item.Receta != null)
                            .Sum(item => item.Receta.CostoIngredientes)
                            +
                            Items
                            .Where(item => item.Receta == null)
                            .Sum(item => item.Suministro.Costo * item.Suministro.Cantidad);
            }
        }
        
        // costo por porcion
        
        public string ShortId
        {
            get
            {
                // return last 4 characters of IdProducto
                return $"...{IdProducto.ToString().Substring(IdProducto.ToString().Length - 4)}";
            }
        }

        public ProductoModelo()
        {
            Items = new List<ItemDetalleProductoModelo>();
        }

    }
}
