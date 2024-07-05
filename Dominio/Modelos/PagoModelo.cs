using System;

namespace Dominio.Modelos
{
    public class PagoModelo
    {
        public Guid IdPago { get; set; }
        public Guid IdOrden { get; set; }
        public Guid IdCliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public string TipoPago { get; set; }
    }
}