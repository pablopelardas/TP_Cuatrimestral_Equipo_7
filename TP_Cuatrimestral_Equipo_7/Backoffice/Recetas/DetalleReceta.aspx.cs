using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Dominio.Modelos;
using System.Web.UI;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Recetas
{
    public partial class DetalleReceta : System.Web.UI.Page
    {
        public Dominio.Modelos.RecetaModelo receta;
        public List<Dominio.Modelos.IngredienteDetalleRecetaModelo> ingredientes;
        public string redirect_to = "/Backoffice/Recetas";
        
        private Negocio.Servicios.RecetaServicio servicio;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["redirect_to"] != null)
            {
                redirect_to = Request.QueryString["redirect_to"];
            }

            if (!IsPostBack)
            {
                Guid id = Guid.TryParse(Request.QueryString["id"], out id) ? id : Guid.Empty;
                servicio = new Negocio.Servicios.RecetaServicio();
                if (id == Guid.Empty) Response.Redirect(redirect_to, false);
                try
                {
                    if (id != Guid.Empty)
                    {
                        receta = servicio.ObtenerPorId(id);
                        ingredientes = receta.DetalleRecetas;
                        //Session[RecetaActual] = receta;
                        //Session[Ingredientes] = ingredientes;
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