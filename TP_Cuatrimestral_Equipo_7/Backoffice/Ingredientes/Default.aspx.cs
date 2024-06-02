using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Ingredientes
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Dominio.Modelos.IngredienteModelo> ingredientes;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ( !IsPostBack)
            {
                Negocio.Servicios.IngredienteServicio servicio = new Negocio.Servicios.IngredienteServicio();
                ingredientes = servicio.Listar();
            }
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            Negocio.Servicios.IngredienteServicio servicio = new Negocio.Servicios.IngredienteServicio();
            ingredientes = servicio.Listar();
            if (txtBuscar.Text != "")
            {
                ingredientes = ingredientes.Where(c => c.Nombre.Contains(txtBuscar.Text)).ToList();
            }
        }
    }
}