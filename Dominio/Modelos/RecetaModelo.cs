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
        public decimal PrecioPersonalizado {  get; set; }
        public CategoriaModelo Categoria { get; set; }
        public List<IngredienteDetalleRecetaModelo> DetalleRecetas { get; set; }
        public RecetaModelo() { }
    }
}
