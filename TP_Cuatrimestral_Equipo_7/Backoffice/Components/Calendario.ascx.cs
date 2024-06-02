using Dominio.Modelos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Components
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        Hashtable ListaDeEventos;
        private Negocio.Servicios.EventoServicio eventoServicio = new Negocio.Servicios.EventoServicio();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                CargarCalendario();
            }

            if (Session["ListaDeEventos"] != null)
            {
                ListaDeEventos = (Hashtable)Session["ListaDeEventos"];
            }

            cldFecha.FirstDayOfWeek = FirstDayOfWeek.Monday;
            cldFecha.NextPrevFormat = NextPrevFormat.ShortMonth;
            cldFecha.TitleFormat = TitleFormat.Month;
            cldFecha.ShowGridLines = true;
            cldFecha.DayStyle.Height = new Unit(50);
            cldFecha.DayStyle.Width = new Unit(150);
            cldFecha.DayStyle.HorizontalAlign = HorizontalAlign.Center;
            cldFecha.DayStyle.VerticalAlign = VerticalAlign.Middle;
            cldFecha.OtherMonthDayStyle.BackColor = System.Drawing.Color.AliceBlue;

        }

        private void CargarCalendario()
        {
            List<EventoModelo> eventos = eventoServicio.ListarEventos();
            foreach (EventoModelo evento in eventos)
            {
                    if (ListaDeEventos == null)
                    {
                        ListaDeEventos = new Hashtable();
                    }
                    if (ListaDeEventos[evento.Fecha.ToShortDateString()] == null)
                    {
                        ListaDeEventos[evento.Fecha.ToShortDateString()] = new List<EventoModelo>();   
                    }
                    List<EventoModelo> listaDeEventos = (List<EventoModelo>)ListaDeEventos[evento.Fecha.ToShortDateString()];
                    listaDeEventos.Add(evento);
            }
            Session["ListaDeEventos"] = ListaDeEventos;
        }
        protected void cldFecha_SelectionChanged(object sender, EventArgs e)
        {
            LabelAction.Text = "Date changed to :" + cldFecha.SelectedDate.ToShortDateString();
        }

        protected void cldFecha_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            LabelAction.Text = "Month changed to :" + e.NewDate.ToShortDateString();
        }


        protected void cldFecha_DayRender(object sender, DayRenderEventArgs e)
        {
            // target cell link to style
            e.Cell.BorderColor = ColorTranslator.FromHtml("#ccc");
            e.Cell.BorderWidth = 1;
            // move the anchor to a div
            // add a div to the cell
            Panel div = new Panel();
            div.CssClass = "day";
            e.Cell.Controls.Add(div);

            if (ListaDeEventos != null && ListaDeEventos[e.Day.Date.ToShortDateString()] != null)
            {
               var _ListaEventosTest = (List<EventoModelo>)ListaDeEventos[e.Day.Date.ToShortDateString()];
                List<EventoModelo> _ListaEventos = (List<EventoModelo>)ListaDeEventos[e.Day.Date.ToShortDateString()];
                foreach (EventoModelo evento in _ListaEventos)
                {
                    // add every event to the cell
                    //Literal lit = new Literal();
                    //lit.Text = "<br />";
                    //div.Controls.Add(lit);
                    //Label lbl = new Label();
                    //lbl.Text = evento.Descripcion;
                    //lbl.Font.Size = new FontUnit(FontSize.XSmall);
                    //div.Controls.Add(lbl);
                    Literal literalEvento = new Literal();
                    string OrdenId = evento.Orden != null && evento.Orden.IdOrden > 0 ? $"<div class='event-order'>#{evento.Orden.IdOrden}</div>" : "";
                    literalEvento.Text = $@"
<a class='event' href='./Ordenes/DetalleOrden.aspx?id={evento.Orden.IdOrden}&redirect_back=/Backoffice/Dashboard.aspx'>
    <div class='event-title'>{evento.Descripcion}</div>
    {OrdenId}
</a>
";
                    div.Controls.Add(literalEvento);
                }
            }
        }
    }
}