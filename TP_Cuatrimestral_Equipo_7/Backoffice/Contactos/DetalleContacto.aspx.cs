using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio.Modelos;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Contactos
{
    public partial class DetalleContacto : System.Web.UI.Page
    {
        public Dominio.Modelos.ContactoModelo contacto;
        public List<Dominio.Modelos.EventoModelo> eventos;
        private Negocio.Servicios.ContactoServicio negocio;
        private Negocio.Servicios.EventoServicio eventoServicio;
        private Negocio.Servicios.HistoricoServicio servicioHistorico;
        public List<HistoricoModelo> historicos;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            servicioHistorico = new Negocio.Servicios.HistoricoServicio();
            if (Session["contacto"] != null)
            {
                contacto = (Dominio.Modelos.ContactoModelo)Session["contacto"];
            }
            if (Session["eventosDelContacto"] != null)
            {
               eventos = (List<Dominio.Modelos.EventoModelo>)Session["eventosDelContacto"];
            }
            if (Session["historicos"] != null)
            {
                historicos = (List<HistoricoModelo>)Session["historicos"];
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
                        historicos = servicioHistorico.ListarPorEntidad(contacto.Id);
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