using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class ItemDetalleProductoModelo
    {
        public int Cantidad {  get; set; }
        public RecetaModelo Receta { get; set; }
        public SuministroModelo Suministro { get; set; }
        
        public decimal SubTotal
        {
            get
            {
                if (Receta != null)
                {
                    return Receta.CostoTotal * Cantidad;
                }
                else if (Suministro != null)
                {
                    return Suministro.CostoNormalizado * Cantidad;
                }

                return 0;
            }
        }
        public ItemDetalleProductoModelo() { }

    }
}
