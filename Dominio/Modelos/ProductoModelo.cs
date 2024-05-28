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
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public int Porciones { get; set; }

        public decimal HorasTrabajo { get; set; }

        public string TipoPrecio { get; set; }

        public decimal ValorPrecio { get; set; }

        public CategoriaModelo Categoria { get; set; }

        public ProductoModelo()
        {
        }
    }
}
