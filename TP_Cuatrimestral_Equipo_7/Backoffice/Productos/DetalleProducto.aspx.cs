using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Productos
{
    public partial class DetalleProducto : System.Web.UI.Page
    {
        public Dominio.Modelos.ProductoModelo producto;
        private Negocio.Servicios.ProductoServicio negocio;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Guid id = Guid.TryParse(Request.QueryString["id"], out id) ? id : Guid.Empty;
                negocio = new Negocio.Servicios.ProductoServicio();
                if (id == Guid.Empty) Response.Redirect("/Backoffice/Producto", false);
                try
                {
                    if (id != Guid.Empty)
                    {
                        producto = negocio.ObtenerPorId(id);
                        if (producto != null)
                        {
                            lblNombre.Text = producto.Nombre;
                            lblDescripcion.Text = producto.Descripcion.ToString();
                            lblPorciones.Text = producto.Porciones.ToString();
                            lblHorasTrabajo.Text = producto.HorasTrabajo.ToString();
                            lblTipoPrecio.Text = producto.TipoPrecio.ToString();
                            lblValorPrecio.Text = producto.ValorPrecio.ToString();
                            lblCategoria.Text = producto.Categoria.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("/Backoffice/Productos", false);
                }

            }

        }
    }
}