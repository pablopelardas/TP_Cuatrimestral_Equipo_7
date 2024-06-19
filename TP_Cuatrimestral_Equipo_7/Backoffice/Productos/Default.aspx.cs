using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Productos
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Dominio.Modelos.ProductoModelo> productos;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListarProductos();
            }
        }

        private void ListarProductos(string categoria = "")
        {
            Negocio.Servicios.ProductoServicio servicio = new Negocio.Servicios.ProductoServicio();
            productos = servicio.Listar();
        }

        protected void ddCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListarProductos(ddCategoria.SelectedValue);
        }

    }
}