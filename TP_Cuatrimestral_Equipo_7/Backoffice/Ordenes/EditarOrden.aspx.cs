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
        public string id = null;
        Hashtable HolidayList;
        
        private Negocio.Servicios.OrdenServicio servicioOrden;
        private Negocio.Servicios.EventoServicio servicioEvento;
        private Negocio.Servicios.ContactoServicio servicioContacto;

        private Components.Calendario calendario;
        private Components.ComboBoxAutoComplete cboTipo;
        private Components.ComboBoxAutoComplete cboCliente;

        protected void Page_Load(object sender, EventArgs e)
        {
            servicioOrden = new Negocio.Servicios.OrdenServicio();
            servicioEvento = new Negocio.Servicios.EventoServicio();
            servicioContacto = new Negocio.Servicios.ContactoServicio();

            CargarCalendario();
            CargarComboBoxTipo();
            CargarComboBoxCliente();

            id = Request.QueryString["id"];
           

            if (id == null)
            {
                orden = new Dominio.Modelos.OrdenModelo();
            }
            else
            {
                try
                {
                    int idInt = Convert.ToInt32(Request.QueryString["id"]);
                    if (idInt > 0)
                    {
                        orden = servicioOrden.ObtenerPorId(idInt);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            if (!IsPostBack && id != null)
            {
                CargarDatos();
            }

        }
        private void _initComboBoxTipo(DropDownList comboBox)
        {
            comboBox.DataSource = 
                new List<TipoEventoModelo> { new TipoEventoModelo { IdTipoEvento = 0, Nombre = "Seleccione un tipo de evento" } }.Concat(servicioEvento.ListarTipoDeEventos());
            comboBox.DataTextField = "Nombre";
            comboBox.DataValueField = "IdTipoEvento";
            comboBox.DataBind();
        }

        private void _initComboBoxCliente(DropDownList comboBox)
        {
            comboBox.DataSource = 
                new List<ContactoModelo> { new ContactoModelo { Id = 0, NombreApellido = "Seleccione un cliente" } }.Concat(servicioContacto.Listar().Where(contacto => contacto.Rol == "Cliente"));
            comboBox.DataTextField = "NombreApellido";
            comboBox.DataValueField = "Id";
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
            txtHora.Text = orden.HoraEntrega.ToString();
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
        private OrdenModelo ObtenerModeloDesdeFormulario()
        {
            return new OrdenModelo
            {
                

            };
        }

        protected void rbtnTipoEntrega_SelectedIndexChanged(object sender, EventArgs e)
        {
            orden.TipoEntrega = rbtnTipoEntrega.SelectedValue;

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (id != null)
            {
                OrdenModelo Ob = ObtenerModeloDesdeFormulario();
                Ob.IdOrden = Convert.ToInt32(id);
                servicioOrden.Modificar(Ob);
            }
            else
            {
                servicioOrden.Agregar(ObtenerModeloDesdeFormulario());
            }
            
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
            //Agregar producto a la orden
        }

        protected void btnEditarProducto_Click(object sender, EventArgs e)
        {
            //Editar producto de la orden
        }
    }

}