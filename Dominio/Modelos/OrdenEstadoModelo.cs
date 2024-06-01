using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class OrdenEstadoModelo
    {
        public int IdOrdenEstado { get; set; }
        public string Nombre { get; set; }

        public string PillClass
        {
            get
            {
                switch (Nombre)
                {
                    case "PENDIENTE":
                        return "status-pill status-pill--warning";
                    case "ENTREGADO":
                        return "status-pill status-pill--success";
                    case "CANCELADO":
                        return "status-pill status-pill--danger";
                    default:
                        return "status-pill status-pill--default";
                }
            }
        }
    }
}
