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
                    case "Pendiente":
                        return "status-pill status-pill--pending";
                    case "En preparación":
                        return "status-pill status-pill--doing";
                    case "Finalizada":
                        return "status-pill status-pill--done";
                    case "Entregada":
                        return "status-pill status-pill--delivered";
                    case "Cancelada":
                        return "status-pill status-pill--canceled";
                    default:
                        return "status-pill";
                }
            }
        }
    }
}
