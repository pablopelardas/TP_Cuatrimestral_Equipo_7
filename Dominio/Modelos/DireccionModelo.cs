using System;

namespace Dominio.Modelos
{
    public class DireccionModelo
    { 
        public Guid IdDireccion { get; set; }
        public string Descripcion { get; set; }
        public string GoogleName { get; set; }
        public string GoogleLat { get; set; }
        public string GoogleLng { get; set; }
        public string GooglePlaceId { get; set; }
        public string GoogleFormattedAddress { get; set; }
        public string GoogleUrl { get; set; }
    }
}