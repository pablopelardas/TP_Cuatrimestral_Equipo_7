using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7.Contactos
{
    public partial class EditarContacto : System.Web.UI.Page
    {
        public Dominio.Modelos.ContactoModelo contacto;
        private Negocio.Servicios.ContactoServicio negocio;
        public string id = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            negocio = new Negocio.Servicios.ContactoServicio();
            id = Request.QueryString["id"];
            
            if (!IsPostBack)
            {
                if (id == null)
                {
                    contacto = new Dominio.Modelos.ContactoModelo();
                } else
                {
                    try
                    {
                        int idInt = Convert.ToInt32(Request.QueryString["id"]);
                        if (idInt > 0)
                        {
                            contacto = negocio.ObtenerPorId(idInt);
                            if (contacto != null)
                            {
                                ddlTipo.SelectedValue = contacto.Rol == "Cliente" ? "1" : "2";
                                txtNombreApellido.Text = contacto.NombreApellido;
                                txtCorreo.Text = contacto.Email;
                                txtTelefono.Text = contacto.Telefono;
                                txtDireccion.Text = contacto.Direccion;
                                txtFuente.Text = contacto.Fuente;
                                chkDeseaRecibirCorreos.Checked = contacto.DeseaRecibirCorreos;
                                chkDeseaRecibirWhatsapps.Checked = contacto.DeseaRecibirWhatsapp;
                                tiny.Text = contacto.InformacionPersonal;
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

        private ContactoModelo ObtenerModeloDesdeFormulario()
        {
            return new ContactoModelo
            {
                NombreApellido = txtNombreApellido.Text,
                Email = txtCorreo.Text,
                Telefono = txtTelefono.Text,
                Direccion = txtDireccion.Text,
                Rol = ddlTipo.SelectedValue == "1" ? "Cliente" : "Proveedor",
                Fuente = txtFuente.Text,
                DeseaRecibirCorreos = chkDeseaRecibirCorreos.Checked,
                DeseaRecibirWhatsapp = chkDeseaRecibirWhatsapps.Checked,
                InformacionPersonal = tiny.Text
            };
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (id != null)
            {
                ContactoModelo Ob = ObtenerModeloDesdeFormulario();
                Ob.Id = Convert.ToInt32(id);
                negocio.Modificar(Ob);
            }
            else
            {
                negocio.Agregar(ObtenerModeloDesdeFormulario());
            }
            
        }

    }
}