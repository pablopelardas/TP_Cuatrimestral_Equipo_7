using System;

namespace Dominio.Modelos
{
    [Serializable()]
    public class HistoricoModelo
    {
        public Guid IdHistorico { get; set; }
        public Guid IdEntidad { get; set; }
        public DateTime Fecha { get; set; }
        public string Justificacion { get; set; }
        
    }
}