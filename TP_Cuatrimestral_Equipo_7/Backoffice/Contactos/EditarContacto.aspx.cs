using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Contactos
{
    public partial class EditarContacto : System.Web.UI.Page
    {
        public Dominio.Modelos.ContactoModelo contacto;
        private Negocio.Servicios.ContactoServicio negocio;
        public Guid id = Guid.Empty;
        public string redirect_to = "Contactos.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            negocio = new Negocio.Servicios.ContactoServicio();
            id = Guid.TryParse(Request.QueryString["id"], out id) ? id : Guid.Empty; ;
            if (ViewState["contacto"] != null)
            {
                contacto = (Dominio.Modelos.ContactoModelo)ViewState["contacto"];
            }
            
            if (!IsPostBack)
            {
                if (id == null)
                {
                    contacto = new Dominio.Modelos.ContactoModelo();
                } else
                {
                    try
                    {
                        if (id != Guid.Empty)
                        {
                            contacto = negocio.ObtenerPorId(id);
                            ViewState["contacto"] = contacto;
                            if (contacto != null)
                            {
                                ddTipoContacto.SelectedValue = contacto.Rol;
                                txtNombreYApellido.Text = contacto.NombreApellido;
                                txtCorreo.Text = contacto.Email;
                                txtTelefono.Text = contacto.Telefono;
                                ddFuente.SelectedValue = contacto.Fuente;
                                // txtDireccion.Text = contacto.Direcciones.FirstOrDefault()?.CalleNumero;
                                chkDeseaRecibirCorreos.Checked = contacto.DeseaRecibirCorreos;
                                chkDeseaRecibirWhatsapps.Checked = contacto.DeseaRecibirWhatsapp;
                                rptDirecciones.DataSource = contacto.Direcciones;
                                rptDirecciones.DataBind();
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

        private ContactoModelo ObtenerModeloDesdeFormulario()
        {
            return new ContactoModelo
            {
                // NombreApellido = txtNombreApellido.Text,
                // Email = txtCorreo.Text,
                // Telefono = txtTelefono.Text,
                // Direcciones = new List<DireccionModelo>
                // {
                //     new DireccionModelo
                //     {
                //         CalleNumero = txtDireccion.Text
                //     }
                // },
                // Rol = ddlTipo.SelectedValue == "1" ? "Cliente" : "Proveedor",
                // Fuente = txtFuente.Text,
                // DeseaRecibirCorreos = chkDeseaRecibirCorreos.Checked,
                // DeseaRecibirWhatsapp = chkDeseaRecibirWhatsapps.Checked,
                // InformacionPersonal = tiny.Text
            };
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (id != Guid.Empty)
            {
                ContactoModelo Ob = ObtenerModeloDesdeFormulario();
                Ob.Id = id;
                negocio.Modificar(Ob);
            }
            else
            {
                negocio.Agregar(ObtenerModeloDesdeFormulario());
            }
            
        }
        
        protected void btnAgregarDireccion_Click(object sender, EventArgs e)
        {
            
            ViewState["updatingDireccion"] = null;
            lblTitleModalDireccion.Text = "Agregar Dirección";
            
            txtCalleYNumero.Text = "";
            txtLocalidad.Text = "";
            txtProvincia.Text = "";
            txtCodigoPostal.Text = "";
            txtPiso.Text = "";
            txtDepartamento.Text = "";
            
            AbrirModal("modalDireccion");
        }
        
        protected void btnEliminarDireccion_Click(object sender, EventArgs e)
        {
            // contacto.Direcciones.RemoveAt(contacto.Direcciones.Count - 1);
            // ViewState["contacto"] = contacto;
        }
        
        protected void btnEditarDireccion_Click(object sender, EventArgs e)
        {
            
            string idDireccion = ((Button)sender).CommandArgument;
            DireccionModelo direccion = contacto.Direcciones.FirstOrDefault(x => x.IdDireccion == Guid.Parse(idDireccion));
            txtCalleYNumero.Text = direccion.CalleNumero;
            txtLocalidad.Text = direccion.Localidad;
            txtProvincia.Text = direccion.Provincia;
            txtCodigoPostal.Text = direccion.CodigoPostal;
            txtPiso.Text = direccion.Piso;
            txtDepartamento.Text = direccion.Departamento;
            ViewState["updatingDireccion"] = idDireccion;
            lblTitleModalDireccion.Text = "Editar Dirección";
            
            AbrirModal("modalDireccion");
        }
        
        protected void OnAceptarAgregarDireccion(object sender, EventArgs e)
        {
            if (ViewState["updatingDireccion"] != null)
            {
                DireccionModelo direccion = contacto.Direcciones.FirstOrDefault(x => x.IdDireccion == Guid.Parse((string)ViewState["updatingDireccion"]));
                direccion.CalleNumero = txtCalleYNumero.Text;
                direccion.Localidad = txtLocalidad.Text;
                direccion.Provincia = txtProvincia.Text;
                direccion.CodigoPostal = txtCodigoPostal.Text;
                direccion.Piso = txtPiso.Text;
                direccion.Departamento = txtDepartamento.Text;
                ViewState["updatingDireccion"] = null;
            }
            else
            {
                contacto.Direcciones.Add(new DireccionModelo
                {
                    CalleNumero = txtCalleYNumero.Text,
                    Localidad = txtLocalidad.Text,
                    Provincia = txtProvincia.Text,
                    CodigoPostal = txtCodigoPostal.Text,
                    Piso = txtPiso.Text,
                    Departamento = txtDepartamento.Text
                });
            };
            rptDirecciones.DataSource = contacto.Direcciones;
            rptDirecciones.DataBind();
            HideModal("modalDireccion");
        }
        
        private void AbrirModal(string id)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", $"ShowModal({id});", true);
        }
        
        // private void FirePostBackEvent(string eventTarget, bool async = false)
        // {
        //     if (async)
        //     {
        //         ScriptManager.RegisterStartupScript(this, this.GetType(), "PostBack", "__doPostBack('" + eventTarget + "','async');", true);
        //         return;
        //     }
        //     ScriptManager.RegisterStartupScript(this, this.GetType(), "PostBack", "__doPostBack('" + eventTarget + "','');", true);
        // }
        
        // protected void OnInfoExtraLoad(object sender, EventArgs e)
        // {
        //     ScriptManager.RegisterStartupScript(this, GetType(), "text", "LoadInfoExtra();", true);
        // }       
        
        public void HideModalDireccion(object sender, EventArgs e)
        {
            HideModal("modalDireccion");
        }

        public void HideModal(string id)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", $"HideModal({id});", true);
        }
        

    }
}