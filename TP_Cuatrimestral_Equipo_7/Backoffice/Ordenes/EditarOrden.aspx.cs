using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Ordenes
{
    public partial class EditarOrden : System.Web.UI.Page
    {
        public Dominio.Modelos.OrdenModelo orden;
        private Negocio.Servicios.OrdenServicio servicioOrden;
        public string id = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            servicioOrden = new Negocio.Servicios.OrdenServicio();
            id = Request.QueryString["id"];
            
            if (!IsPostBack)
            {
                if (id == null)
                {
                    orden = new Dominio.Modelos.OrdenModelo();
                } else
                {
                    try
                    {
                        int idInt = Convert.ToInt32(Request.QueryString["id"]);
                        if (idInt > 0)
                        {
                            orden = servicioOrden.ObtenerPorId(idInt);
                            if (orden != null)
                            {
                               
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }

        }
    
        protected void OnTinyLoad(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "text", "LoadTiny();", true);
        }

        private OrdenModelo ObtenerModeloDesdeFormulario()
        {
            return new OrdenModelo
            {

            };
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

    }
}