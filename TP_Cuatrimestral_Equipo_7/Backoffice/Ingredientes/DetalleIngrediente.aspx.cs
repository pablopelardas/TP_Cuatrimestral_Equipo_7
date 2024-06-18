using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Ingredientes
{
    public partial class DetalleIngrediente : System.Web.UI.Page
    {
        public Dominio.Modelos.IngredienteModelo ingrediente;
        private Negocio.Servicios.IngredienteServicio servicio;
        public string redirect_to = "/Backoffice/Ingredientes";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["redirect_to"] != null)
                {
                    redirect_to = Request.QueryString["redirect_to"];
                }

                if (!IsPostBack)
                {
                    Guid id = Guid.TryParse(Request.QueryString["id"], out id) ? id : Guid.Empty;
                    servicio = new Negocio.Servicios.IngredienteServicio();
                    if (id == Guid.Empty) Response.Redirect(redirect_to, false);
                    try
                    {
                        if (id != Guid.Empty)
                        {
                            ingrediente = servicio.ObtenerPorId(id);
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
}