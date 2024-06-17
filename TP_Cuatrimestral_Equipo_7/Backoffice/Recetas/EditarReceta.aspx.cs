﻿using Dominio.Modelos;
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

        private Negocio.Servicios.RecetaServicio servicioReceta;
        private Negocio.Servicios.IngredienteServicio servicioIngrediente;

        private Components.ComboBoxAutoComplete cboIngrediente;

        protected void Page_Load(object sender, EventArgs e)
        {
            servicioReceta = new Negocio.Servicios.RecetaServicio();
            servicioIngrediente = new Negocio.Servicios.IngredienteServicio();

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
                CargarComponentes();
                if (receta.IdReceta != Guid.Empty)
                {
                    CargarDatos();
                }
                else
                {
                    //rbtnTipoEntrega.SelectedValue = "R";
                }
            }
            else
            {
                CargarComponentes();
            }
        }
        private void CargarComponentes()
        {
            //CargarComboBoxProducto();
            if (!IsPostBack)
            {
                //CargarRepeaterDetalle();
            }
        }


        private void _initComboBoxProducto(DropDownList comboBox)
        {
            comboBox.DataTextField = "Nombre";
            comboBox.DataValueField = "IdIngrediente";
        }

        private void CargarComboBoxProducto()
        {
            cboIngrediente = (Components.ComboBoxAutoComplete)LoadControl("~/Backoffice/Components/ComboBoxAutoComplete.ascx");
            //phComboBoxIngrediente.Controls.Add(cboIngrediente);
            //cboIngrediente.InicializarComboBox(odsIngredientes, _initComboBoxProducto);
        }

        public void OnAceptarAgregarProducto(object sender, EventArgs e)
        {
            // Agregar producto a la orden
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
                    if ((bool)ViewState["updatingProduct"])
                    {
                        receta.DetalleRecetas.Find(x => x.Ingrediente.IdIngrediente == IdIngrediente).Cantidad = Cantidad;
                        //CargarRepeaterDetalle();
                        return;
                    }
                    receta.DetalleRecetas.Find(x => x.Ingrediente.IdIngrediente == IdIngrediente).Cantidad += Cantidad;
                    //CargarRepeaterDetalle();
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
            //CargarRepeaterDetalle();
        }
        private void CargarDatos()
        {
            if (receta == null)
            {
                return;
            }

            Session[RecetaActual] = receta;

        }

        protected void TotalChanged(object sender, EventArgs e)
        {
            decimal descuento = 0;
            decimal costoEnvio = 0;
            //decimal.TryParse(txtDescuento.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out descuento);
            //decimal.TryParse(txtCostoEnvio.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out costoEnvio);

            //orden.DescuentoPorcentaje = descuento;
            //orden.CostoEnvio = costoEnvio;

        }

        private RecetaModelo ObtenerModeloDesdeFormulario()
        {
            bool hayError = false;

            if (receta.DetalleRecetas == null || receta.DetalleRecetas.Count == 0)
            {
                ((LayoutTailwind)Master)?.Toasts?.Add(new Toast
                {
                    Message = "Debe agregar Ingredientes a la receta",
                    Type = "error"
                });
                hayError = true;
            }

            //receta.Descripcion = txtDescripcion.Text;

            decimal descuento = 0;
            decimal costoEnvio = 0;
            //decimal.TryParse(txtDescuento.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out descuento);
            //decimal.TryParse(txtCostoEnvio.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out costoEnvio);

            //orden.DescuentoPorcentaje = descuento;
            //orden.CostoEnvio = costoEnvio;


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
                ((LayoutTailwind)Master)?.FireToasts();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void editarCantidadProducto_Click(object sender, EventArgs e)
        {
            // Recuperar id de producto commandArgument
            string idIngrediente = ((Button)sender).CommandArgument;
            // Guardar id de producto en sesión
            cboIngrediente.SelectedValue = idIngrediente;
            cboIngrediente.Enabled = false;
            //txtCantidad.Text = receta.DetalleRecetas.Find(x => x.Ingrediente.IdIngrediente == Guid.Parse(idIngrediente)).Cantidad.ToString();
            ViewState["updatingProduct"] = true;
            AbrirModal();
        }

        protected void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            // Recuperar id de producto commandArgument
            string idIngrediente = ((Button)sender).CommandArgument;
            // Eliminar producto de la orden
            receta.DetalleRecetas.RemoveAll(x => x.Ingrediente.IdIngrediente == Guid.Parse(idIngrediente));
            //CargarRepeaterDetalle();
        }


        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            // Limpiar campos
            cboIngrediente.Enabled = true;
            cboIngrediente.SelectedValue = "";
            //txtCantidad.Text = "";
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


    }
}