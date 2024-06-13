using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Contactos
{
    public partial class DetalleContacto : System.Web.UI.Page
    {
        public Dominio.Modelos.ContactoModelo contacto;
        private Negocio.Servicios.ContactoServicio negocio;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Guid id = Guid.TryParse(Request.QueryString["id"], out id) ? id : Guid.Empty;
                negocio = new Negocio.Servicios.ContactoServicio();
                if (id == Guid.Empty) Response.Redirect("/Backoffice/Contactos", false);
                try
                { ;
                    if (id != Guid.Empty)
                    {
                        contacto = negocio.ObtenerPorId(id);
                        if (contacto != null)
                        {
                            lblTipo.Text = contacto.Rol;
                            lblNombreApellido.Text = contacto.NombreApellido;
                            lblCorreo.Text = contacto.Email;
                            lblTelefono.Text = contacto.Telefono;
                            lblFuente.Text = contacto.Fuente;
                            lblDireccion.Text = contacto.Direcciones.FirstOrDefault()?.CalleNumero;
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