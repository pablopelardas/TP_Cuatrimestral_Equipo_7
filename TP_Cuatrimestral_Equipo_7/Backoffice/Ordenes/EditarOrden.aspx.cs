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
        private Components.DetailList detailList;

        private Components.ComboBoxAutoComplete cboProducto;
        private TextBox txtCantidad;

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
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                Session[OrdenActual] = orden;
                CargarComponentes();
                if (orden.IdOrden != Guid.Empty) CargarDatos();
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
            CargarDetailList();
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

        private void _initDetailList(GridView gvData)
        {
            if (!IsPostBack)
            {
                gvData.AutoGenerateColumns = false;
                gvData.CssClass = "table table-striped table-bordered table-hover";
                gvData.HeaderStyle.CssClass = "thead-dark";
                gvData.RowStyle.CssClass = "tbody-light";
                gvData.FooterStyle.CssClass = "tfoot-dark";
                gvData.AlternatingRowStyle.CssClass = "tbody-light";
                gvData.EmptyDataText = "No hay productos en la orden";
                gvData.Columns.Add(new BoundField { DataField = "Producto.IdProducto", HeaderText = "ID", SortExpression = "IdProducto" });
                gvData.Columns.Add(new BoundField { DataField = "Producto.Nombre", HeaderText = "Nombre", SortExpression = "Nombre" });
                gvData.Columns.Add(new BoundField { DataField = "Cantidad", HeaderText = "Cantidad", SortExpression = "Cantidad" });
                gvData.Columns.Add(new BoundField { DataField = "PrecioUnitarioActual", HeaderText = "Precio", SortExpression = "Precio" });
                gvData.Columns.Add(new BoundField { DataField = "Subtotal", HeaderText = "Subtotal", SortExpression = "Subtotal" });
            }


            if (orden != null && orden.DetalleProductos != null )
            {
                gvData.DataSource = orden.DetalleProductos;
                gvData.DataBind();
            }
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

        private void OnAceptarAgregarProducto(object sender, EventArgs e)
        {
            // Agregar producto a la orden
            Guid IdProducto = (Guid)cboProducto.SelectedValue;
            string Cantidad = txtCantidad.Text;

            if (orden.DetalleProductos == null)
            {
                orden.DetalleProductos = new List<ProductoDetalleOrdenModelo>();
            }
            ProductoModelo producto = servicioProducto.ObtenerPorId(IdProducto);

            ProductoDetalleOrdenModelo detalle = new ProductoDetalleOrdenModelo
            {
                IdOrden = orden.IdOrden,
                Producto = producto,
                Cantidad = Convert.ToInt32(Cantidad),
                PrecioUnitarioActual = producto.Precio,
                CostoUnitarioActual = producto.Costo,
                Porciones = producto.Porciones
            };

            orden.DetalleProductos.Add(detalle);
            detailList.CargarListaDetalle();
        }

        private void CargarDetailList()
        {

           detailList = (Components.DetailList)LoadControl("~/Backoffice/Components/DetailList.ascx");
           phDetalleOrden.Controls.Add(detailList);

           // Construir Header con boton de agregar producto

           LiteralControl headerTitle = new LiteralControl("<h3>Detalle de Orden</h3>");

           Button btnAgregarProducto = new Button();
           btnAgregarProducto.Text = "Agregar Producto";
           btnAgregarProducto.CssClass = "btn btn-primary";
           btnAgregarProducto.Attributes.Add("type", "button");
           btnAgregarProducto.Click += new EventHandler(btnAgregarProducto_Click);

           Panel headerPanel = new Panel();
            headerPanel.CssClass = "d-flex justify-content-between";
            headerPanel.Controls.Add(headerTitle);
           headerPanel.Controls.Add(btnAgregarProducto);

           detailList.InicializarListaDetalle(_initDetailList, headerPanel);

           // Construir Modal
           cboProducto = (Components.ComboBoxAutoComplete)LoadControl("~/Backoffice/Components/ComboBoxAutoComplete.ascx");
           cboProducto.ComboID = "cboProducto";
           cboProducto.InicializarComboBox(_initComboBoxProducto);

           Panel modalBody = new Panel();
           

            txtCantidad = new TextBox();
            txtCantidad.CssClass = "form-control";

            modalBody.Controls.Add(cboProducto);
            modalBody.Controls.Add(txtCantidad);

           detailList.CargarModal("Agregar Producto", modalBody, OnAceptarAgregarProducto);

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
            orden.TipoEntrega = rbtnTipoEntrega.SelectedValue;
            orden.HoraEntrega = TimeSpan.TryParse(inputHora.Value, out TimeSpan hora) ? hora : orden.HoraEntrega;
            orden.DireccionEntrega = txtDireccion.Text;

            Guid idTipoEvento = (Guid)cboTipo.SelectedValue;
            Guid idCliente = (Guid)cboCliente.SelectedValue;

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

        protected void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            // Eliminar producto de la orden
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            detailList.MostrarModal();
        }

        protected void btnEditarProducto_Click(object sender, EventArgs e)
        {
            //Editar producto de la orden
        }
    }

}