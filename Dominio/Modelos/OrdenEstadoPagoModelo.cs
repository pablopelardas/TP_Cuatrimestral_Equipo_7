using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    [Serializable()]
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
                    case "Sin pagos":
                        return "status-pill status-pill--canceled";
                    case "Parcialmente pagado":
                        return "status-pill status-pill--doing";
                    case "Pagado":
                        return "status-pill status-pill--delivered";
                    case "Cancelado":
                        return "status-pill status-pill--pending";
                    default:
                        return "status-pill";
                }
            }
        }
    }
}
