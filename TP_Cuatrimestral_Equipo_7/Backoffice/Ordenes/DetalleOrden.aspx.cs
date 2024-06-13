using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Dominio.Modelos;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Ordenes
{
    public partial class DetalleOrden : System.Web.UI.Page
    {
        public Dominio.Modelos.OrdenModelo orden;
        public List<Dominio.Modelos.OrdenEstadoModelo> estados;
        public string redirect_to = "/Backoffice/Ordenes";
        
        private Negocio.Servicios.OrdenServicio servicioOrden;
        private string OrdenActual = "dtl_orden_actual";
        private string Estados = "dtl_estados";

        protected void Page_Load(object sender, EventArgs e)
        {
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
            
            phEstados.Controls.Clear();
                
            foreach (OrdenEstadoModelo estado in estados)
            {     
                Button btn = new Button
                {
                    Text = estado.Nombre,
                    CssClass = $"w-full justify-center {estado.PillClass} cursor-pointer",
                    CommandName = "CambiarEstado",
                    CommandArgument = estado.IdOrdenEstado.ToString(),
                };
                btn.Click += Btn_Click;
                phEstados.Controls.Add(btn);
            }
        }
        
        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int estado = int.Parse(btn.CommandArgument);
            servicioOrden = new Negocio.Servicios.OrdenServicio();
            orden = servicioOrden.CambiarEstado(orden.IdOrden, estado);
            Session[OrdenActual] = orden;
        }
    }
}