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
        private Negocio.Servicios.IngredienteServicio negocioIngrediente;

        public List<UnidadMedidaModelo> unidadesMedida;
        private Negocio.Servicios.UnidadMedidaServicio negocioUnidad;
        public string id = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            negocioIngrediente = new Negocio.Servicios.IngredienteServicio();
            negocioUnidad = new Negocio.Servicios.UnidadMedidaServicio();
            id = Request.QueryString["id"];
            
            if (!IsPostBack)
            {
                if (id == null)
                {
                    ingrediente = new Dominio.Modelos.IngredienteModelo();
                    unidadesMedida = new List<UnidadMedidaModelo>();
                    BindDDL(unidadesMedida);
                } else
                {
                    try
                    {
                        int idInt = Convert.ToInt32(Request.QueryString["id"]);
                        if (idInt > 0)
                        {
                            ingrediente = negocioIngrediente.ObtenerPorId(idInt);
                            BindDDL(unidadesMedida);
                            if (ingrediente != null)
                            {
                                txtNombre.Text = ingrediente.Nombre;
                                txtCantidad.Text = ingrediente.Cantidad.ToString();
                                txtCosto.Text = ingrediente.Costo.ToString();
                                txtProveedor.Text = ingrediente.Proveedor;

                                ddlUnidad.SelectedIndex = ingrediente.Unidad.Id - 1;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("/Backoffice/Ingredientes", false);
                    }
                }

            }

        }

        private void BindDDL(List<UnidadMedidaModelo> unidadMedidaModelo)
        {
            unidadesMedida = negocioUnidad.Listar();
            ddlUnidad.DataSource = unidadesMedida;
            ddlUnidad.DataTextField = "Nombre";
            ddlUnidad.DataValueField = "Id";
            ddlUnidad.DataBind();
        }

        private IngredienteModelo ObtenerModeloDesdeFormulario()
        {
            return new IngredienteModelo
            {
                Nombre = txtNombre.Text,
                Cantidad = Convert.ToDouble(txtCantidad.Text),
                
                Costo = Convert.ToDecimal(txtCosto.Text),
                Proveedor = txtProveedor.Text,
                Unidad = negocioUnidad.ObtenerPorId(ddlUnidad.SelectedIndex + 1)
            };
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (id != null)
            {
                IngredienteModelo Ob = ObtenerModeloDesdeFormulario();
                negocioIngrediente.Modificar(Ob);
            }
            else
            {
                negocioIngrediente.Agregar(ObtenerModeloDesdeFormulario());
            }
        }
    }
}