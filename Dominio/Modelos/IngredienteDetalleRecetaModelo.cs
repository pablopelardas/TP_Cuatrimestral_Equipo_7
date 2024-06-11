using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class IngredienteDetalleRecetaModelo
    {
        public Guid IdReceta { get; set; }
        public IngredienteModelo Ingrediente { get; set; }
        public double Cantidad { get; set; }

        public double Subtotal
        {
            get
            {
                return Cantidad * (double)Ingrediente.Costo;
            }
        }

        public IngredienteDetalleRecetaModelo() { }

    }
}
