using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Ingredientes
{
    public partial class EditarIngrediente : System.Web.UI.Page
    {
        public Dominio.Modelos.IngredienteModelo ingrediente;
        private Negocio.Servicios.IngredienteServicio negocio;
        public string id = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            negocio = new Negocio.Servicios.IngredienteServicio();
            id = Request.QueryString["id"];
            
            if (!IsPostBack)
            {
                if (id == null)
                {
                    ingrediente = new Dominio.Modelos.IngredienteModelo();
                } else
                {
                    try
                    {
                        int idInt = Convert.ToInt32(Request.QueryString["id"]);
                        if (idInt > 0)
                        {
                            ingrediente = negocio.ObtenerPorId(idInt);
                            if (ingrediente != null)
                            {
                                //ddlTipo.SelectedValue = ingrediente.Rol == "Cliente" ? "1" : "2";
                                txtNombre.Text = ingrediente.Nombre;
                                txtCantidad.Text = ingrediente.Cantidad.ToString();
                                txtUnidadMedida.Text = ingrediente.Unidad.Nombre;
                                txtCosto.Text = ingrediente.Costo.ToString();
                                txtProveedor.Text = ingrediente.Proveedor;
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

        private IngredienteModelo ObtenerModeloDesdeFormulario()
        {
            return new IngredienteModelo
            {
                Nombre = txtNombre.Text,
                Cantidad = Convert.ToDouble(txtCantidad.Text),
                //Unidad = txtUnidadMedida.Text,
                Costo = Convert.ToDecimal(txtCosto.Text),
                Proveedor = txtProveedor.Text
            };
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (id != null)
            {
                IngredienteModelo Ob = ObtenerModeloDesdeFormulario();
                Ob.IdIngrediente = Convert.ToInt32(id);
                negocio.Modificar(Ob);
            }
            else
            {
                negocio.Agregar(ObtenerModeloDesdeFormulario());
            }
        }
    }
}