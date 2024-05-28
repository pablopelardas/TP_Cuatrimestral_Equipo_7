using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Entidades
{
    public class ProductoEntidad
    {
        public int codigo { get; set; }

        public string nombre { get; set; }

        public string descripcion { get; set; }

        public string categoria { get; set; }

        public int porciones { get; set; }

        public int horas { get; set; }

        public string recetas { get; set; }

        public string suministros { get; set; }

        public decimal costo { get; set; }

        public decimal costo_porcion { get; set; }

        public decimal precio_venta { get; set; }

        public decimal tarifa_impuesto { get; set; }

        public decimal ganancia_neta { get; set; }

        public List<string> Imagenes = new List<string>();

        public ProductoEntidad() { }
    }
}
