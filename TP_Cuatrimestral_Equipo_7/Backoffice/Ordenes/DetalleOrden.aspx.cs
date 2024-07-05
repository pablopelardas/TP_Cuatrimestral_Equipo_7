using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio.Modelos;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Ordenes
{
    
    public partial class DetalleOrden : System.Web.UI.Page
    {
        public Dominio.Modelos.OrdenModelo orden;
        public List<Dominio.Modelos.OrdenEstadoModelo> estados;
        public List<HistoricoModelo> historicos;
        public string redirect_to = "/Backoffice/Ordenes";
        
        private Negocio.Servicios.OrdenServicio servicioOrden;
        private Negocio.Servicios.HistoricoServicio servicioHistorico;
        private string OrdenActual = "dtl_orden_actual";
        private string Estados = "dtl_estados";
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScript.GetPostBackEventReference(this, string.Empty);
            servicioHistorico = new Negocio.Servicios.HistoricoServicio();
            
            if (Request.QueryString["redirect_to"] != null)
            {
                redirect_to = Request.QueryString["redirect_to"];
            }

            if (Session[OrdenActual] != null)
            {
                orden = (Dominio.Modelos.OrdenModelo)Session[OrdenActual];
            }

            if (Session[Estados] != null)
            {
                estados = (List<Dominio.Modelos.OrdenEstadoModelo>)Session[Estados];
            }
            if (Session["historicos"] != null)
            {
                historicos = (List<HistoricoModelo>)Session["historicos"];
            }
            
            if (IsPostBack && Request.Form["__EVENTTARGET"] == "btnCancelarOrden")
            {
                try
                {
                    if (orden.Estado.IdOrdenEstado != 5)
                    {
                        servicioOrden = new Negocio.Servicios.OrdenServicio();
                        orden = servicioOrden.CambiarEstado(orden.IdOrden, 5);
                        Session[OrdenActual] = orden;
                        Master?.FireToasts("success", "Orden cancelada correctamente");
                        servicioHistorico.GeneraryGuardarHistorico(orden.IdOrden, "Orden cancelada");
                        historicos = servicioHistorico.ListarPorEntidad(orden.IdOrden);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    Master?.FireToasts("error", "Error al cancelar la orden");
                }
            }

            if (IsPostBack && Request.Form["__EVENTTARGET"] == "abrirHistorial")
            {
                try
                {
                    // HOLA
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    Master?.FireToasts("error", "Error al abrir el historial de la orden");
                }
            }

            if (!IsPostBack)
            {
                Guid id = Guid.TryParse(Request.QueryString["id"], out id) ? id : Guid.Empty;
                servicioOrden = new Negocio.Servicios.OrdenServicio();
                if (id == Guid.Empty) Response.Redirect(redirect_to, false);
                try
                {
                    if (id != Guid.Empty)
                    {
                        orden = servicioOrden.ObtenerPorId(id);
                        estados = servicioOrden.ListarEstadosDeOrden();
                        historicos = servicioHistorico.ListarPorEntidad(orden.IdOrden);
                        if (orden == null) throw new Exception();
                        Session[OrdenActual] = orden;
                        Session[Estados] = estados;
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect(redirect_to, false);
                }

            }
            
            litOrdenExtra.Text = orden?.Descripcion ?? "";
            
        }
        
        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int estado = int.Parse(btn.CommandArgument);
            servicioOrden = new Negocio.Servicios.OrdenServicio();
            orden = servicioOrden.CambiarEstado(orden.IdOrden, estado);
            Session[OrdenActual] = orden;
        }
        protected void GenerateShoppingList(object sender, EventArgs e)
        {
            try
            {
                string subtitle = "#"+orden.ShortId + " - "+ orden.Cliente.NombreApellido + " - " + orden.Evento
                    .Fecha.ToString("dd/MM/yyyy");
                Negocio.Servicios.PdfServicio.GeneratePdfAttachment(orden.ListaCompra.GenerateHTML(subtitle), $"lista-compras-{orden.ShortId}.pdf");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                Master?.FireToasts("error", "Error al generar la lista de compras" );
            }
        }
        
        protected void AvanzarEstado(object sender, EventArgs e)
        {
            try
            {
                servicioOrden = new Negocio.Servicios.OrdenServicio();
                orden = servicioOrden.CambiarEstado(orden.IdOrden, orden.Estado.IdOrdenEstado + 1);
                Session[OrdenActual] = orden;
                string message = $"La orden paso al estado {orden.Estado.Nombre}";
                Master?.FireToasts("success", "Estado avanzado correctamente", message);
                servicioHistorico.GeneraryGuardarHistorico(orden.IdOrden, message);
                if (sm.IsInAsyncPostBack)
                {
                    historicos = servicioHistorico.ListarPorEntidad(orden.IdOrden);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                Master?.FireToasts("error", "Error al avanzar el estado de la orden");
            }
        }

        protected void btnAgregarPago_OnClick(object sender, EventArgs e)
        {
            string formatted = txtMontoPago.Text.Replace(".", ",");
            decimal monto = decimal.TryParse(formatted, out monto) ? monto : 0;
            string tipoPago = ddTipoPago.SelectedValue;
                
            try
            {
                servicioOrden = new Negocio.Servicios.OrdenServicio();
                servicioOrden.AgregarPago(orden, new PagoModelo
                {
                    IdOrden = orden.IdOrden,
                    IdCliente = orden.Cliente.Id,
                    Fecha = DateTime.Now,
                    Monto = monto,
                    TipoPago = tipoPago
                });
                Master?.FireToasts("success", "Pago agregado correctamente");
                orden = servicioOrden.ObtenerPorId(orden.IdOrden);
                Session[OrdenActual] = orden;
            }
            catch (Exception exception)
            {
                Master?.FireToasts("error", "Error al agregar el pago", exception.Message);
            }
        }
    }
}