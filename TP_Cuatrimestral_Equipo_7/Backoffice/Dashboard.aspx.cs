using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio.Modelos;

namespace TP_Cuatrimestral_Equipo_7
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private Backoffice.Components.Calendario calendario;

        public DateTime FechaSeleccionada { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            calendario = (Backoffice.Components.Calendario)LoadControl("~/Backoffice/Components/Calendario.ascx");
            phCalendario.Controls.Add(calendario);
            calendario.InicializarCalendario(OpenModal);
        }
        
        protected void OpenModal(object sender, EventArgs e)
        {
            Calendar control = (Calendar)sender;
            FechaSeleccionada = calendario.FechaCalendario;
            control.SelectedDates.Clear();
            rptEventos.DataSource = null;
            rptEventos.DataBind();
            rptOrdenes.DataSource = null;
            rptOrdenes.DataBind();
            lblEventos.Visible = false;
            lblOrdenes.Visible = false;
            if (calendario.EventosDelDia != null)
            {
                List<EventoModelo> ordenesDelDia = calendario.EventosDelDia.Where(x => x.Orden != null).ToList();
                List<EventoModelo> eventosDelDia = calendario.EventosDelDia.Where(x => x.Orden == null).ToList();
                if (calendario.EventosDelDia.Count > 0)
                {
                    if (ordenesDelDia.Count > 0)
                    {
                        lblOrdenes.Visible = true;
                        rptOrdenes.DataSource = ordenesDelDia;
                        rptOrdenes.DataBind();
                    }
                    if (eventosDelDia.Count > 0)
                    {
                        lblEventos.Visible = true;
                        rptEventos.DataSource = eventosDelDia;
                        rptEventos.DataBind();
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup();", true);

        }

    }
    

}