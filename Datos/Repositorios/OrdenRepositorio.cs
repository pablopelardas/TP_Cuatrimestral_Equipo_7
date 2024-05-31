﻿using Datos.Entidades;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class OrdenRepositorio
    {
        private static string CLIENTE_PREFIX = "cli";
        private static string ESTADO_PREFIX = "est";
        private static string ESTADO_PAGO_PREFIX = "estp";

        public static string GetSelect(string prefix = "")
        {
            string prefixTable = prefix.Length > 0 ? prefix.Replace(".", "_") + '_' : "";
            prefix = prefix.Length > 0 ? prefix + "." : "";
            return $@"
{prefixTable}ORDENES.id_orden as '{prefix}id_orden',
{prefixTable}ORDENES.fecha as '{prefix}fecha',
{prefixTable}ORDENES.tipo_evento as '{prefix}tipo_evento',
{prefixTable}ORDENES.tipo_entrega as '{prefix}tipo_entrega',
{prefixTable}ORDENES.hora_entrega as '{prefix}hora_entrega',
{prefixTable}ORDENES.descripcion as '{prefix}descripcion',
(SELECT SUM (DO.cantidad * DO.producto_precio) FROM DETALLE_ORDENES DO WHERE DO.id_orden = ORDENES.id_orden) as '{prefix}subtotal',
{prefixTable}ORDENES.descuento_porcentaje as '{prefix}descuento_porcentaje',
{prefixTable}ORDENES.costo_envio as '{prefix}costo_envio',
{prefixTable}ORDENES.direccion_entrega as '{prefix}direccion_entrega',
{ContactoRepositorio.GetSelect(prefix + CLIENTE_PREFIX)},
{OrdenEstadoRepositorio.GetSelect(prefix + ESTADO_PREFIX)},
{OrdenEstadoPagoRepositorio.GetSelect(prefix + ESTADO_PAGO_PREFIX)}
";
        }


        public static string GetJoin(string prefix = "")
        {
            prefix = prefix.Length > 0 ? prefix.Replace(".", "_") + '_' : "";
            string clienteAlias = prefix + CLIENTE_PREFIX + "_CONTACTOS";
            string estadoAlias = prefix + ESTADO_PREFIX + "_ORDENES_ESTADOS";
            string estadoPagoAlias = prefix + ESTADO_PAGO_PREFIX + "_ORDENES_PAGO_ESTADOS";

            return $@"
            INNER JOIN CONTACTOS AS {clienteAlias} ON {prefix}ORDENES.ID_CLIENTE = {clienteAlias}.ID_CONTACTO
            INNER JOIN ORDENES_ESTADOS as {estadoAlias} ON {prefix}ORDENES.id_orden_estado = {estadoAlias}.id_orden_estado
            INNER JOIN ORDENES_PAGO_ESTADOS as {estadoPagoAlias} ON {prefix}ORDENES.id_orden_pago_estado = {estadoPagoAlias}.id_orden_pago_estado
            ";
        }

        private OrdenEntidad GetEntidadFromReader(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            prefix = prefix.Length > 0 ? prefix + "." : "";

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
            // orden.
            entidad.cliente = ContactoRepositorio.GetEntidadFromReader(reader, prefix + CLIENTE_PREFIX);
            entidad.estado = OrdenEstadoRepositorio.GetEntidadFromReader(reader, prefix + ESTADO_PREFIX);
            entidad.estado_pago = OrdenEstadoPagoRepositorio.GetEntidadFromReader(reader, prefix + ESTADO_PAGO_PREFIX);

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
                string cmd = $@"
SELECT
{GetSelect()}
FROM ORDENES
{GetJoin()}
";
                datos.SetearConsulta(cmd);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    ordenes.Add(Mappers.OrdenMapper.EntidadAModelo(GetEntidadFromReader(datos.Lector)));
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
                string cmd = $@"
SELECT
{GetSelect()}
FROM ORDENES
{GetJoin()}
WHERE ORDENES.id_orden = @id
";
                datos.SetearConsulta(cmd);
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();
                datos.Lector.Read();
                orden = Mappers.OrdenMapper.EntidadAModelo(GetEntidadFromReader(datos.Lector), true);
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
