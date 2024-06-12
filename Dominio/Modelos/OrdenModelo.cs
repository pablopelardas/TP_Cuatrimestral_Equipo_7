﻿using System;
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
        public TimeSpan HoraEntrega { get; set; }
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
        
        public string DetalleEntrega
        {
            get
            {
                // time format HH:MM
                string horario = HoraEntrega.ToString("hh\\:mm");
                if (TipoEntrega == "Delivery")
                {
                    return $"{TipoEntrega} - {DireccionEntrega} - {horario}";
                }
                else
                {
                    return $"{TipoEntrega} - {horario}";
                }
            }
        }
        
        public string ShortId
        {
            get
            {
                // last 4 digits of the id
                return IdOrden.ToString().Substring(IdOrden.ToString().Length - 4);
            }
        }

        public ContactoModelo Cliente { get; set; }
        public OrdenEstadoModelo Estado { get; set; }
        public OrdenEstadoPagoModelo EstadoPago { get; set; }
        
        public DireccionModelo DireccionEntrega { get; set; }


        public List<ProductoDetalleOrdenModelo> DetalleProductos { get; set; } = new List<ProductoDetalleOrdenModelo>();
        public EventoModelo Evento { get; set; }

        public OrdenModelo()
        {
            DireccionEntrega = new DireccionModelo();
        }


    }
}
