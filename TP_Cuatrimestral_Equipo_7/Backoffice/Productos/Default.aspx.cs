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
                Negocio.Servicios.ProductoServicio servicio = new Negocio.Servicios.ProductoServicio();
                productos = servicio.Listar();
            }
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            Negocio.Servicios.ProductoServicio servicio = new Negocio.Servicios.ProductoServicio();
            productos = servicio.Listar();
            if (txtBuscar.Text != "")
            {
                productos = productos.Where(c => c.Nombre.Contains(txtBuscar.Text)).ToList();
            }
        }
    }
}