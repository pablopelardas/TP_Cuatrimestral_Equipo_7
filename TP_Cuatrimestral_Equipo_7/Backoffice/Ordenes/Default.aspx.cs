using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Ordenes
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Dominio.Modelos.ContactoModelo> contactos;
        public List<Dominio.Modelos.OrdenModelo> ordenes;
        public int idContacto;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ( !IsPostBack)
            {
                Negocio.Servicios.OrdenServicio servicio = new Negocio.Servicios.OrdenServicio();
                ordenes = servicio.Listar();
            }
        }

        protected void ddlFiltro_SelectedIndexChanged (object sender, EventArgs e)
        {
            Negocio.Servicios.ContactoServicio servicio = new Negocio.Servicios.ContactoServicio();
            contactos = servicio.Listar();
            if (ddlFiltro.SelectedValue == "1")
            {
                contactos = contactos.Where(c => c.Rol == "Proveedor").ToList();
            }
            else if (ddlFiltro.SelectedValue == "2")
            {
                contactos = contactos.Where(c => c.Rol == "Cliente").ToList();
            }

            if (txtBuscar.Text != "")
            {
                contactos = contactos.Where(c => c.NombreApellido.Contains(txtBuscar.Text) || c.Email.Contains(txtBuscar.Text)).ToList();
            }
            
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            Negocio.Servicios.ContactoServicio servicio = new Negocio.Servicios.ContactoServicio();
            contactos = servicio.Listar();
            if (txtBuscar.Text != "")
            {
                contactos = contactos.Where(c => c.NombreApellido.Contains(txtBuscar.Text) || c.Email.Contains(txtBuscar.Text)).ToList();
            }

            if (ddlFiltro.SelectedValue == "Proveedor")
            {
                contactos = contactos.Where(c => c.Rol == "Proveedor").ToList();
            }
            else if (ddlFiltro.SelectedValue == "Cliente")
            {
                contactos = contactos.Where(c => c.Rol == "Cliente").ToList();
            }
        }
    }
}