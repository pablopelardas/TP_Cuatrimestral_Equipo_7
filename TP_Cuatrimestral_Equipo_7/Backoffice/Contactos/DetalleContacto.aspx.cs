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
        public List<Dominio.Modelos.EventoModelo> eventos;
        private Negocio.Servicios.ContactoServicio negocio;
        private Negocio.Servicios.EventoServicio eventoServicio;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["contacto"] != null)
            {
                contacto = (Dominio.Modelos.ContactoModelo)Session["contacto"];
            }
            if (Session["eventosDelContacto"] != null)
            {
               eventos = (List<Dominio.Modelos.EventoModelo>)Session["eventosDelContacto"];
            }
            if (!IsPostBack)
            {
                Guid id = Guid.TryParse(Request.QueryString["id"], out id) ? id : Guid.Empty;
                negocio = new Negocio.Servicios.ContactoServicio();
                eventoServicio = new Negocio.Servicios.EventoServicio();
                if (id == Guid.Empty) Response.Redirect("/Backoffice/Contactos", false);
                try
                { ;
                    if (id != Guid.Empty)
                    {
                        contacto = negocio.ObtenerPorId(id);
                        eventos = eventoServicio.ListarEventosPorCliente(id);
                        Session["eventosDelContacto"] = eventos;
                        Session["contacto"] = contacto;
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("/Backoffice/Contactos", false);
                }
            }
            litInformacionPersonal.Text = contacto?.InformacionPersonal ?? "";
            
            Components.ListaDeOrdenes listaDeOrdenes = (Components.ListaDeOrdenes)LoadControl("~/Backoffice/Components/ListaDeOrdenes.ascx");
            phListaDeOrdenes.Controls.Add(listaDeOrdenes);
            listaDeOrdenes.InicializarGrilla(contacto, 2);
            
            Components.ListaDeEventos listaDeEventos = (Components.ListaDeEventos)LoadControl("~/Backoffice/Components/ListaDeEventos.ascx");
            phListaDeEventos.Controls.Add(listaDeEventos);
            listaDeEventos.InicializarGrilla(contacto);
        }
    }
}