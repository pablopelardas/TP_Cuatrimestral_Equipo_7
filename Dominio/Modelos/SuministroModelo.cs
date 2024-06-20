using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class SuministroModelo
    {
        public Guid IdSuministro { get; set; }
        public string Nombre { get; set; }
        public string Proveedor { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Costo { get; set; }
        
        public decimal CostoNormalizado
        {
            get
            {
                return Costo / Cantidad;
            }
        }
        public CategoriaModelo Categoria { get; set; }
        public SuministroModelo() { }
    }
}
