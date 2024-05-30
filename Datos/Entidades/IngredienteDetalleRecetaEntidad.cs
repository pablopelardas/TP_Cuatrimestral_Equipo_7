using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Entidades
{
    internal class IngredienteDetalleRecetaEntidad
    {
        public Entidades.IngredienteEntidad ingrediente { get; set; }
        public int cantidad { get; set; }
    }
}
