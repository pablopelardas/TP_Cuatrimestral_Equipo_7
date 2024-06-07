using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private Backoffice.Components.Calendario calendario;
        protected void Page_Load(object sender, EventArgs e)
        {
            calendario = (Backoffice.Components.Calendario)LoadControl("~/Backoffice/Components/Calendario.ascx");
            phCalendario.Controls.Add(calendario);
            calendario.InicializarCalendario(null);
        }

    }
}