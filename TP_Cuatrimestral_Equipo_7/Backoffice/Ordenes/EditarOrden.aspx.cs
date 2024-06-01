using Dominio.Modelos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Ordenes
{
    public partial class EditarOrden : System.Web.UI.Page
    {
        Hashtable HolidayList;
        public Dominio.Modelos.OrdenModelo orden;
        private Negocio.Servicios.OrdenServicio servicioOrden;
        public string id = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            servicioOrden = new Negocio.Servicios.OrdenServicio();
            id = Request.QueryString["id"];
           

            if (id == null)
            {
                orden = new Dominio.Modelos.OrdenModelo();
            }
            else
            {
                try
                {
                    int idInt = Convert.ToInt32(Request.QueryString["id"]);
                    if (idInt > 0)
                    {
                        orden = servicioOrden.ObtenerPorId(idInt);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            if (!IsPostBack && id != null)
            {
                CargarDatos();
            }

            HolidayList = Getholiday();
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
    
        protected void OnTinyLoad(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "text", "LoadTiny();", true);
        }

        private void CargarDatos()
        {
            ddlTipo.SelectedValue = orden.TipoEvento;
            cldFecha.SelectedDate = orden.Fecha;
            txtCliente.Text = orden.Cliente.NombreApellido;

            rbtnTipoEntrega.SelectedValue = orden.TipoEntrega;
            txtHora.Text = orden.HoraEntrega.ToString();
            txtDireccion.Text = orden.DireccionEntrega;

            txtDescuento.Text = orden.DescuentoPorcentaje.ToString();
            txtCostoEnvio.Text = orden.CostoEnvio.ToString();


            tiny.Text = orden.Descripcion;

        }
        private OrdenModelo ObtenerModeloDesdeFormulario()
        {
            return new OrdenModelo
            {
                

            };
        }

        protected void rbtnTipoEntrega_SelectedIndexChanged(object sender, EventArgs e)
        {
            orden.TipoEntrega = rbtnTipoEntrega.SelectedValue;

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (id != null)
            {
                OrdenModelo Ob = ObtenerModeloDesdeFormulario();
                Ob.IdOrden = Convert.ToInt32(id);
                servicioOrden.Modificar(Ob);
            }
            else
            {
                servicioOrden.Agregar(ObtenerModeloDesdeFormulario());
            }
            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }   

        protected void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            // Eliminar producto de la orden
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            //Agregar producto a la orden
        }

        protected void btnEditarProducto_Click(object sender, EventArgs e)
        {
            //Editar producto de la orden
        }


        private Hashtable Getholiday()
        {
            Hashtable holiday = new Hashtable();
            holiday["1/1/2009"] = "New Year";
            holiday["1/5/2009"] = "Guru Govind Singh Jayanti";
            holiday["1/8/2009"] = "Muharam (Al Hijra)";
            holiday["1/14/2009"] = "Pongal";
            holiday["1/26/2009"] = "Republic Day";
            holiday["2/23/2009"] = "Maha Shivaratri";
            holiday["3/10/2009"] = "Milad un Nabi (Birthday of the Prophet";
            holiday["3/21/2009"] = "Holi";
            holiday["3/21/2009"] = "Telugu New Year";
            holiday["4/3/2009"] = "Ram Navmi";
            holiday["4/7/2009"] = "Mahavir Jayanti";
            holiday["4/10/2009"] = "Good Friday";
            holiday["4/12/2009"] = "Easter";
            holiday["4/14/2009"] = "Tamil New Year and Dr Ambedkar Birth Day";
            holiday["5/1/2009"] = "May Day";
            holiday["5/9/2009"] = "Buddha Jayanti and Buddha Purnima";
            holiday["6/24/2009"] = "Rath yatra";
            holiday["8/13/2009"] = "Krishna Jayanthi";
            holiday["8/14/2009"] = "Janmashtami";
            holiday["8/15/2009"] = "Independence Day";
            holiday["8/19/2009"] = "Parsi New Year";
            holiday["8/23/2009"] = "Vinayaka Chaturthi";
            holiday["9/2/2009"] = "Onam";
            holiday["9/5/2009"] = "Teachers Day";
            holiday["9/21/2009"] = "Ramzan";
            holiday["9/27/2009"] = "Ayutha Pooja";
            holiday["9/28/2009"] = "Vijaya Dasami (Dusherra)";
            holiday["10/2/2009"] = "Gandhi Jayanti";
            holiday["10/17/2009"] = "Diwali & Govardhan Puja";
            holiday["10/19/2009"] = "Bhaidooj";
            holiday["11/2/2009"] = "Guru Nanak Jayanti";
            holiday["11/14/2009"] = "Children's Day";
            holiday["11/28/2009"] = "Bakrid";
            holiday["12/25/2009"] = "Christmas";
            holiday["12/28/2009"] = "Muharram";
            return holiday;
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
            if (HolidayList[e.Day.Date.ToShortDateString()] != null)
            {
                Literal literal1 = new Literal();
                literal1.Text = "<br/>";
                e.Cell.Controls.Add(literal1);
                Label label1 = new Label();
                label1.Text = (string)HolidayList[e.Day.Date.ToShortDateString()];
                label1.Font.Size = new FontUnit(FontSize.Small);
                e.Cell.Controls.Add(label1);
            }
        }
    }

}