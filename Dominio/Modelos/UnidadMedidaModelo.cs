﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class UnidadMedidaModelo
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Abreviatura { get; set; }
        public UnidadMedidaModelo() { }
    }
}
