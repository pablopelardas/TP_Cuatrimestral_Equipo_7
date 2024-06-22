using Dominio.Modelos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio.Servicios;
using Newtonsoft.Json;
using TP_Cuatrimestral_Equipo_7.Backoffice.Productos;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Ordenes
{
    public partial class EditarOrden : System.Web.UI.Page
    {
        public Dominio.Modelos.OrdenModelo orden;
        public string FechaSeleccionada;
        public Guid id = Guid.Empty;
        public string redirect_to = "/Backoffice/Ordenes";
        private string OrdenActual = "editorOrden_OrdenActual";
        private string ListaTipoEvento = "editorOrden_ListaTipoEvento";
        private string ListaClientes = "editorOrden_ListaClientes";
        private string ListaProductos = "editorOrden_ListaProductos";
        private string ClienteListaDirecciones = "editorOrden_ClienteListaDirecciones";
        
        private List<string> ToastMessages = new List<string>();
        
        private Negocio.Servicios.OrdenServicio servicioOrden;
        private Negocio.Servicios.EventoServicio servicioEvento;
        private Negocio.Servicios.ContactoServicio servicioContacto;
        private Negocio.Servicios.ProductoServicio servicioProducto;
        private Negocio.Servicios.HistoricoServicio servicioHistorico;

        private Components.Calendario calendario;


        protected void Page_Load(object sender, EventArgs e)
        {
            servicioOrden = new Negocio.Servicios.OrdenServicio();
            servicioEvento = new Negocio.Servicios.EventoServicio();
            servicioContacto = new Negocio.Servicios.ContactoServicio();
            servicioProducto = new Negocio.Servicios.ProductoServicio();
            servicioHistorico = new Negocio.Servicios.HistoricoServicio();
            
            // add script to page

            id = Guid.TryParse(Request.QueryString["id"], out id) ? id : Guid.Empty;
            if (Request.QueryString["redirect_to"] != null)
            {
                redirect_to = Request.QueryString["redirect_to"];
            }

            if (!IsPostBack)
            {
                Session[ClienteListaDirecciones] = null;
                if (id == Guid.Empty)
                {
                    orden = new Dominio.Modelos.OrdenModelo();
                }
                else
                {
                    try
                    {
                        if (id != Guid.Empty)
                        {
                            ddCliente.Enabled = false;
                            orden = servicioOrden.ObtenerPorId(id);
                            if (orden == null)
                            {
                                throw new Exception("Orden no encontrada");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect(redirect_to, false);
                        return;
                    }
                }
                Session[OrdenActual] = orden;
                if (orden.Cliente != null)
                {
                    Session[ClienteListaDirecciones] = orden.Cliente.Direcciones;
                }
                CargarComponentes();
                if (orden.IdOrden != Guid.Empty)
                {
                    CargarDatos();
                }
                else
                {
                    rbtnTipoEntrega.SelectedValue = "R";                    
                }
            }
            else
            {
                if (Session[OrdenActual] != null)
                {
                    orden = (OrdenModelo)Session[OrdenActual];
                }
                CargarComponentes();
            }
        }
        private void CargarComponentes()
        {
            CargarCalendario();
            if (!IsPostBack)
            {
                CargarRepeaterDetalle();
            }
            if (sm.IsInAsyncPostBack)
            {
                if (orden != null && orden.Cliente != null)
                {
                    Session[ClienteListaDirecciones] = orden.Cliente.Direcciones;
                }
            }
            CargarListaDirecciones();
        }
        
        
        protected void OnClienteChanged(object sender, EventArgs e)
        {
            DropDownList cbo = (DropDownList)sender;
            Guid idCliente = Guid.TryParse(cbo.SelectedValue, out Guid id) ? id : Guid.Empty;
            
            if (idCliente != Guid.Empty)
            {
                orden.Cliente = odsCliente.Select()?.Cast<ContactoModelo>().FirstOrDefault(x => x.Id == idCliente);
                Session[OrdenActual] = orden;
                Session[ClienteListaDirecciones] = orden.Cliente?.Direcciones;
                CargarListaDirecciones();
            }
            
        }
        
        
        
        private void CargarRepeaterDetalle()
        {
                rptDetalleOrden.DataSource = null;
                rptDetalleOrden.DataSource = orden.DetalleProductos;
                rptDetalleOrden.DataBind();
        }
        
        private void OnDayClick(object sender, EventArgs e)
        {
            FechaSeleccionada = calendario.FechaCalendario.ToShortDateString();
        }
        private void CargarCalendario()
        {
            calendario = (Backoffice.Components.Calendario)LoadControl("~/Backoffice/Components/Calendario.ascx");
            phCalendario.Controls.Add(calendario);
            calendario.InicializarCalendario(OnDayClick);
            FechaSeleccionada = calendario.FechaCalendario.ToShortDateString();
        }
        
        private void CargarListaDirecciones()
        {
            if (Session[ClienteListaDirecciones] == null)
            {
                phDirecciones.Controls.Clear();
                // add empty label
                Label lbl = new Label();
                lbl.Text = "Seleccione un cliente para ver sus direcciones";
                phDirecciones.Controls.Add(lbl);
                return;
            }
            List<DireccionModelo> direcciones = new List<DireccionModelo> { new DireccionModelo { IdDireccion = Guid.Empty, CalleNumero = "Nueva dirección" } }.Concat((IEnumerable<DireccionModelo>)Session[ClienteListaDirecciones]).ToList();
            phDirecciones.Controls.Clear();
            foreach (DireccionModelo direccion in direcciones)
            {
                Button btn = new Button();
                btn.Text = direccion.CalleNumero;
                btn.CommandArgument = direccion.IdDireccion.ToString();
                btn.CssClass = "text-left w-100 my-1 hover:bg-primary-600 p-2 cursor-pointer";
                btn.Click += OnDireccionClick;
                phDirecciones.Controls.Add(btn);
            }
        }
        
        private void OnDireccionClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            DireccionModelo direccion = ((IEnumerable<DireccionModelo>)Session[ClienteListaDirecciones]).FirstOrDefault(x => x.IdDireccion == Guid.Parse(btn.CommandArgument));
            if (direccion != null)
            {
                txtCalleYNumero.Text = direccion.CalleNumero;
                txtLocalidad.Text = direccion.Localidad;
                txtProvincia.Text = direccion.Provincia;
                txtDepartamento.Text = direccion.Departamento;
                txtPiso.Text = direccion.Piso;
                txtCodigoPostal.Text = direccion.CodigoPostal;

                orden.DireccionEntrega = direccion;

                chkGuardarDireccion.Text = "Modificar dirección del cliente";
            }
            else
            {
                orden.DireccionEntrega = new DireccionModelo();
                txtCalleYNumero.Text = "";
                txtLocalidad.Text = "";
                txtProvincia.Text = "";
                txtDepartamento.Text = "";
                txtPiso.Text = "";
                txtCodigoPostal.Text = "";
                chkGuardarDireccion.Text = "Guardar dirección en cliente";
            }
        }

        public void OnAceptarAgregarProducto(object sender, EventArgs e)
        {
            // Agregar producto a la orden
            try
            {
                Guid IdProducto = Guid.TryParse((string)ddProductos.SelectedValue, out Guid idProducto)
                    ? idProducto
                    : Guid.Empty;
                int Cantidad = int.TryParse(txtCantidad.Text, out int cantidad) ? cantidad : 0;
                if (Cantidad == 0 || IdProducto == Guid.Empty)
                {
                    return;
                }

                if (orden.DetalleProductos == null)
                {
                    orden.DetalleProductos = new List<ProductoDetalleOrdenModelo>();
                }
                else
                {
                    if (orden.DetalleProductos.Exists(x => x.Producto.IdProducto == IdProducto))
                    {
                        if ((bool)ViewState["updatingProduct"])
                        {
                            orden.DetalleProductos.Find(x => x.Producto.IdProducto == IdProducto).Cantidad = Cantidad;
                            CargarRepeaterDetalle();
                            return;
                        }

                        orden.DetalleProductos.Find(x => x.Producto.IdProducto == IdProducto).Cantidad += Cantidad;
                        CargarRepeaterDetalle();
                        return;
                    }
                }

                ProductoModelo producto = servicioProducto.ObtenerPorId(IdProducto);
                ProductoDetalleOrdenModelo detalle = new ProductoDetalleOrdenModelo
                {
                    IdOrden = orden.IdOrden,
                    Producto = producto,
                    Cantidad = Cantidad,
                    PrecioUnitarioActual = producto.Precio,
                    CostoUnitarioActual = producto.Costo,
                    Porciones = producto.Porciones
                };

                orden.DetalleProductos.Add(detalle);
                CargarRepeaterDetalle();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
            finally
            {
                hideModal(null, null);
            } 
        }
        private void CargarDatos()
        {
            if (orden == null)
            {
                return;
            }
            
            orden.TipoEntrega = orden.TipoEntrega == "Delivery" ? "D" : "R";
            Session[OrdenActual] = orden;

            FechaSeleccionada = orden.Evento.Fecha.ToShortDateString();
            rbtnTipoEntrega.SelectedValue = orden.TipoEntrega;

            inputHora.Value = orden.HoraEntrega.ToString();
            txtDescuento.Text = orden.DescuentoPorcentaje.ToString();
            txtCostoEnvio.Text = orden.CostoEnvio.ToString();

            // COMPONENTES
            ddTipoEvento.SelectedValue = orden.Evento.TipoEvento.IdTipoEvento.ToString();
            ddCliente.SelectedValue = orden.Cliente.Id.ToString();
            calendario.FechaCalendario = orden.Evento.Fecha;

            // EDITOR
            txtInfoExtra.Text = orden.Descripcion;
            
            if (orden.TipoEntrega == "D")
            {
                txtCalleYNumero.Text = orden.DireccionEntrega.CalleNumero;
                txtLocalidad.Text = orden.DireccionEntrega.Localidad;
                txtProvincia.Text = orden.DireccionEntrega.Provincia;
                txtDepartamento.Text = orden.DireccionEntrega.Departamento;
                txtPiso.Text = orden.DireccionEntrega.Piso;
                txtCodigoPostal.Text = orden.DireccionEntrega.CodigoPostal;
                FirePostBackEvent("rbtnTipoEntrega_SelectedIndexChanged");
            }
        }

        protected void TotalChanged(object sender, EventArgs e)
        {
            decimal descuento = 0;
            decimal costoEnvio = 0;
            decimal.TryParse(txtDescuento.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out descuento);
            decimal.TryParse(txtCostoEnvio.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out costoEnvio);

            orden.DescuentoPorcentaje = descuento;
            orden.CostoEnvio = costoEnvio;

        }
        
        protected void rbtnTipoEntrega_SelectedIndexChanged(object sender, EventArgs e)
        {
            orden.TipoEntrega = rbtnTipoEntrega.SelectedValue;
        }
        
        protected void TextAddressChanged(object sender, EventArgs e)
        {
            orden.DireccionEntrega.CalleNumero = txtCalleYNumero.Text;
            orden.DireccionEntrega.Localidad = txtLocalidad.Text;
            orden.DireccionEntrega.Provincia = txtProvincia.Text;
            orden.DireccionEntrega.Departamento = txtDepartamento.Text;
            orden.DireccionEntrega.Piso = txtPiso.Text;
            orden.DireccionEntrega.CodigoPostal = txtCodigoPostal.Text;
        }
        
        private OrdenModelo ObtenerModeloDesdeFormulario()
        {

            bool hayError = id != Guid.Empty && !Page.IsValid;
            orden.TipoEntrega = rbtnTipoEntrega.SelectedValue ?? "R";
            orden.HoraEntrega = TimeSpan.TryParse(inputHora.Value, out TimeSpan hora) ? hora : TimeSpan.Zero;

            if (id != Guid.Empty && string.IsNullOrEmpty(txtJustificacion.Text))
            {
                ToastMessages.Add("Debe justificar la modificación de la orden");
                hayError = true;
            }
            
            if (orden.TipoEntrega == "D") // Delivery
            {
                if (string.IsNullOrEmpty(orden.DireccionEntrega.CalleNumero) || string.IsNullOrEmpty(orden.DireccionEntrega.Localidad))
                {
                    
                    ToastMessages.Add("Debe ingresar una dirección de entrega");
                    // add class error
                    if (string.IsNullOrEmpty(orden.DireccionEntrega.CalleNumero))
                    {
                        // keep old class
                        txtCalleYNumero.CssClass = "input-error";
                    }
                    if (string.IsNullOrEmpty(orden.DireccionEntrega.Localidad))
                    {
                        txtLocalidad.CssClass = "input-error";
                    }

                    hayError = true;
                }
                else
                {
                    orden.TipoEntrega = "Delivery";
                }
            }
            else
            {
                // limpiar dirección    
                orden.DireccionEntrega = new DireccionModelo();
                orden.TipoEntrega = "Retiro";
            }
            
            
            if (orden.HoraEntrega == TimeSpan.Zero)
            {
                ToastMessages.Add("Debe seleccionar una hora de entrega");
                hayError = true;
            }

            Guid idTipoEvento = Guid.TryParse((string)ddTipoEvento.SelectedValue, out Guid _idTipoEvento) ? _idTipoEvento : Guid.Empty;
            Guid idCliente = Guid.TryParse((string)ddCliente.SelectedValue, out Guid _idCliente) ? _idCliente : Guid.Empty;

            if (idCliente == Guid.Empty)
            {
                ToastMessages.Add("Debe seleccionar un cliente");
                hayError = true;
            }
            
            if (idTipoEvento == Guid.Empty)
            {
                ToastMessages.Add("Debe seleccionar un tipo de evento");
                hayError = true;
            }
            
            if (orden.DetalleProductos == null || orden.DetalleProductos.Count == 0)
            {
                ToastMessages.Add("Debe agregar productos a la orden");
                hayError = true;
            }
            
            if (calendario.FechaCalendario == DateTime.MaxValue || calendario.FechaCalendario == DateTime.MinValue)
            {
                ToastMessages.Add("Debe seleccionar una fecha de entrega");
                hayError = true;
            }
            orden.Evento = new EventoModelo
            {
                Fecha = calendario.FechaCalendario,
                TipoEvento = new TipoEventoModelo { IdTipoEvento = idTipoEvento }
            };

            orden.Cliente = new ContactoModelo { Id = idCliente };
            orden.Descripcion = txtInfoExtra.Text;

            decimal descuento = 0;
            decimal costoEnvio = 0;
            decimal.TryParse(txtDescuento.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out descuento);
            decimal.TryParse(txtCostoEnvio.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out costoEnvio);

            orden.DescuentoPorcentaje = descuento;
            orden.CostoEnvio = costoEnvio;
            
            if (hayError)
            {
                throw new Exception("Error al guardar la orden");
            }
            
            return orden;
        }


        public void validateJustificacion(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = !string.IsNullOrEmpty(txtJustificacion.Text) && txtJustificacion.Text.Length > 10;
            if (!e.IsValid)
            {
                ToastMessages.Add("La justificación debe tener al menos 10 caracteres");
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                OrdenModelo Ob = ObtenerModeloDesdeFormulario();
                if (id != Guid.Empty)
                {
                    Ob.IdOrden = id;
                }
                
                bool guardarDireccionEnContacto = chkGuardarDireccion.Checked;
                OrdenModelo updatedOrder = servicioOrden.GuardarOrden(Ob, guardarDireccionEnContacto);
                orden.IdOrden = updatedOrder.IdOrden;
                
                
                
                string justificacion = id == Guid.Empty ? "Creación de orden" : txtJustificacion.Text;
                // TODO: ask for justificacion
                
                servicioHistorico.GeneraryGuardarHistorico(orden.IdOrden, justificacion);
                
                Response.Redirect(redirect_to, false);
                Master?.FireToasts("success", "Orden guardada correctamente", "");
                Context.ApplicationInstance.CompleteRequest();

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                // string htmlFromToast = @"<ul class=""list-disc list-inside"">";
                // foreach (string message in ToastMessages)
                // {
                //     htmlFromToast += @"<li class=""text-sm""><span class=""font-semibold"">" + message + "</span></li>";
                // }
                // htmlFromToast += "</ul>";
                
                Master?.FireToasts("error", "Error al guardar la orden", ToastMessages);
                ToastMessages.Clear();
            }
            
            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }   

        protected void editarCantidadProducto_Click(object sender, EventArgs e)
        {
            // Recuperar id de producto commandArgument
            string idProducto = ((Button)sender).CommandArgument;
            // Guardar id de producto en sesión
            ddProductos.SelectedValue = idProducto;
            ddProductos.Enabled = false;
            txtCantidad.Text = orden.DetalleProductos.Find(x => x.Producto.IdProducto == Guid.Parse(idProducto)).Cantidad.ToString();
            ViewState["updatingProduct"] = true;
            AbrirModal();
        }
        
        protected void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            // Recuperar id de producto commandArgument
            string idProducto = ((Button)sender).CommandArgument;
            // Eliminar producto de la orden
            orden.DetalleProductos.RemoveAll(x => x.Producto.IdProducto == Guid.Parse(idProducto));
            CargarRepeaterDetalle();
        }
        
        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            // Limpiar campos
            ddProductos.Enabled = true;
            ddProductos.SelectedValue = "";
            txtCantidad.Text = "";
            // add updatingProduct to viewstate
            
            ViewState["updatingProduct"] = false;
            
            AbrirModal();
        }
        
        private void AbrirModal()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowModal();", true);
        }
        
        private void FirePostBackEvent(string eventTarget, bool async = false)
        {
            if (async)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PostBack", "__doPostBack('" + eventTarget + "','async');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PostBack", "__doPostBack('" + eventTarget + "','');", true);
        }
        
        protected void OnInfoExtraLoad(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "text", "LoadInfoExtra();", true);
        }       
        
        public void hideModal(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "HideModal();", true);
        }
        
    }
}