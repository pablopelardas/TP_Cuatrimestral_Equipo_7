using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Entidades
{
    public class DetalleEntidad
    {
        public int id_detalle {  get; set; }
        public int id_orden {  get; set; }
        public int id_producto { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public DetalleEntidad() { }
    }
}
