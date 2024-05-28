using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class OrdenModelo
    {
        public int IdOrden { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoEvento { get; set; }
        public string TipoEntrega { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public decimal Subtotal { get; set; }
        public decimal IncrementoPorcentaje { get; set; }
        public string Descripcion { get; set; }

        public decimal Total
        {
            get
            {
                return Subtotal - (Subtotal * DescuentoPorcentaje / 100) + (Subtotal * IncrementoPorcentaje / 100);
            }
        }

        public ContactoModelo Cliente { get; set; }
        public List<ProductoModelo> Productos { get; set; } = new List<ProductoModelo>();
    }
}
