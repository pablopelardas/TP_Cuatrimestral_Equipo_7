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
        
        private Negocio.Servicios.OrdenServicio servicioOrden;
        private Negocio.Servicios.EventoServicio servicioEvento;
        private Negocio.Servicios.ContactoServicio servicioContacto;
        private Negocio.Servicios.ProductoServicio servicioProducto;

        private Components.Calendario calendario;
        private Components.ComboBoxAutoComplete cboTipo;
        private Components.ComboBoxAutoComplete cboCliente;

        private Components.ComboBoxAutoComplete cboProducto;

        protected void Page_Load(object sender, EventArgs e)
        {
            servicioOrden = new Negocio.Servicios.OrdenServicio();
            servicioEvento = new Negocio.Servicios.EventoServicio();
            servicioContacto = new Negocio.Servicios.ContactoServicio();
            servicioProducto = new Negocio.Servicios.ProductoServicio();

            id = Guid.TryParse(Request.QueryString["id"], out id) ? id : Guid.Empty;
            if (Request.QueryString["redirect_to"] != null)
            {
                redirect_to = Request.QueryString["redirect_to"];
            }

            if (Session[OrdenActual] != null)
            {
                orden = (OrdenModelo)Session[OrdenActual];
            }


            if (!IsPostBack)
            {
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
                
                Session[ListaTipoEvento] = servicioEvento.ListarTipoDeEventos();
                Session[ListaClientes] = servicioContacto.Listar().Where(contacto => contacto.Rol == "Cliente");
                Session[ListaProductos] = servicioProducto.Listar();
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
                CargarComponentes();
            }
        }
        private void CargarComponentes()
        {
            CargarCalendario();
            CargarComboBoxTipo();
            CargarComboBoxCliente();
            CargarComboBoxProducto();
            if (!IsPostBack)
            {
                CargarRepeaterDetalle();
            }
            CargarListaDirecciones();
        }
        private void _initComboBoxTipo(DropDownList comboBox)
        {
            comboBox.DataSource = new List<TipoEventoModelo> { new TipoEventoModelo { IdTipoEvento = Guid.Empty, Nombre = "Seleccione un tipo de evento" } }.Concat((IEnumerable<TipoEventoModelo>)Session[ListaTipoEvento]);
            comboBox.DataTextField = "Nombre";
            comboBox.DataValueField = "IdTipoEvento";
            comboBox.DataBind();
        }

        private void _initComboBoxCliente(DropDownList comboBox)
        {
            comboBox.DataSource = 
                new List<ContactoModelo> { new ContactoModelo { Id = Guid.Empty, NombreApellido = "Seleccione un cliente" } }.Concat((IEnumerable<ContactoModelo>)Session[ListaClientes]);
            comboBox.DataTextField = "NombreApellido";
            comboBox.DataValueField = "Id";
            comboBox.DataBind();
        }

        private void _initComboBoxProducto(DropDownList comboBox)
        {
            comboBox.DataSource = 
                new List<ProductoModelo> { new ProductoModelo { IdProducto = Guid.Empty, Nombre = "Seleccione un producto" } }.Concat((IEnumerable<ProductoModelo>)Session[ListaProductos]);
            comboBox.DataTextField = "Nombre";
            comboBox.DataValueField = "IdProducto";
            comboBox.DataBind();
        }
        
        
        private void CargarComboBoxTipo()
        {
            cboTipo = (Components.ComboBoxAutoComplete)LoadControl("~/Backoffice/Components/ComboBoxAutoComplete.ascx");
            cboTipo.ComboID = "cboTipo";
            phComboBoxTipo.Controls.Add(cboTipo);
            cboTipo.InicializarComboBox(_initComboBoxTipo);
        }
        
        private void OnClienteChanged(object sender, EventArgs e)
        {
            DropDownList cbo = (DropDownList)sender;
            Guid idCliente = Guid.TryParse(cbo.SelectedValue, out Guid id) ? id : Guid.Empty;
            
            if (idCliente != Guid.Empty)
            {
                orden.Cliente = Session[ListaClientes] != null ? ((IEnumerable<ContactoModelo>)Session[ListaClientes]).FirstOrDefault(x => x.Id == idCliente) : null;
                Session[OrdenActual] = orden;
                Session[ClienteListaDirecciones] = orden.Cliente?.Direcciones;
            }
            
        }

        private void CargarComboBoxCliente()
        {
            cboCliente = (Components.ComboBoxAutoComplete)LoadControl("~/Backoffice/Components/ComboBoxAutoComplete.ascx");
            cboCliente.ComboID = "cboCliente";
            phComboBoxCliente.Controls.Add(cboCliente);
            cboCliente.InicializarComboBox(_initComboBoxCliente, OnSelectedIndexChanged: OnClienteChanged, AutoPostBack: true);
            if (id != Guid.Empty) cboCliente.Enabled = false;
        }

        private void CargarComboBoxProducto()
        {
            cboProducto = (Components.ComboBoxAutoComplete)LoadControl("~/Backoffice/Components/ComboBoxAutoComplete.ascx");
            cboProducto.ComboID = "cboProducto";
            phComboBoxProducto.Controls.Add(cboProducto);
            cboProducto.InicializarComboBox(_initComboBoxProducto);
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
            Guid IdProducto = Guid.TryParse((string)cboProducto.SelectedValue, out Guid idProducto) ? idProducto : Guid.Empty;
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
            cboTipo.SelectedValue = orden.Evento.TipoEvento.IdTipoEvento.ToString();
            cboCliente.SelectedValue = orden.Cliente.Id.ToString();
            calendario.FechaCalendario = orden.Evento.Fecha;

            // EDITOR
            tiny.Text = orden.Descripcion;
            
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
            orden.TipoEntrega = rbtnTipoEntrega.SelectedValue ?? "R";
            orden.HoraEntrega = TimeSpan.TryParse(inputHora.Value, out TimeSpan hora) ? hora : TimeSpan.Zero;
            
            if (orden.TipoEntrega == "D") // Delivery
            {
                if (string.IsNullOrEmpty(orden.DireccionEntrega.CalleNumero) || string.IsNullOrEmpty(orden.DireccionEntrega.Localidad))
                {
                    throw  new Exception("Debe ingresar una dirección de entrega");
                }
            }
            else
            {
                // limpiar dirección    
                orden.DireccionEntrega = new DireccionModelo();
            }
            
            orden.TipoEntrega = orden.TipoEntrega == "D" ? "Delivery" : "Retiro";
            
            if (orden.HoraEntrega == TimeSpan.Zero)
            {
                throw  new Exception("Debe seleccionar una hora de entrega");
            }

            Guid idTipoEvento = Guid.TryParse((string)cboTipo.SelectedValue, out Guid _idTipoEvento) ? _idTipoEvento : Guid.Empty;
            Guid idCliente = Guid.TryParse((string)cboCliente.SelectedValue, out Guid _idCliente) ? _idCliente : Guid.Empty;

            if (idCliente == Guid.Empty)
            {
                throw  new Exception("Debe seleccionar un cliente");
            }
            
            if (idTipoEvento == Guid.Empty)
            {
                throw  new Exception("Debe seleccionar un tipo de evento");
            }
            
            if (orden.DetalleProductos == null)
            {
                throw  new Exception("Debe agregar productos a la orden");
            }
            
            if (calendario.FechaCalendario == DateTime.MaxValue || calendario.FechaCalendario == DateTime.MinValue)
            {
                throw  new Exception("Debe seleccionar una fecha");
            }
            orden.Evento = new EventoModelo
            {
                Fecha = calendario.FechaCalendario,
                TipoEvento = new TipoEventoModelo { IdTipoEvento = idTipoEvento }
            };

            orden.Cliente = new ContactoModelo { Id = idCliente };
            orden.Descripcion = tiny.Text;

            decimal descuento = 0;
            decimal costoEnvio = 0;
            decimal.TryParse(txtDescuento.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out descuento);
            decimal.TryParse(txtCostoEnvio.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out costoEnvio);

            orden.DescuentoPorcentaje = descuento;
            orden.CostoEnvio = costoEnvio;
            
            orden.Descripcion = tiny.Text;
            
            return orden;
        }



        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            OrdenModelo Ob = ObtenerModeloDesdeFormulario();
            if (id != Guid.Empty)
            {
                Ob.IdOrden = id;
            }
            
            bool guardarDireccionEnContacto = chkGuardarDireccion.Checked;
            servicioOrden.GuardarOrden(Ob, guardarDireccionEnContacto);
            Response.Redirect(redirect_to);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }   

        protected void btnAbrirModalEditar_Click(object sender, EventArgs e)
        {
            // Recuperar id de producto commandArgument
            string idProducto = ((Button)sender).CommandArgument;
            // Guardar id de producto en sesión
            cboProducto.SelectedValue = idProducto;
            cboProducto.Enabled = false;
            txtCantidad.Text = orden.DetalleProductos.Find(x => x.Producto.IdProducto == Guid.Parse(idProducto)).Cantidad.ToString();
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
            cboProducto.Enabled = true;
            cboProducto.SelectedValue = Guid.Empty.ToString();
            txtCantidad.Text = "";
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