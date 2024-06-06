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

        public delegate void OnDayClick(object sender, EventArgs e);

        private OnDayClick onDayClick = null;

        public DateTime FechaCalendario
        {
            get
            {
                return cldFecha.SelectedDate;
            }
            set
            {
                cldFecha.SelectedDate = Convert.ToDateTime(value);
                cldFecha.VisibleDate = Convert.ToDateTime(value);
            }
        }
       
        public void AddDayClick(OnDayClick onDayClick)
        {
            this.onDayClick = onDayClick;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                CargarCalendario();
                Session[CLDFECHASELECCIONADA] = null;
            }

            if (Session[CLDLISTADEEVENTOS] != null)
            {
                ListaDeEventos = (Hashtable)Session[CLDLISTADEEVENTOS];
            }

            if (Session[CLDFECHASELECCIONADA] != null)
            {
                FechaSeleccionada = Session[CLDFECHASELECCIONADA].ToString();
            }


            cldFecha.FirstDayOfWeek = FirstDayOfWeek.Monday;
            cldFecha.NextPrevFormat = NextPrevFormat.ShortMonth;
            cldFecha.TitleFormat = TitleFormat.Month;
            cldFecha.ShowGridLines = true;
            cldFecha.DayStyle.Height = new Unit(50);
            // width auto
            cldFecha.DayStyle.Width = new Unit(100);
            cldFecha.DayStyle.HorizontalAlign = HorizontalAlign.Center;
            cldFecha.DayStyle.VerticalAlign = VerticalAlign.Middle;
            cldFecha.OtherMonthDayStyle.BackColor = System.Drawing.Color.AliceBlue;
            cldFecha.SelectionChanged += new EventHandler(cldFechaClicked);

        }

        private void CargarCalendario()
        {
            List<EventoModelo> eventos = eventoServicio.ListarEventos();
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
        protected void OpenModal()
        {
            if (ListaDeEventos != null && ListaDeEventos[FechaSeleccionada] != null)
            {
                var _ListaEventos = (List<EventoModelo>)ListaDeEventos[FechaSeleccionada];
                if (_ListaEventos.Count > 0)
                {
                    rptEventos.DataSource = _ListaEventos;
                    rptEventos.DataBind();
                }
            }
            else
            {
                rptEventos.DataSource = null;
                rptEventos.DataBind();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup();", true);

        }

        protected void cldFechaClicked(object sender, EventArgs e)
        {
            FechaSeleccionada = cldFecha.SelectedDate.ToShortDateString();
            Session[CLDFECHASELECCIONADA] = FechaSeleccionada;
            if (onDayClick != null)
            {
                onDayClick(sender, e);
                return;
            }
            cldFecha.SelectedDates.Clear();
            OpenModal();
        }

        protected void cldFecha_DayRender(object sender, DayRenderEventArgs e)
        {
            // target cell link to style
            e.Cell.BorderColor = ColorTranslator.FromHtml("#ccc");
            e.Cell.BorderWidth = 1;


            // EVENTOS EN GRILLA
            //Panel div = new Panel();
            //div.CssClass = "day";
            //e.Cell.Controls.Add(div);

            //if (ListaDeEventos != null && ListaDeEventos[e.Day.Date.ToShortDateString()] != null)
            //{
            //    var _ListaEventosTest = (List<EventoModelo>)ListaDeEventos[e.Day.Date.ToShortDateString()];
            //    List<EventoModelo> _ListaEventos = (List<EventoModelo>)ListaDeEventos[e.Day.Date.ToShortDateString()];
            //    foreach (EventoModelo evento in _ListaEventos)
            //    {
            //        Literal literalEvento = new Literal();
            //        string OrdenId = evento.Orden != null && evento.Orden.IdOrden > 0 
            //            ? $@"<div class='event-order'>#{evento.Orden.IdOrden}</div>" 
            //            : "";
            //        literalEvento.Text = $@"
            //            <a class='event' href='./Ordenes/DetalleOrden.aspx?id={evento.Orden.IdOrden}&redirect_back=/Backoffice/Dashboard.aspx'>
            //                <div class='event-title'>{evento.Descripcion}</div>
            //                {OrdenId}
            //            </a>
            //            ";
            //        div.Controls.Add(literalEvento);
            //    }
            //}

            // WARMMAP EN GRILLA
            if (ListaDeEventos != null && ListaDeEventos[e.Day.Date.ToShortDateString()] != null)
            {
                var _ListaEventos = (List<EventoModelo>)ListaDeEventos[e.Day.Date.ToShortDateString()];
                if (_ListaEventos.Count > 0)
                {
                    e.Cell.BackColor = ColorTranslator.FromHtml("#fffec3");
                    e.Cell.ToolTip = _ListaEventos.Count + " evento" + (_ListaEventos.Count > 1 ? "s" : "");
                }
                if (_ListaEventos.Count > 1)
                {
                    e.Cell.BackColor = ColorTranslator.FromHtml("#f5eea5");
                }
                if (_ListaEventos.Count > 2)
                {
                    e.Cell.BackColor = ColorTranslator.FromHtml("#ff9288");
                }
                if (_ListaEventos.Count > 3)
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
        }
    }
}