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
        private Negocio.Servicios.ProductoServicio servicio;
        public string redirect_to = "/Backoffice/Productos";
        public decimal costoRecetas = 0;
        public decimal costoSuministros = 0;
        public List<ItemDetalleProductoModelo> Recetas;
        public List<ItemDetalleProductoModelo> Suministros;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Guid id = Guid.TryParse(Request.QueryString["id"], out id) ? id : Guid.Empty;
                servicio = new Negocio.Servicios.ProductoServicio();
                if (id == Guid.Empty) Response.Redirect(redirect_to, false);
                Recetas = new List<ItemDetalleProductoModelo>();
                Suministros = new List<ItemDetalleProductoModelo>();
                try
                {
                    if (id != Guid.Empty)
                    {
                        producto = servicio.ObtenerPorId(id);
                        CargarListas();
                    }

                }
                catch (Exception ex)
                {
                    Response.Redirect(redirect_to, false);
                }
            }
        }

        private void CargarListas()
        {
            Recetas = producto.Items.Where(x => x.Receta != null).ToList();
            Suministros = producto.Items.Where(x => x.Suministro != null).ToList();
        }
    }
}