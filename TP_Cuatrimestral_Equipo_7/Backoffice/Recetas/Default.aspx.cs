using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Recetas
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Dominio.Modelos.RecetaModelo> recetas;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ListarRecetas();

            }
        }

        private void ListarRecetas(string categoria = "")
        {
            Negocio.Servicios.RecetaServicio servicio = new Negocio.Servicios.RecetaServicio();
            recetas = servicio.Listar(categoria);
        }
        protected void ddCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListarRecetas(ddCategoria.SelectedValue);
        }
    }
}