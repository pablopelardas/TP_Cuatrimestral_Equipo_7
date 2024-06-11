using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class OrdenModelo
    {
        public Guid IdOrden { get; set; }
        public string TipoEntrega { get; set; }
        public string HoraEntrega { get; set; }
        public string DireccionEntrega { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public decimal CostoEnvio { get; set; }
        public string Descripcion { get; set; }

        public decimal Subtotal
        {
            get
            {
                return DetalleProductos.Sum(x => x.Subtotal);
            }
        }

        public decimal Total
        {
            get
            {
                return Subtotal - (Subtotal * DescuentoPorcentaje / 100) + CostoEnvio;
            }
        }

        public ContactoModelo Cliente { get; set; }
        public OrdenEstadoModelo Estado { get; set; }
        public OrdenEstadoPagoModelo EstadoPago { get; set; }



        public List<ProductoDetalleOrdenModelo> DetalleProductos { get; set; } = new List<ProductoDetalleOrdenModelo>();
        public EventoModelo Evento { get; set; }
    }
}
