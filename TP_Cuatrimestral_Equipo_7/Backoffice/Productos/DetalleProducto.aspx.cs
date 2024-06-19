using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Productos
{
    public partial class DetalleProducto : System.Web.UI.Page
    {
        public Dominio.Modelos.ProductoModelo producto;
        public List<Dominio.Modelos.ItemDetalleProductoModelo> items;
        private Negocio.Servicios.ProductoServicio servicio;
        public string redirect_to = "/Backoffice/Productos";
        public decimal costoRecetas = 0;
        public decimal costoSuministros = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Guid id = Guid.TryParse(Request.QueryString["id"], out id) ? id : Guid.Empty;
                servicio = new Negocio.Servicios.ProductoServicio();
                if (id == Guid.Empty) Response.Redirect(redirect_to, false);
                try
                {
                    if (id != Guid.Empty)
                    {
                        producto = servicio.ObtenerPorId(id);
                        items = producto.Items;
                        CalcularCostos();
                    }

                }
                catch (Exception ex)
                {
                    Response.Redirect(redirect_to, false);
                }
            }
        }

        private void CalcularCostos()
        {
            foreach (ItemDetalleProductoModelo item in items)
            {
                if (item.Receta != null)
                {
                    costoRecetas += item.Receta.CostoTotal;
                }
                else
                {
                    costoSuministros += item.Suministro.Costo * item.Suministro.Cantidad;
                }
            }
        }
    }
}