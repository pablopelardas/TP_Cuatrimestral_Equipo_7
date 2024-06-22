using Dominio.Modelos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using TP_Cuatrimestral_Equipo_7.Backoffice.Ingredientes;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Recetas
{
    public partial class EditarReceta : System.Web.UI.Page
    {
        public Dominio.Modelos.RecetaModelo receta;
        public Guid id = Guid.Empty;
        public string redirect_to = "/Backoffice/Recetas";
        private string RecetaActual = "editorReceta_RecetaActual";
        private string ListaIngredientes = "editorReceta_ListaIngredientes";
        private string Categorias = "editorReceta_Categorias";
        private List<string> ToastMessages = new List<string>();

        private Negocio.Servicios.RecetaServicio servicioReceta;
        private Negocio.Servicios.IngredienteServicio servicioIngrediente;
        private Negocio.Servicios.CategoriaServicio servicioCategoria;

        private Components.ComboBoxAutoComplete cboIngrediente;

        private void CargarRepeaterDetalle()
        {
            rptDetalleReceta.DataSource = null;
            rptDetalleReceta.DataSource = receta.DetalleRecetas;
            rptDetalleReceta.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            servicioReceta = new Negocio.Servicios.RecetaServicio();
            servicioIngrediente = new Negocio.Servicios.IngredienteServicio();
            servicioCategoria = new Negocio.Servicios.CategoriaServicio();
         
            id = Guid.TryParse(Request.QueryString["id"], out id) ? id : Guid.Empty;
            if (Request.QueryString["redirect_to"] != null)
            {
                redirect_to = Request.QueryString["redirect_to"];
            }

            if (Session[RecetaActual] != null)
            {
                receta = (RecetaModelo)Session[RecetaActual];
            }


            if (!IsPostBack)
            {
                if (id == Guid.Empty)
                {
                    receta = new Dominio.Modelos.RecetaModelo();
                    receta.DetalleRecetas = new List<Dominio.Modelos.IngredienteDetalleRecetaModelo>();
                }
                else
                {
                    try
                    {
                        if (id != Guid.Empty)
                        {
                            receta = servicioReceta.ObtenerPorId(id);
                            if (receta == null)
                            {
                                throw new Exception("Receta no encontrada");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect(redirect_to, false);
                        return;
                    }
                }

                Session[RecetaActual] = receta;
                Session[ListaIngredientes] = servicioIngrediente.Listar();
                Session[Categorias] = servicioCategoria.Listar("Receta");

                CargarComponentes();

                if (receta.IdReceta != Guid.Empty)
                {
                    CargarDatos();
                }
            }
            else
            {
                CargarComponentes();
            }
        }
        private void CargarComponentes()
        {
            CargarComboBoxIngrediente();
            if (!IsPostBack)
            {
                CargarRepeaterDetalle();
            }
        }

        
        private void _initComboBoxIngrediente(DropDownList comboBox)
        {
            comboBox.DataTextField = "Nombre";
            comboBox.DataValueField = "IdIngrediente";
        }

        private void CargarComboBoxIngrediente()
        {
            cboIngrediente = (Components.ComboBoxAutoComplete)LoadControl("~/Backoffice/Components/ComboBoxAutoComplete.ascx");
            phComboBoxIngrediente.Controls.Add(cboIngrediente);
            cboIngrediente.InicializarComboBox(odsIngredientes, _initComboBoxIngrediente);
        }

        public void OnAceptarAgregarIngrediente(object sender, EventArgs e)
        {
            try
            {
                Guid IdIngrediente = Guid.TryParse((string)cboIngrediente.SelectedValue, out Guid idIngrediente) ? idIngrediente : Guid.Empty;
                int Cantidad = int.TryParse(txtCantidad.Text, out int cantidad) ? cantidad : 0;
                if (Cantidad == 0 || IdIngrediente == Guid.Empty)
                {
                    return;
                }
                if (receta.DetalleRecetas == null)
                {
                    receta.DetalleRecetas = new List<IngredienteDetalleRecetaModelo>();
                }
                else
                {
                    if (receta.DetalleRecetas.Exists(x => x.Ingrediente.IdIngrediente == IdIngrediente))
                    {
                        if ((bool)ViewState["updatingIngrediente"])
                        {
                            receta.DetalleRecetas.Find(x => x.Ingrediente.IdIngrediente == IdIngrediente).Cantidad = Cantidad;
                            CargarRepeaterDetalle();
                            return;
                        }
                        receta.DetalleRecetas.Find(x => x.Ingrediente.IdIngrediente == IdIngrediente).Cantidad += Cantidad;
                        CargarRepeaterDetalle();
                        return;
                    }
                }
                IngredienteModelo ingrediente = servicioIngrediente.ObtenerPorId(IdIngrediente);
                IngredienteDetalleRecetaModelo detalle = new IngredienteDetalleRecetaModelo
                {
                    IdReceta = receta.IdReceta,
                    Ingrediente = ingrediente,
                    Cantidad = Cantidad,
                };

                receta.DetalleRecetas.Add(detalle);
                CargarRepeaterDetalle();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                hideModal(null, null);
            }
        }
        private void CargarDatos()
        {
            if (receta == null)
            {
                return;
            }
            txtNombre.Text = receta.Nombre;
            ddCategoria.SelectedValue = receta.Categoria.Id.ToString();
            tiny.Text = receta.Descripcion;
            Session[RecetaActual] = receta;
        }


        private RecetaModelo ObtenerModeloDesdeFormulario()
        {
            bool hayError = false;

            if (receta.DetalleRecetas == null || receta.DetalleRecetas.Count == 0)
            {
                ToastMessages.Add("Debe agregar Ingredientes a la receta");
                hayError = true;
            }

            Guid idCategoria = Guid.TryParse((string)ddCategoria.SelectedValue, out Guid _idTipoEvento) ? _idTipoEvento : Guid.Empty;

            if (idCategoria == Guid.Empty)
            {
                ToastMessages.Add("Debe seleccionar una categoria");
                ddCategoria.CssClass += " input-error";
                hayError = true;
            }
            List<CategoriaModelo> categorias = (List<CategoriaModelo>)Session[Categorias];
            receta.Nombre = txtNombre.Text;
            receta.Categoria = categorias.Find(x => x.Id == idCategoria);
            receta.Descripcion = tiny.Text;

            if (hayError)
            {
                throw new Exception("Error al guardar la receta");
            }

            return receta;
        }



        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                RecetaModelo Ob = ObtenerModeloDesdeFormulario();
                if (id != Guid.Empty)
                {
                    Ob.IdReceta = id;
                    servicioReceta.Modificar(Ob);
                }
                else
                {
                    servicioReceta.Agregar(Ob);
                }

                Response.Redirect(redirect_to);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                ((LayoutTailwind)Master)?.FireToasts("error", "Error al guardar la receta");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void editarCantidadIngrediente_Click(object sender, EventArgs e)
        {
            // Recuperar id de producto commandArgument
            string idIngrediente = ((Button)sender).CommandArgument;
            // Guardar id de producto en sesión
            cboIngrediente.SelectedValue = idIngrediente;
            cboIngrediente.Enabled = false;
            txtCantidad.Text = receta.DetalleRecetas.Find(x => x.Ingrediente.IdIngrediente == Guid.Parse(idIngrediente)).Cantidad.ToString();
            ViewState["updatingIngrediente"] = true;
            AbrirModal();
        }

        protected void btnEliminarIngrediente_Click(object sender, EventArgs e)
        {
            // Recuperar id de producto commandArgument
            string idIngrediente = ((Button)sender).CommandArgument;
            // Eliminar producto de la orden
            receta.DetalleRecetas.RemoveAll(x => x.Ingrediente.IdIngrediente == Guid.Parse(idIngrediente));
            CargarRepeaterDetalle();
        }


        protected void btnAgregarIngrediente_Click(object sender, EventArgs e)
        {
            // Limpiar campos
            cboIngrediente.Enabled = true;
            cboIngrediente.SelectedValue = "";
            txtCantidad.Text = "";
            // add updatingProduct to viewstate

            ViewState["updatingIngrediente"] = false;

            AbrirModal();
        }

        private void AbrirModal()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowModal();", true);
        }

        private void FirePostBackEvent(string eventTarget)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PostBack", "__doPostBack('" + eventTarget + "','');", true);
        }

        protected void OnTinyLoad(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "text", "LoadTiny();", true);
        }
        public void hideModal(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "HideModal();", true);
        }

    }
}