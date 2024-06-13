using System;
using Datos.EF;

namespace Datos.Mappers
{
    internal class OrdenDireccionMapper
    {
        internal static Dominio.Modelos.DireccionModelo EntidadAModelo(ORDENDIRECCION direccion, bool mapOrden = true)
        {
            Dominio.Modelos.DireccionModelo modelo = new Dominio.Modelos.DireccionModelo
            {
                // ATRIBUTOS DE ENTIDAD
                Localidad = direccion.localidad,
                CodigoPostal = direccion.codigo_postal,
                Piso = direccion.piso,
                Departamento = direccion.departamento,
                CalleNumero = direccion.calle_numero,
                Provincia = direccion.provincia,
                Orden = mapOrden && direccion.ORDEN != null ? OrdenMapper.EntidadAModelo(direccion.ORDEN) : null,
                Cliente = null,
            };

            return modelo;
        } 
        
        internal static ORDENDIRECCION ModeloAEntidad(Dominio.Modelos.DireccionModelo modelo)
        {
            ORDENDIRECCION direccion = new ORDENDIRECCION();
            ActualizarEntidad(ref direccion, modelo);
            return direccion;
        }
        
        internal static void ActualizarEntidad(ref ORDENDIRECCION direccion, Dominio.Modelos.DireccionModelo modelo)
        {
            // ATRIBUTOS DE MODELO
            direccion.localidad = modelo.Localidad;
            direccion.codigo_postal = modelo.CodigoPostal;
            direccion.piso = modelo.Piso;
            direccion.departamento = modelo.Departamento;
            direccion.calle_numero = modelo.CalleNumero;
            direccion.provincia = modelo.Provincia;
            direccion.id_orden_direccion = modelo.IdDireccion;
        }
    }
}