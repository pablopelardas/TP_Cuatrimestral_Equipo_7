﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Productos
{
    public partial class DetalleProducto : System.Web.UI.Page
    {
        public Dominio.Modelos.ProductoModelo producto;
        private Negocio.Servicios.ProductoServicio negocio;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["id"];
                negocio = new Negocio.Servicios.ProductoServicio();
                if (id == null) Response.Redirect("/Backoffice/Producto", false);
                try
                {
                    int idInt = Convert.ToInt32(Request.QueryString["id"]);
                    if (idInt > 0)
                    {
                        producto = negocio.ObtenerPorId(idInt);
                        if (producto != null)
                        {
                            lblNombre.Text = producto.Nombre;
                            lblDescripcion.Text = producto.Descripcion.ToString();
                            lblPorciones.Text = producto.Porciones.ToString();
                            lblHorasTrabajo.Text = producto.HorasTrabajo.ToString();
                            lblTipoPrecio.Text = producto.TipoPrecio.ToString();
                            lblValorPrecio.Text = producto.ValorPrecio.ToString();
                            lblCategoria.Text = producto.Categoria.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("/Backoffice/Productos", false);
                }

            }

        }
    }
}