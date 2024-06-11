using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class OrdenEstadoPagoModelo
    {
        public int IdOrdenPagoEstado { get; set; }
        public string Nombre { get; set; }

        public string PillClass
        {
            get
            {
                switch (Nombre)
                {
                    case "Sin Pagos":
                        return "status-pill status-pill--danger";
                    case "PARCIALMENTE PAGADO":
                        return "status-pill status-pill--warning";
                    case "PAGADO":
                        return "status-pill status-pill--success";
                    default:
                        return "status-pill status-pill--default";
                }
            }
        }
    }
}
