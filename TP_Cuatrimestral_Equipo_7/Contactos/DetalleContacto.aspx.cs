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
        private Dominio.Modelos.ContactoModelo contacto;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["id"];
                Negocio.Servicios.ContactoServicio negocio = new Negocio.Servicios.ContactoServicio();
                if (id == null) Response.Redirect("/Dashboard.aspx", false);
                try
                {
                    int idInt = Convert.ToInt32(Request.QueryString["id"]);
                    if (idInt > 0)
                    {
                        contacto = negocio.ObtenerPorId(idInt);
                        if (contacto != null)
                        {
                            // CARGAR LOS DATOS DEL CONTACTO EN LOS CONTROLES
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("/Dashboard.aspx", false);
                }

            }

        }
    }
}