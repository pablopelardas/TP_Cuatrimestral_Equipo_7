using System.Collections.Generic;
using Datos.EF;

namespace Datos.Mappers
{
    public class PagoMapper
    {
        internal static Dominio.Modelos.PagoModelo EntidadAModelo(PAGO pago)
        {
            Dominio.Modelos.PagoModelo modelo = new Dominio.Modelos.PagoModelo()
            {
                IdPago = pago.id_pago,
                IdOrden = pago.id_orden,
                IdCliente = pago.id_cliente,
                Fecha = pago.fecha,
                Monto = pago.monto,
            };

            return modelo;
        } 
        
        internal static PAGO ModeloAEntidad(Dominio.Modelos.PagoModelo modelo)
        {
            PAGO pago = new PAGO();
            ActualizarEntidad(ref pago, modelo);
            return pago;
        }
        
        internal static void ActualizarEntidad(ref PAGO pago, Dominio.Modelos.PagoModelo pagoModelo)
        {
            // ATRIBUTOS DE MODELO
            pago.id_pago = pagoModelo.IdPago;
            pago.id_orden = pagoModelo.IdOrden;
            pago.id_cliente = pagoModelo.IdCliente;
            pago.fecha = pagoModelo.Fecha;
            pago.monto = pagoModelo.Monto;
        }
        
        internal static List<Dominio.Modelos.PagoModelo> EntidadesAModelos(List<PAGO> pagos)
        {
            List<Dominio.Modelos.PagoModelo> modelos = new List<Dominio.Modelos.PagoModelo>();

            foreach (var pago in pagos)
            {
                modelos.Add(EntidadAModelo(pago));
            }

            return modelos;
        }
    }
}