﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    [Serializable()]
    public class OrdenModelo
    {
        public Guid IdOrden { get; set; }
        public string TipoEntrega { get; set; }
        public TimeSpan HoraEntrega { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public decimal CostoEnvio { get; set; }
        public string Descripcion { get; set; }

        public decimal Subtotal
        {
            get
            {
                return decimal.Round(DetalleProductos.Sum(x => x.Subtotal), 2);
            }
        }

        public decimal Total
        {
            get
            {
                return decimal.Round(Subtotal - (Subtotal * DescuentoPorcentaje / 100) + CostoEnvio, 2);
            }
        }
        
        public decimal TotalPagado
        {
            get
            {
                return decimal.Round(Pagos.Sum(x => x.Monto), 2);
            }
        }
        public string DetalleEntrega
        {
            get
            {
                    return $"{TipoEntrega} - {HoraEntrega:hh\\:mm}";
            }
        }
        
        public string ShortId
        {
            get
            {
                return IdOrden.ToString().Substring(0, 8);
            }
        }

        public ContactoModelo Cliente { get; set; }
        public OrdenEstadoModelo Estado { get; set; }
        public OrdenEstadoPagoModelo EstadoPago { get; set; }
        
        public DireccionModelo DireccionEntrega { get; set; }


        public List<ProductoDetalleOrdenModelo> DetalleProductos { get; set; }
        
        public List<PagoModelo> Pagos { get; set; }
        public EventoModelo Evento { get; set; }
        
        public ListaCompra ListaCompra { get; set; }

        public OrdenModelo()
        {
            DireccionEntrega = new DireccionModelo();
            DetalleProductos = new List<ProductoDetalleOrdenModelo>();
            Pagos = new List<PagoModelo>();
        }


    }
}
