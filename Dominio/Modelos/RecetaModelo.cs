using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class RecetaModelo
    {
        public Guid IdReceta { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        
        public string Rendimiento { get; set; }
        public List<IngredienteDetalleRecetaModelo> DetalleRecetas { get; set; }
        public decimal CostoIngredientes
        {
            get
            {
                decimal total = 0;

                foreach (var detalle in DetalleRecetas)
                {
                    total += detalle.Subtotal;
                }

                return total;
            }
        }
        public decimal PrecioPersonalizado { get; set; }

        public decimal CostoTotal
        {
            get
            {
                return decimal.Round(PrecioPersonalizado == 0 ? CostoIngredientes : PrecioPersonalizado, 2);
            }
        }

        public CategoriaModelo Categoria { get; set; }
        public RecetaModelo() { }
    }
}
