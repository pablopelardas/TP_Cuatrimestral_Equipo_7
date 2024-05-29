﻿using System;
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
        public decimal CostoEnvio { get; set; }
        public string Descripcion { get; set; }

        public decimal Total
        {
            get
            {
                return Subtotal - (Subtotal * DescuentoPorcentaje / 100) + CostoEnvio;
            }
        }

        public ContactoModelo Cliente { get; set; }

        public List<ProductoDetalleOrdenModelo> DetalleProductos { get; set; } = new List<ProductoDetalleOrdenModelo>();
    }
}
