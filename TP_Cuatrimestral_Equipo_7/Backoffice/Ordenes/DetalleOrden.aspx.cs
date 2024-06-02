using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Ordenes
{
    public partial class DetalleOrden : System.Web.UI.Page
    {
        public Dominio.Modelos.OrdenModelo orden;
        private Negocio.Servicios.OrdenServicio servicioOrden;
        public string redirect_to = "/Backoffice/Ordenes";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["redirect_back"] != null)
            {
                redirect_to = Request.QueryString["redirect_back"];
            }
            if (!IsPostBack)
            {
                string id = Request.QueryString["id"];
                servicioOrden = new Negocio.Servicios.OrdenServicio();
                if (id == null) Response.Redirect(redirect_to, false);
                try
                {
                    int idInt = Convert.ToInt32(Request.QueryString["id"]);
                    if (idInt > 0)
                    {
                        orden = servicioOrden.ObtenerPorId(idInt);
                        if (orden == null) throw new Exception();
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect(redirect_to, false);
                }

            }

        }
    }
}