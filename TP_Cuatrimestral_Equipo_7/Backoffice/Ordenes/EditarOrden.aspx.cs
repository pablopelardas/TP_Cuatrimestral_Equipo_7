using Dominio.Modelos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
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
                                Response.Redirect(redirect_to);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                Session[OrdenActual] = orden;
                CargarComponentes();
                if (orden != null && orden.IdOrden != Guid.Empty) CargarDatos();
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
        }
        private void _initComboBoxTipo(DropDownList comboBox)
        {
            comboBox.DataSource = 
                new List<TipoEventoModelo> { new TipoEventoModelo { IdTipoEvento = Guid.Empty, Nombre = "Seleccione un tipo de evento" } }.Concat(servicioEvento.ListarTipoDeEventos());
            comboBox.DataTextField = "Nombre";
            comboBox.DataValueField = "IdTipoEvento";
            comboBox.DataBind();
        }

        private void _initComboBoxCliente(DropDownList comboBox)
        {
            comboBox.DataSource = 
                new List<ContactoModelo> { new ContactoModelo { Id = Guid.Empty, NombreApellido = "Seleccione un cliente" } }.Concat(servicioContacto.Listar().Where(contacto => contacto.Rol == "Cliente"));
            comboBox.DataTextField = "NombreApellido";
            comboBox.DataValueField = "Id";
            comboBox.DataBind();
        }

        private void _initComboBoxProducto(DropDownList comboBox)
        {
            comboBox.DataSource = 
                new List<ProductoModelo> { new ProductoModelo { IdProducto = Guid.Empty, Nombre = "Seleccione un producto" } }.Concat(servicioProducto.Listar());
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

        private void CargarComboBoxCliente()
        {
            cboCliente = (Components.ComboBoxAutoComplete)LoadControl("~/Backoffice/Components/ComboBoxAutoComplete.ascx");
            cboCliente.ComboID = "cboCliente";
            phComboBoxCliente.Controls.Add(cboCliente);
            cboCliente.InicializarComboBox(_initComboBoxCliente);
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
                    orden.DetalleProductos.Find(x => x.Producto.IdProducto == IdProducto).Cantidad = Cantidad;
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
        
        protected void OnTinyLoad(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "text", "LoadTiny();", true);
        }

        private void CargarDatos()
        {
            if (orden == null)
            {
                return;
            }

            FechaSeleccionada = orden.Evento.Fecha.ToShortDateString();
            rbtnTipoEntrega.SelectedValue = orden.TipoEntrega;
            inputHora.Value = orden.HoraEntrega.ToString();
            txtDireccion.Text = orden.DireccionEntrega;
            txtDescuento.Text = orden.DescuentoPorcentaje.ToString();
            txtCostoEnvio.Text = orden.CostoEnvio.ToString();

            // COMPONENTES
            cboTipo.SelectedValue = orden.Evento.TipoEvento.IdTipoEvento.ToString();
            cboCliente.SelectedValue = orden.Cliente.Id.ToString();
            calendario.FechaCalendario = orden.Evento.Fecha;

            // EDITOR
            tiny.Text = orden.Descripcion;

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
        private OrdenModelo ObtenerModeloDesdeFormulario()
        {
            orden.TipoEntrega = rbtnTipoEntrega.SelectedValue ?? "R";
            orden.HoraEntrega = TimeSpan.TryParse(inputHora.Value, out TimeSpan hora) ? hora : TimeSpan.Zero;
            orden.DireccionEntrega = txtDireccion.Text;
            
            if (orden.TipoEntrega == "D") // Delivery
            {
                if (string.IsNullOrEmpty(orden.DireccionEntrega))
                {
                    throw  new Exception("Debe ingresar una dirección de entrega");
                }
            }
            else
            {
                orden.DireccionEntrega = "";
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

        protected void rbtnTipoEntrega_SelectedIndexChanged(object sender, EventArgs e)
        {
            orden.TipoEntrega = rbtnTipoEntrega.SelectedValue;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            OrdenModelo Ob = ObtenerModeloDesdeFormulario();
            if (id != Guid.Empty)
            {
                Ob.IdOrden = id;
            }
            servicioOrden.GuardarOrden(Ob);
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
        
    }
}