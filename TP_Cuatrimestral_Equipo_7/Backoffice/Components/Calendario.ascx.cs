using Dominio.Modelos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Components
{
    public partial class Calendario : System.Web.UI.UserControl
    {
        Hashtable ListaDeEventos;

        private Negocio.Servicios.EventoServicio eventoServicio = new Negocio.Servicios.EventoServicio();

        private string FechaSeleccionada;
        private string CLDFECHASELECCIONADA = "cldFechaSeleccionada";
        private string CLDLISTADEEVENTOS = "CldListaDeEventos";
        
        public string CellSizeTWClass { get; set;}

        public Action<object,EventArgs> OnDayClick { get; set; }

        public DateTime FechaCalendario
        {
            get
            {
                if (cldFecha.SelectedDate.ToShortDateString() == "1/1/0001" || cldFecha.SelectedDate == null)
                {
                    cldFecha.VisibleDate = DateTime.Now;
                    cldFecha.SelectedDate = DateTime.Now;
                    return DateTime.Now;
                }
                cldFecha.VisibleDate = cldFecha.SelectedDate;
                return cldFecha.SelectedDate;
            }
            set
            {
                cldFecha.SelectedDate = Convert.ToDateTime(value);
                cldFecha.VisibleDate = Convert.ToDateTime(value);
            }
        }
        
        
        public List<EventoModelo> EventosDelDia
        {
            get
            {
                if (ListaDeEventos != null && ListaDeEventos[FechaSeleccionada] != null)
                {
                    return (List<EventoModelo>)ListaDeEventos[FechaSeleccionada];
                }
                return null;
            }
        }
       
        public void InicializarCalendario(Action<object, EventArgs> onDayClick, List<EventoModelo> eventos = null, DateTime? fechaInicial = null)
        {
            
            if (!IsPostBack)
            {
                Session[CLDFECHASELECCIONADA] = null;
                CargarCalendario(eventos, fechaInicial);
                Populate_MonthList();
                Populate_YearList();
            }


            cldFecha.SelectionChanged += new EventHandler(cldFechaClicked);

            if (Session[CLDLISTADEEVENTOS] != null)
            {
                ListaDeEventos = (Hashtable)Session[CLDLISTADEEVENTOS];
            }

            if (Session[CLDFECHASELECCIONADA] != null)
            {
                FechaSeleccionada = Session[CLDFECHASELECCIONADA].ToString();
            }


            OnDayClick = onDayClick != null ? onDayClick : null;
            
            cldFecha.Font.Name = "Verdana";
            cldFecha.Font.Size = FontUnit.Point(8);
            cldFecha.FirstDayOfWeek = FirstDayOfWeek.Monday;
            cldFecha.NextPrevFormat = NextPrevFormat.ShortMonth;
            cldFecha.TitleFormat = TitleFormat.MonthYear;
            cldFecha.VisibleDate = fechaInicial ?? DateTime.Now;
            cldFecha.CssClass = "w-full";
            cldFecha.TitleStyle.CssClass = "bg-gray-700 text-white";
            cldFecha.Style.Add("border", "1px solid #374151");
            cldFecha.TitleStyle.BackColor = ColorTranslator.FromHtml("#374151");
            cldFecha.NextPrevStyle.ForeColor = ColorTranslator.FromHtml("#FFFFCC");
            cldFecha.DayHeaderStyle.CssClass = "bg-gray-700 text-white";
            cldFecha.DayHeaderStyle.BorderStyle = BorderStyle.None;
            CellSizeTWClass = "h-8 sm:h-12 lg:h-20";
        }

        private void CargarCalendario(List<EventoModelo> eventos = null, DateTime? fechaInicial = null)
        {
            if (eventos == null)
            {
                eventos = eventoServicio.ListarEventos();
            }
            if (ListaDeEventos == null)
            {
                ListaDeEventos = new Hashtable();
            }
            foreach (EventoModelo evento in eventos)
            {
                    if (ListaDeEventos[evento.Fecha.ToShortDateString()] == null)
                    {
                        ListaDeEventos[evento.Fecha.ToShortDateString()] = new List<EventoModelo>();   
                    }
                    List<EventoModelo> listaDeEventos = (List<EventoModelo>)ListaDeEventos[evento.Fecha.ToShortDateString()];
                    listaDeEventos.Add(evento);
            }
            Session[CLDLISTADEEVENTOS] = ListaDeEventos;



        }


        protected void cldFechaClicked(object sender, EventArgs e)
        {
            FechaSeleccionada = cldFecha.SelectedDate.ToShortDateString();
            Session[CLDFECHASELECCIONADA] = FechaSeleccionada;
            if (OnDayClick != null)
            {
                OnDayClick(sender, e);
                return;
            }
        }

        protected void cldFecha_DayRender(object sender, DayRenderEventArgs e)
        {
            // target cell link to style
            e.Cell.BorderColor = ColorTranslator.FromHtml("#111827");
            e.Cell.BorderWidth = 1;
            e.Cell.ForeColor = ColorTranslator.FromHtml("#FFF");
            // tailwind cell
            e.Cell.CssClass = $"{CellSizeTWClass} bg-[#2B4450]";
            if (e.Day.IsOtherMonth)
            {
                e.Cell.CssClass = $"{CellSizeTWClass} bg-[#1f2937]";
            }
            if (e.Day.IsToday)
            {
                e.Cell.CssClass = $"{CellSizeTWClass} bg-blue-400";
            }

            // WARMMAP EN GRILLA
            if (ListaDeEventos != null && ListaDeEventos[e.Day.Date.ToShortDateString()] != null)
            {
                var _listaEventos = (List<EventoModelo>)ListaDeEventos[e.Day.Date.ToShortDateString()];
                
                var _ListaEventosClientes = _listaEventos.Where(x => x.Orden == null).ToList();
                if (_ListaEventosClientes.Count > 0)
                {
                    e.Cell.ForeColor = ColorTranslator.FromHtml("#111827");
                    e.Cell.BackColor = ColorTranslator.FromHtml("#c3f7ff");
                    e.Cell.ToolTip += _ListaEventosClientes.Count + " evento" +
                                      (_ListaEventosClientes.Count > 1 ? "s" : "") + Environment.NewLine;
                }
                
                var _listaOrdenes = _listaEventos.OrderBy(x => x.Fecha).Where(x => x.Orden != null).ToList();
                if (_listaOrdenes.Count > 0)
                {
                    e.Cell.BackColor = ColorTranslator.FromHtml("#fffec3");
                    e.Cell.ForeColor = ColorTranslator.FromHtml("#111827");
                    e.Cell.ToolTip += _listaOrdenes.Count + " orden" + (_listaOrdenes.Count > 1 ? "es" : "");
                }
                if (_listaOrdenes.Count > 1)
                {
                    e.Cell.BackColor = ColorTranslator.FromHtml("#f5eea5");
                }
                if (_listaOrdenes.Count > 2)
                {
                    e.Cell.BackColor = ColorTranslator.FromHtml("#ff9288");
                }
                if (_listaOrdenes.Count > 3)
                {
                    e.Cell.BackColor = ColorTranslator.FromHtml("#ff625e");
                }
                

            }
        }

        // remove the query string if user navigates to another month or year or refreshes the page
        protected void cldFecha_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            FechaSeleccionada = null;
            Session[CLDFECHASELECCIONADA] = null;
            string newmonth = e.NewDate.Month.ToString();
            string newyear = e.NewDate.Year.ToString();
            
            ddlMonth.SelectedValue = newmonth;
            ddlYear.SelectedValue = newyear;
            
        }
        

        protected void Set_Calendar(object Sender, EventArgs e)
        {
            int year = int.Parse(ddlYear.SelectedValue);
            int month = int.Parse(ddlMonth.SelectedValue);
            cldFecha.VisibleDate = new DateTime(year, month, 1);
        }
        
        protected void Populate_MonthList()
        {
            for (int month = 1; month <= 12; month++)
            {
                ddlMonth.Items.Add(new ListItem(new DateTime(1900, month, 1).ToString("MMMM"), month.ToString()));
            }
            ddlMonth.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
        }
        protected void Populate_YearList()
        {
            for (int year = DateTime.Now.Year - 10; year <= DateTime.Now.Year + 10; year++)
            {
                ddlYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            }
            ddlYear.Items.FindByValue(DateTime.Now.Year.ToString()).Selected = true;
        }
    }
}