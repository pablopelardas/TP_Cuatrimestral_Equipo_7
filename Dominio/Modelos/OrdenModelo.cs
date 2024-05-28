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
        public int IdCliente { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoEvento { get; set; }
        public string TipoEntrega { get; set; }
        public List<ProductoModelo> Productos { get; set; } = new List<ProductoModelo>();
        public decimal Total { get; set; }
        public decimal Descuento { get; set; }
        public decimal Incremento { get; set; }
        public string Descripcion { get; set; }
    }
}
