using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class IngredienteModelo
    {
        public int IdIngrediente {  get; set; }
        public string Nombre { get; set; }
        public float Cantidad { get; set; }
        public UnidadMedidaModelo Unidad { get; set; }
        public decimal Costo { get; set; }
        public string Proveedor { get; set; }
        public IngredienteModelo() { }
    }
}
