using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Entidades
{
    public class CategoriaEntidad
    {
        public int id_categoria { get; set; }
        public string nombre { get; set; }
        public string tipo { get; set; }

        public CategoriaEntidad()
        {
        }
    }
}
