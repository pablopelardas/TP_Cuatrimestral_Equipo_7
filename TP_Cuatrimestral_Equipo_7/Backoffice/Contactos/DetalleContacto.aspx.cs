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
        private Components.Calendario calendario;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["contacto"] != null)
            {
                contacto = (Dominio.Modelos.ContactoModelo)Session["contacto"];
            }
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
                        Session["contacto"] = contacto;
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("/Backoffice/Contactos", false);
                }
            }
            litInformacionPersonal.Text = contacto?.InformacionPersonal ?? "";

            // cargar calendario
            calendario = (Backoffice.Components.Calendario)LoadControl("~/Backoffice/Components/Calendario.ascx");
            phCalendario.Controls.Add(calendario);
            calendario.InicializarCalendario(null);

        }
        
        private void OnDayClick(object sender, EventArgs e)
        {
            // do something
        }
    }
}