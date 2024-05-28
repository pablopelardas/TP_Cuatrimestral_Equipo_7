using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7.Contactos
{
    public partial class DetalleContacto : System.Web.UI.Page
    {
        public Dominio.Modelos.ContactoModelo contacto;
        private Negocio.Servicios.ContactoServicio negocio;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["id"];
                negocio = new Negocio.Servicios.ContactoServicio();
                if (id == null) Response.Redirect("/Backoffice/Contactos", false);
                try
                {
                    int idInt = Convert.ToInt32(Request.QueryString["id"]);
                    if (idInt > 0)
                    {
                        contacto = negocio.ObtenerPorId(idInt);
                        if (contacto != null)
                        {
                            lblTipo.Text = contacto.Rol;
                            lblNombreApellido.Text = contacto.NombreApellido;
                            lblCorreo.Text = contacto.Email;
                            lblTelefono.Text = contacto.Telefono;
                            lblFuente.Text = contacto.Fuente;
                            lblDireccion.Text = contacto.Direccion;
                            lblDeseaRecibirCorreos.Text = contacto.DeseaRecibirCorreos ? "Sí" : "No";
                            lblDeseaRecibirWhatsapps.Text = contacto.DeseaRecibirWhatsapp ? "Sí" : "No";
                            
                            litInformacionPersonal.Text = contacto.InformacionPersonal;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("/Backoffice/Contactos", false);
                }

            }

        }
    }
}