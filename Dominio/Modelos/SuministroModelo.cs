using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class SuministroModelo
    {
        public int IdSuministro { get; set; }
        public string Nombre { get; set; }
        public string Proveedor { get; set; }
        public double Cantidad { get; set; }
        public decimal Costo { get; set; }
        public CategoriaModelo Categoria { get; set; }
        public SuministroModelo() { }
    }
}
