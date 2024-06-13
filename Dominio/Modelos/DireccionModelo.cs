using System;

namespace Dominio.Modelos
{
    public class DireccionModelo
    { 
        public Guid IdDireccion { get; set; }
        
        public string CalleNumero { get; set; }
        public string Localidad { get; set; }
        
        public string Provincia { get; set; }
        public string CodigoPostal { get; set; }
        public string Piso { get; set; }
        public string Departamento { get; set; }
    }
}