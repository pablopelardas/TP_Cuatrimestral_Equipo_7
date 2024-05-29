using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Entidades
{
    public class ProductoEntidad
    {
        public int id_producto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }

        public int porciones { get; set; }

        public decimal horas_trabajo { get; set; }

        public string tipo_precio { get; set; }

        public decimal valor_precio { get; set; }

        public Entidades.CategoriaEntidad categoria { get; set; }

        public ProductoEntidad()
        {
        }
    }
}
