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
        public decimal Cantidad { get; set; }

        public decimal Subtotal
        {
            get
            {
                // TODO: Implementar lógica de cálculo de subtotal de ingrediente en base a cantidad y costo
                return Cantidad * Ingrediente.Costo;
            }
        }

        public IngredienteDetalleRecetaModelo() { }

    }
}
