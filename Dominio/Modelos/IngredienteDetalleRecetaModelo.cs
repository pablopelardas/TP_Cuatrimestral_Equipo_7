using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class IngredienteDetalleRecetaModelo
    {
        public int IdReceta { get; set; }
        public IngredienteModelo Ingrediente { get; set; }
        public int Cantidad { get; set; }

        public decimal Subtotal
        {
            get
            {
                return Cantidad * Ingrediente.Costo;
            }
        }

        public IngredienteDetalleRecetaModelo() { }

    }
}
