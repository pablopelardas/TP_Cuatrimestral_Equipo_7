using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using Dominio.Modelos;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Components
{
    public partial class ListaDeOrdenes : UserControl
    {
        public List<Dominio.Modelos.OrdenModelo> ordenes;
        private ContactoModelo contacto;
        private Negocio.Servicios.OrdenServicio ordenServicio = new Negocio.Servicios.OrdenServicio();
        private int paginaActual = 1;
        private int ordenesPorPagina = 5;
        public int totalPaginas = 0;
        private int semanas = 4;
        private int estado = 0;
        
        public void InicializarGrilla(ContactoModelo _contacto, int _ordenesPorPagina = 5)
        {
            ordenesPorPagina = _ordenesPorPagina;
            phPaginado.Controls.Clear();
            
            if (!IsPostBack)
            {
                ViewState["paginaActual"] = 1;
                ViewState["semanas"] = 4;
                ViewState["estado"] = 0;
                ViewState["ordenes"] = null;
                ViewState["totalPaginas"] = 0;
            }
            
            if (ViewState["paginaActual"] != null)
            {
                paginaActual = (int)ViewState["paginaActual"];
            }
            if (ViewState["semanas"] != null)
            {
                semanas = (int)ViewState["semanas"];
                ddIntervalo.SelectedValue = semanas.ToString();
            }
            if (ViewState["estado"] != null)
            {
                estado = (int)ViewState["estado"];
                ddEstado.SelectedValue = estado.ToString();
            }
            if (ViewState["ordenes"] != null)
            {
                ordenes = (List<Dominio.Modelos.OrdenModelo>)ViewState["ordenes"];
            }
            if (ViewState["totalPaginas"] != null)
            {
                totalPaginas = (int)ViewState["totalPaginas"];
            }
            
            contacto = _contacto;

            if (IsPostBack)
            {
                if (Request.Form["__EVENTTARGET"].Contains("setPage"))
                {
                    paginaActual = int.Parse(Request.Form["__EVENTTARGET"].Replace("setPage", ""));
                }
                if (Request.Form["__EVENTTARGET"] == "nextPage")
                {
                    if (paginaActual < totalPaginas)
                    {
                        paginaActual++;
                    }
                }
                if (Request.Form["__EVENTTARGET"] == "previousPage")
                {
                    if (paginaActual > 1)
                    {
                        paginaActual--;
                    }
                }
                if (Request.Form["__EVENTTARGET"] == "clearFilters")
                {
                    semanas = 0;
                    estado = 0;
                    ddIntervalo.SelectedValue = semanas.ToString();
                    ddEstado.SelectedValue = estado.ToString();
                }
            }
            
            ListarOrdenes(contacto);
             if (totalPaginas > 1)
            {
                    phPaginado.Visible = true;
                    StringBuilder paginado = new StringBuilder();
                
                    paginado.Append($"<li><a href='javascript:__doPostBack(\"previousPage\",\"async\")' class='flex h-8 items-center justify-center rounded-s-lg border border-e-0 border-gray-300 bg-white px-3 leading-tight text-gray-500 hover:bg-gray-100 hover:text-gray-700 dark:border-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white'><span class='sr-only'>Previous</span><svg class='h-4 w-4 rtl:rotate-180' aria-hidden='true' xmlns='http://www.w3.org/2000/svg' width='24' height='24' fill='none' viewBox='0 0 24 24'><path stroke='currentColor' stroke-linecap='round' stroke-linejoin='round' stroke-width='2' d='m15 19-7-7 7-7'/></svg></a></li>");
                
                    for (int i = 1; i <= totalPaginas; i++)
                    {
                        if (i == paginaActual)
                        {
                            paginado.Append("<li><a aria-current='page' href='#' class='z-10 flex items-center justify-center px-3 h-8 leading-tight text-blue-600 border border-blue-300 bg-blue-50 hover:bg-blue-100 hover:text-blue-700 dark:border-gray-700 dark:bg-gray-700 dark:text-white'>" + i + "</a></li>");
                        }
                        else
                        {
                            paginado.Append($"<li><a href='javascript:__doPostBack(\"setPage{i}\",\"async\")' class='flex h-8 items-center justify-center border border-gray-300 bg-white px-3 leading-tight text-gray-500 hover:bg-gray-100 hover:text-gray-700 dark:border-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white'>{i}</a></li>");
                        }
                    }
                    
                    paginado.Append("<li><a href='javascript:__doPostBack(\"nextPage\",\"async\")' class='flex h-8 items-center justify-center rounded-e-lg border border-gray-300 bg-white px-3 leading-tight text-gray-500 hover:bg-gray-100 hover:text-gray-700 dark:border-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white'><span class='sr-only'>Next</span><svg class='h-4 w-4 rtl:rotate-180' aria-hidden='true' xmlns='http://www.w3.org/2000/svg' width='24' height='24' fill='none' viewBox='0 0 24 24'><path stroke='currentColor' stroke-linecap='round' stroke-linejoin='round' stroke-width='2' d='m9 5 7 7-7 7'/></svg></a></li>");
                    
                    phPaginado.Controls.Add(new LiteralControl(paginado.ToString()));
                    
            }
        }
        
        protected void ddIntervaloChanged(object sender, EventArgs e)
        {
            // Response.Redirect("Default.aspx?semanas=" + ddIntervalo.SelectedValue + "&estado=" + ddEstado.SelectedValue, false);
            // Context.ApplicationInstance.CompleteRequest();
            
            ViewState["semanas"] = int.Parse(ddIntervalo.SelectedValue);
            ViewState["paginaActual"] = 1;
            ViewState["totalPaginas"] = 1;
            InicializarGrilla(contacto);
        }

        protected void ddEstadoChanged(object sender, EventArgs e)
        {
            // Response.Redirect("Default.aspx?semanas=" + ddIntervalo.SelectedValue + "&estado=" + ddEstado.SelectedValue, false);
            // Context.ApplicationInstance.CompleteRequest();
            
            ViewState["estado"] = int.Parse(ddEstado.SelectedValue);
            ViewState["paginaActual"] = 1;
            ViewState["totalPaginas"] = 1;
            InicializarGrilla(contacto);
        }
        
        private void ListarOrdenes(ContactoModelo contacto = null)
        {
            ordenes = ordenServicio.ListarOrdenes(semanas, estado, paginaActual, ordenesPorPagina, out totalPaginas, contacto);
            ViewState["totalPaginas"] = totalPaginas;
        }
    }
}