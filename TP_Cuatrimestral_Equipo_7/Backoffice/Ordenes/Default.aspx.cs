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
        private int paginaActual = 1;
        private int ordenesPorPagina = 5;
        private int totalPaginas = 0;
        private int semanas = 1;
        private int estado = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["page"] != null)
                {
                    paginaActual = Int32.TryParse(Request.QueryString["page"], out paginaActual) ? paginaActual : 1;
                }
                if (Request.QueryString["semanas"] != null)
                {
                    semanas = Int32.TryParse(Request.QueryString["semanas"], out semanas) ? semanas : 0;
                    ddIntervalo.SelectedValue = semanas.ToString();
                }
                if (Request.QueryString["estado"] != null)
                {
                    estado = Int32.TryParse(Request.QueryString["estado"], out estado) ? estado : 0;
                    ddEstado.SelectedValue = estado.ToString();
                }
                ListarOrdenes();
            }
            
            // cargar pagiando
            if (totalPaginas > 1)
            {
                    phPaginado.Visible = true;
                    StringBuilder paginado = new StringBuilder();
                    string prevQuery = "Default.aspx?semanas=" + semanas + "&estado=" + estado + "&page=";
                
                    paginado.Append("<li><a href='" + prevQuery + (paginaActual > 1 ? paginaActual - 1 : paginaActual) + "' class='flex h-8 items-center justify-center rounded-s-lg border border-e-0 border-gray-300 bg-white px-3 leading-tight text-gray-500 hover:bg-gray-100 hover:text-gray-700 dark:border-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white'><span class='sr-only'>Previous</span><svg class='h-4 w-4 rtl:rotate-180' aria-hidden='true' xmlns='http://www.w3.org/2000/svg' width='24' height='24' fill='none' viewBox='0 0 24 24'><path stroke='currentColor' stroke-linecap='round' stroke-linejoin='round' stroke-width='2' d='m15 19-7-7 7-7'/></svg></a></li>");
                
                    for (int i = 1; i <= totalPaginas; i++)
                    {
                        if (i == paginaActual)
                        {
                            paginado.Append("<li><a aria-current='page' href='#' class='z-10 flex items-center justify-center px-3 h-8 leading-tight text-blue-600 border border-blue-300 bg-blue-50 hover:bg-blue-100 hover:text-blue-700 dark:border-gray-700 dark:bg-gray-700 dark:text-white'>" + i + "</a></li>");
                        }
                        else
                        {
                            paginado.Append("<li><a href='" + prevQuery +  i + "' class='flex h-8 items-center justify-center border border-gray-300 bg-white px-3 leading-tight text-gray-500 hover:bg-gray-100 hover:text-gray-700 dark:border-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white'>" + i + "</a></li>");
                        }
                    }
                    
                    paginado.Append("<li><a href='" + prevQuery +  (paginaActual == totalPaginas ? paginaActual : paginaActual + 1) + "' class='flex h-8 items-center justify-center rounded-e-lg border border-gray-300 bg-white px-3 leading-tight text-gray-500 hover:bg-gray-100 hover:text-gray-700 dark:border-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white'><span class='sr-only'>Next</span><svg class='h-4 w-4 rtl:rotate-180' aria-hidden='true' xmlns='http://www.w3.org/2000/svg' width='24' height='24' fill='none' viewBox='0 0 24 24'><path stroke='currentColor' stroke-linecap='round' stroke-linejoin='round' stroke-width='2' d='m9 5 7 7-7 7'/></svg></a></li>");
                    
                    phPaginado.Controls.Add(new LiteralControl(paginado.ToString()));
                    
            }
            
        }

        private void ListarOrdenes()
        {
            ordenes = ordenServicio.ListarOrdenes(semanas, estado, paginaActual, ordenesPorPagina, out totalPaginas);
        }
        
        protected void ddIntervaloChanged(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx?semanas=" + ddIntervalo.SelectedValue + "&estado=" + ddEstado.SelectedValue, false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void ddEstadoChanged(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx?semanas=" + ddIntervalo.SelectedValue + "&estado=" + ddEstado.SelectedValue, false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}