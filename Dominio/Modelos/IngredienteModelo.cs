using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class IngredienteModelo
    {
        public Guid IdIngrediente {  get; set; }
        public string Nombre { get; set; }
        public decimal Cantidad { get; set; }
        public UnidadMedidaModelo Unidad { get; set; }
        public decimal Costo { get; set; }
        public decimal CostoNormalizado 
        { 
            get
            {
                return decimal.Round(Costo / Cantidad, 2);
            } 
        }
        public string Proveedor { get; set; }
        public IngredienteModelo() { }
    }
}
