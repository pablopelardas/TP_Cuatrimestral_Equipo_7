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
                Localidad = direccion.localidad,
                CodigoPostal = direccion.codigo_postal,
                Piso = direccion.piso,
                Departamento = direccion.departamento,
                CalleNumero = direccion.calle_numero,
                Provincia = direccion.provincia
            };

            return modelo;
        } 
        
        internal static DIRECCION ModeloAEntidad(Dominio.Modelos.DireccionModelo modelo)
        {
            DIRECCION direccion = new DIRECCION
            {
                // ATRIBUTOS DE MODELO
                id_direccion = modelo.IdDireccion,
                localidad = modelo.Localidad,
                codigo_postal = modelo.CodigoPostal,
                piso = modelo.Piso,
                departamento = modelo.Departamento,
                calle_numero = modelo.CalleNumero,
                provincia = modelo.Provincia
            };

            return direccion;
        }
        
        internal static void ActualizarEntidad(ref DIRECCION direccion, Dominio.Modelos.DireccionModelo modelo)
        {
            // ATRIBUTOS DE MODELO
            direccion.id_direccion = modelo.IdDireccion;
            direccion.localidad = modelo.Localidad;
            direccion.codigo_postal = modelo.CodigoPostal;
            direccion.piso = modelo.Piso;
            direccion.departamento = modelo.Departamento;
            direccion.calle_numero = modelo.CalleNumero;
            direccion.provincia = modelo.Provincia;
        }
    }
}