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
        Hashtable HolidayList;
        public Dominio.Modelos.OrdenModelo orden;
        private Negocio.Servicios.OrdenServicio servicioOrden;
        private Negocio.Servicios.EventoServicio servicioEvento;
        public string id = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            servicioOrden = new Negocio.Servicios.OrdenServicio();
            servicioEvento = new Negocio.Servicios.EventoServicio();
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




            if (!IsPostBack)
            {
                ddlTipo.DataSource = servicioEvento.ListarTipoDeEventos().Select(x => x.Nombre).ToList();
                ddlTipo.DataBind();
            }
            if (!IsPostBack && id != null)
            {
                CargarDatos();
            }





        }
    
        protected void OnTinyLoad(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "text", "LoadTiny();", true);
        }

        private void CargarDatos()
        {
            cldFecha.SelectedDate = orden.Evento.Fecha;
            txtCliente.Text = orden.Cliente.NombreApellido;

            rbtnTipoEntrega.SelectedValue = orden.TipoEntrega;
            txtHora.Text = orden.HoraEntrega.ToString();
            txtDireccion.Text = orden.DireccionEntrega;

            txtDescuento.Text = orden.DescuentoPorcentaje.ToString();
            txtCostoEnvio.Text = orden.CostoEnvio.ToString();

            ddlTipo.SelectedValue = orden.Evento.TipoEvento.Nombre;


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