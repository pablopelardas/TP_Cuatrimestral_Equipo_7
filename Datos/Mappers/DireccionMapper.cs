using Datos.EF;

namespace Datos.Mappers
{
    internal class DireccionMapper
    {
        internal static Dominio.Modelos.DireccionModelo EntidadAModelo(DIRECCION direccion)
        {
            Dominio.Modelos.DireccionModelo modelo = new Dominio.Modelos.DireccionModelo
            {
                // ATRIBUTOS DE ENTIDAD
                IdDireccion = direccion.id_direccion,
                Descripcion = direccion.descripcion,
                GoogleName = direccion.google_name,
                GoogleLat = direccion.google_lat,
                GoogleLng = direccion.google_lng,
                GooglePlaceId = direccion.google_place_id,
                GoogleFormattedAddress = direccion.google_formatted_address,
                GoogleUrl = direccion.google_url
            };

            return modelo;
        } 
        
        internal static DIRECCION ModeloAEntidad(Dominio.Modelos.DireccionModelo modelo)
        {
            DIRECCION direccion = new DIRECCION
            {
                // ATRIBUTOS DE MODELO
                id_direccion = modelo.IdDireccion,
                descripcion = modelo.Descripcion,
                google_name = modelo.GoogleName,
                google_lat = modelo.GoogleLat,
                google_lng = modelo.GoogleLng,
                google_place_id = modelo.GooglePlaceId,
                google_formatted_address = modelo.GoogleFormattedAddress,
                google_url = modelo.GoogleUrl
            };

            return direccion;
        }
    }
}