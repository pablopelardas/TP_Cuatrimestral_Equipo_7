using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Productos
{
    public partial class EditarProducto : System.Web.UI.Page
    {

        public Dominio.Modelos.ProductoModelo producto;
        private Negocio.Servicios.ProductoServicio negocioProducto;
        public string id = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            negocioProducto = new Negocio.Servicios.ProductoServicio();
            id = Request.QueryString["id"];

            if (!IsPostBack)
            {
                if (id == null)
                {
                    producto = new Dominio.Modelos.ProductoModelo();
                }
                else
                {
                    try
                    {
                        int idInt = Convert.ToInt32(Request.QueryString["id"]);
                        if (idInt > 0)
                        {
                            producto = negocioProducto.ObtenerPorId(idInt);
                            if (producto != null)
                            {                               
                                txtNombre.Text = producto.Nombre;
                                txtDescripcion.Text = producto.Descripcion;
                                txtPorciones.Text = producto.Porciones.ToString();
                                txtHorasTrabajo.Text = producto.HorasTrabajo.ToString();
                                txtTipoPrecio.Text = producto.TipoPrecio;
                                txtValorPrecio.Text = producto.ValorPrecio.ToString();
                                ddlCategoria.SelectedValue = producto.Categoria.ToString() == "Categoria" ? "1" : "2";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
        }
        private ProductoModelo ObtenerModeloDesdeFormulario()
        {
            return new ProductoModelo
            {
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text,
                Porciones = Convert.ToInt32(txtPorciones.Text),
                HorasTrabajo = Convert.ToDecimal(txtHorasTrabajo.Text),
                TipoPrecio = txtTipoPrecio.Text,
                ValorPrecio = Convert.ToDecimal(txtValorPrecio.Text),
                Categoria = ddlCategoria = "Categoria" ? "uno" : "dos"
            };
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (id != null)
            {
                ProductoModelo producto = ObtenerModeloDesdeFormulario();
                producto.IdProducto = Convert.ToInt32(id);
                negocioProducto.Modificar(producto);
            }
            else
            {
                negocioProducto.Agregar(ObtenerModeloDesdeFormulario());
            }
        }
    }
}