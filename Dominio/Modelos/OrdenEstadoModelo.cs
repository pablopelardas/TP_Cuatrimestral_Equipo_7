using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    [Serializable()]
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
        
        public OrdenEstadoModelo SiguienteEstado
        {
            get
            {
                switch (Nombre)
                {
                    case "Pendiente":
                        return new OrdenEstadoModelo { IdOrdenEstado = 2, Nombre = "En preparación" };
                    case "En preparación":
                        return new OrdenEstadoModelo { IdOrdenEstado = 3, Nombre = "Finalizada" };
                    case "Finalizada":
                        return new OrdenEstadoModelo { IdOrdenEstado = 4, Nombre = "Entregada" };
                    case "Entregada":
                        return new OrdenEstadoModelo { IdOrdenEstado = 5, Nombre = "Cancelada" };
                    case "Cancelada":
                        return new OrdenEstadoModelo { IdOrdenEstado = 5, Nombre = "Cancelada" };
                    default:
                        return new OrdenEstadoModelo { IdOrdenEstado = 1, Nombre = "Pendiente" };
                }
            }
        }
    }
}
