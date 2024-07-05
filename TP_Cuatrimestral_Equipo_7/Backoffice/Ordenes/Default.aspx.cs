using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Ordenes
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Dominio.Modelos.OrdenModelo> ordenes;
        private Negocio.Servicios.OrdenServicio ordenServicio = new Negocio.Servicios.OrdenServicio();
        private Components.ListaDeOrdenes listaDeOrdenes;
        private int paginaActual = 1;
        private int ordenesPorPagina = 5;
        private int totalPaginas = 0;
        private int semanas = 4;
        private int estado = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            phGrilla.Controls.Clear();
            listaDeOrdenes = (Components.ListaDeOrdenes)LoadControl("~/Backoffice/Components/ListaDeOrdenes.ascx");
            phGrilla.Controls.Add(listaDeOrdenes);
            listaDeOrdenes.InicializarGrilla(null, 5);
            
        }
        
    }
}