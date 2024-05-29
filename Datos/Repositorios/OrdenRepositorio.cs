using Datos.Entidades;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class OrdenRepositorio
    {

        private string select = @"
SELECT 
   O.id_orden,
   O.fecha,
   O.tipo_evento,
   O.tipo_entrega,
   O.hora_entrega,
   O.descripcion,
   (SELECT SUM (DO.cantidad * DO.producto_precio) FROM DETALLE_ORDENES DO WHERE DO.id_orden = O.id_orden) AS subtotal,
    O.descuento_porcentaje,
    O.costo_envio,
    O.direccion_entrega,
    C.id_contacto as 'cliente.id_contacto',
    C.nombre_apellido as 'cliente.nombre_apellido',
    C.tipo as 'cliente.tipo',
    C.correo as 'cliente.correo',
    C.telefono as 'cliente.telefono',
    C.fuente as 'cliente.fuente',
    C.direccion as 'cliente.direccion',
    C.producto_que_provee as 'cliente.producto_que_provee',
    C.desea_recibir_correos as 'cliente.desea_recibir_correos',
    C.desea_recibir_whatsapp as 'cliente.desea_recibir_whatsapp',
    C.informacion_personal as 'cliente.informacion_personal',
    OE.id_orden_estado as 'estado.id_orden_estado',
    OE.nombre as 'estado.nombre',
    OPE.id_orden_pago_estado as 'estado_pago.id_orden_pago_estado',
    OPE.nombre as 'estado_pago.nombre'
FROM ORDENES O
INNER JOIN CONTACTOS C ON O.ID_CLIENTE = C.ID_CONTACTO
INNER JOIN ORDENES_ESTADOS OE ON O.id_orden_estado = OE.id_orden_estado
INNER JOIN ORDENES_PAGO_ESTADOS OPE ON O.id_orden_pago_estado = OPE.id_orden_pago_estado
                ";

        private OrdenEntidad getEntidadFromReader(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            OrdenEntidad entidad = new OrdenEntidad();
            // OBLIGATORIOS
            entidad.id_orden = (int)reader[$"{prefix}id_orden"];
            entidad.fecha = (DateTime)reader[$"{prefix}fecha"];
            entidad.tipo_evento = (string)reader[$"{prefix}tipo_evento"];
            entidad.tipo_entrega = (string)reader[$"{prefix}tipo_entrega"];
            entidad.descripcion = (string)reader[$"{prefix}descripcion"];
            entidad.hora_entrega = reader[$"{prefix}hora_entrega"].ToString();
            entidad.subtotal = (decimal)reader[$"{prefix}subtotal"];

            // OPCIONALES
            entidad.descuento_porcentaje = reader[$"{prefix}descuento_porcentaje"] == DBNull.Value ? 0 : (decimal)reader[$"{prefix}descuento_porcentaje"];
            entidad.costo_envio = reader[$"{prefix}costo_envio"] == DBNull.Value ? 0 : (decimal)reader[$"{prefix}costo_envio"];
            entidad.direccion_entrega = reader[$"{prefix}direccion_entrega"] == DBNull.Value ? "" : (string)reader[$"{prefix}direccion_entrega"];

            // OTRAS ENTIDADES
            entidad.cliente = ContactoRepositorio.getEntidadFromReader(reader, prefix + "cliente.");
            entidad.estado = OrdenEstadoRepositorio.getEntidadFromReader(reader, prefix + "estado.");
            entidad.estado_pago = OrdenEstadoPagoRepositorio.getEntidadFromReader(reader, prefix + "estado_pago.");

            return entidad;
        }

        private void ParametrizarEntidad(OrdenEntidad entidad, AccesoDatos datos)
        {
            datos.SetearParametro("@id_orden", entidad.id_orden);
            datos.SetearParametro("@fecha", entidad.fecha);
            datos.SetearParametro("@tipo_evento", entidad.tipo_evento);
            datos.SetearParametro("@tipo_entrega", entidad.tipo_entrega);
            datos.SetearParametro("@descripcion", entidad.descripcion);
            datos.SetearParametro("@hora_entrega", entidad.hora_entrega);
            datos.SetearParametro("@subtotal", entidad.subtotal);

            datos.SetearParametro("@descuento_porcentaje", entidad.descuento_porcentaje);
            datos.SetearParametro("@costo_envio", entidad.costo_envio);
            datos.SetearParametro("@direccion_entrega", entidad.direccion_entrega);

            datos.SetearParametro("@id_cliente", entidad.cliente.id_contacto);
        }
        public List<Dominio.Modelos.OrdenModelo> Listar()
        {
            List<Dominio.Modelos.OrdenModelo> ordenes = new List<Dominio.Modelos.OrdenModelo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string cmd = select;
                datos.SetearConsulta(cmd);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    ordenes.Add(Mappers.OrdenMapper.EntidadAModelo(getEntidadFromReader(datos.Lector)));
                }
                return ordenes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public OrdenModelo ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            OrdenModelo orden = new OrdenModelo();
            try
            {
                string cmd = select + " WHERE O.id_orden = @id";
                datos.SetearConsulta(cmd);
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();
                datos.Lector.Read();
                orden = Mappers.OrdenMapper.EntidadAModelo(getEntidadFromReader(datos.Lector), true);
                return orden;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public void Agregar(OrdenModelo orden)
        {
            throw new NotImplementedException();
        }

        public void Modificar(OrdenModelo orden)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

    }
}
