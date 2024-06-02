﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Entidades
{
    public class IngredienteEntidad
    {
        public int id_ingrediente { get; set; }
        public string nombre { get; set; }
        public double cantidad { get; set; }
        public decimal costo { get; set; }
        public string proveedor { get; set; }
        public UnidadMedidaEntidad unidad { get; set; }
        public IngredienteEntidad() { }
    }
}