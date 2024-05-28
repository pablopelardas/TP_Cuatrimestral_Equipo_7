using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class ProductoModelo
    {
        public int Codigo {  get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string Categoria {  get; set; }

        public int Porciones { get; set; }

        public int Horas { get; set; }

        public string Recetas { get; set; }

        public string Suministros { get; set; }

        public decimal Costo { get; set; }

        public decimal CostoPorPorcion {  get; set; }

        public decimal PrecioVenta { get; set; }

        public decimal TarifaImpuesto { get; set; }

        public decimal GananciaNeta { get; set; }

        public List<string> Imagenes = new List<string>();
    }
}
