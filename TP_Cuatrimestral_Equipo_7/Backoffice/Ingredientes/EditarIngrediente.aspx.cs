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
        public Guid id = Guid.Empty;

        private Negocio.Servicios.UnidadMedidaServicio negocioUnidad;
        private Negocio.Servicios.IngredienteServicio negocioIngrediente;

        public List<UnidadMedidaModelo> unidadesMedida;

        public string redirect_to = "/Backoffice/Ingredientes";
        protected void Page_Load(object sender, EventArgs e)
        {
            negocioUnidad = new Negocio.Servicios.UnidadMedidaServicio();
            negocioIngrediente = new Negocio.Servicios.IngredienteServicio();

            id = Guid.TryParse(Request.QueryString["id"], out id) ? id : Guid.Empty;
            if (Request.QueryString["redirect_to"] != null)
            {
                redirect_to = Request.QueryString["redirect_to"];
            }

            if (!IsPostBack)
            {
                cargarUnidadesMedida();
                if (id == null)
                {
                    ingrediente = new Dominio.Modelos.IngredienteModelo();
                }
                else
                {
                    try
                    {

                        if (id != Guid.Empty)
                        {
                            ingrediente = negocioIngrediente.ObtenerPorId(id);
                            if (ingrediente != null)
                            {
                                txtNombre.Text = ingrediente.Nombre;
                                txtCantidad.Text = ingrediente.Cantidad.ToString();
                                txtCosto.Text = ingrediente.Costo.ToString();
                                txtProveedor.Text = ingrediente.Proveedor;

                                ddUnidad.SelectedValue = unidadesMedida.Find(x => x.Id == ingrediente.Unidad.Id).Id.ToString();
                            }
                        }
                        else
                        {
                            ingrediente = new Dominio.Modelos.IngredienteModelo();
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("/Backoffice/Ingredientes", false);
                    }
                }
            }
            if (Session["UnidadesMedida"] != null)
            {
                unidadesMedida = (List<UnidadMedidaModelo>)Session["UnidadesMedida"];
            }
        }

        private void cargarUnidadesMedida()
        {
            unidadesMedida = negocioUnidad.Listar();
            Session["UnidadesMedida"] = unidadesMedida;
        }

        private IngredienteModelo ObtenerModeloDesdeFormulario()
        {
            return new IngredienteModelo
            {
                Nombre = txtNombre.Text,
                Cantidad = decimal.TryParse(txtCantidad.Text, out decimal cantidad) ? cantidad : 0,

                Costo = decimal.TryParse(txtCosto.Text, out decimal costo) ? costo : 0,
                Proveedor = txtProveedor.Text,
                Unidad = unidadesMedida.Find(x => x.Id.ToString() == ddUnidad.SelectedValue)
            };
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    IngredienteModelo ingrediente = ObtenerModeloDesdeFormulario();
                    ingrediente.IdIngrediente = id;
                    negocioIngrediente.Modificar(ingrediente);
                }
                else
                {
                    negocioIngrediente.Agregar(ObtenerModeloDesdeFormulario());
                }
                Response.Redirect(redirect_to);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                ((LayoutTailwind)Master)?.FireToasts();
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}