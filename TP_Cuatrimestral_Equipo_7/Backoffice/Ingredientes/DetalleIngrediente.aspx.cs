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
        private Negocio.Servicios.IngredienteServicio negocio;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["id"];
                negocio = new Negocio.Servicios.IngredienteServicio();
                if (id == null) Response.Redirect("/Backoffice/Ingredientes", false);
                try
                {
                    int idInt = Convert.ToInt32(Request.QueryString["id"]);
                    if (idInt > 0)
                    {
                        ingrediente = negocio.ObtenerPorId(idInt);
                        if (ingrediente != null)
                        {
                            lblNombre.Text = ingrediente.Nombre;
                            lblCantidad.Text = ingrediente.Cantidad.ToString();
                            lblUnidadAbreviatura.Text = ingrediente.Unidad.Abreviatura;
                            lblCosto.Text = ingrediente.Costo.ToString();
                            lblProveedor.Text = ingrediente.Proveedor;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("/Backoffice/ingredientes", false);
                }

            }

        }
    }
}