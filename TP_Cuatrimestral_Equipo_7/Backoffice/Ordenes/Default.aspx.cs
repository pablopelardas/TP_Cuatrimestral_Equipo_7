using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Ordenes
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Dominio.Modelos.OrdenModelo> ordenes;
        private int anioActual = DateTime.Now.Year;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                for (int i = anioActual - 5; i <= anioActual + 5; i++)
                {
                    ddlAnio.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddlAnio.SelectedValue = anioActual.ToString();
                ListarOrdenes();

            }
        }

        private void ListarOrdenes()
        {
            Negocio.Servicios.OrdenServicio servicio = new Negocio.Servicios.OrdenServicio();
            ordenes = servicio.Listar();
            //// enum
            //if (ddlFiltro.SelectedValue != "0")
            //{
            //    switch (ddlFiltro.SelectedValue)
            //    {
            //        case "1":
            //            // sin pagos
            //            //ordenes = ordenes.Where(o => o.Pagos.Count == 0).ToList();
            //            break;
            //        case "2":
            //            // pagos de reserva
            //            //ordenes = ordenes.where(o => o.pagos.count > 0).tolist();
            //            break;
            //        case "3":
            //            // parcialmente pagado
            //            //ordenes = ordenes.where(o => o.pagos.sum(p => p.monto) < o.total).tolist();
            //            break;
            //        case "4":
            //            // pagado
            //            //ordenes = ordenes.where(o => o.pagos.sum(p => p.monto) == o.total).tolist();
            //            break;
            //        case "5":
            //            // cancelado
            //            //ordenes = ordenes.where(o => o.pagos.sum(p => p.monto) < o.total).tolist();
            //            break;
            //        case "6":
            //            // completado (pagado y entregado)
            //            //ordenes = ordenes.where(o => o.pagos.sum(p => p.monto) == o.total && o.fechaentrega != null).tolist();
            //        default:
            //            break;
            //    }
            //}

            //// anio
            //if (ddlAnio.SelectedValue != "0")
            //{
            //    int anio = int.Parse(ddlAnio.SelectedValue);
            //    ordenes = ordenes.Where(o => o.Fecha.Year == anio).ToList();
            //}

            //// mes
            //if (ddlMes.SelectedValue != "0")
            //{
            //    int mes = int.Parse(ddlMes.SelectedValue);
            //    ordenes = ordenes.Where(o => o.Fecha.Month == mes).ToList();
            //}
        }
        protected void ddlFiltro_SelectedIndexChanged (object sender, EventArgs e)
        {

            ListarOrdenes();
        }
    
        protected void ddlAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListarOrdenes();
        }

        protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListarOrdenes();
        }
    }
}