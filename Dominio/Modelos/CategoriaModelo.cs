﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class CategoriaModelo
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Tipo { get; set; }

        public CategoriaModelo()
        {
        }
    }
}