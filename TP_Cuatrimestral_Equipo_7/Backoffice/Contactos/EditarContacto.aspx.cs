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
        public string FechaEventoSeleccionada;
        public string redirect_to = "Default.aspx";
        private List<Guid> idsDireccionesBorradas = new List<Guid>();
        private List<Guid> idsEventosBorrados = new List<Guid>();
        
        private List<string> ToastMessages = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            negocio = new Negocio.Servicios.ContactoServicio();
            id = Guid.TryParse(Request.QueryString["id"], out id) ? id : Guid.Empty; ;
            if (ViewState["contacto"] != null)
            {
                contacto = (Dominio.Modelos.ContactoModelo)ViewState["contacto"];
            }
            if (ViewState["idsDireccionesBorradas"] != null)
            {
                idsDireccionesBorradas = (List<Guid>)ViewState["idsDireccionesBorradas"];
            }
            if (ViewState["idsEventosBorrados"] != null)
            {
                idsEventosBorrados = (List<Guid>)ViewState["idsEventosBorrados"];
            }
            
            if (Request.QueryString["redirect_to"] != null)
            {
                redirect_to = Request.QueryString["redirect_to"];
            }
            
            
            if (!IsPostBack)
            {
                if (id == Guid.Empty)
                {
                    contacto = new Dominio.Modelos.ContactoModelo();
                    contacto.Eventos = new List<EventoModelo>();
                    contacto.Direcciones = new List<DireccionModelo>();
                } else
                {
                    try
                    {
                        if (id != Guid.Empty)
                        {
                            contacto = negocio.ObtenerPorId(id);
                            Negocio.Servicios.EventoServicio negocioEventos = new Negocio.Servicios.EventoServicio();

                            contacto.Eventos = negocioEventos.ListarEventosPorCliente(contacto.Id, true);
                            ViewState["tiposDeEvento"] = negocioEventos.ListarTipoDeEventos();
                            if (contacto != null)
                            {
                                ddTipoContacto.SelectedValue = contacto.Rol;
                                txtNombreYApellido.Text = contacto.NombreApellido;
                                txtCorreo.Text = contacto.Email;
                                txtTelefono.Text = contacto.Telefono;
                                ddFuente.SelectedValue = contacto.Fuente;
                                chkDeseaRecibirCorreos.Checked = contacto.DeseaRecibirCorreos;
                                chkDeseaRecibirWhatsapps.Checked = contacto.DeseaRecibirWhatsapp;

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                ViewState["contacto"] = contacto;
                rptDirecciones.DataSource = contacto.Direcciones;
                rptDirecciones.DataBind();
                rptEventos.DataSource = contacto.Eventos;
                rptEventos.DataBind();


            }
            CargarCalendario();

        }

        private ContactoModelo ObtenerModeloDesdeFormulario()
        {
            bool hayError = id != Guid.Empty && !Page.IsValid;
            if (id != Guid.Empty && string.IsNullOrEmpty(txtJustificacion.Text))
            {
                ToastMessages.Add("Debe justificar la modificación de la orden");
                hayError = true;
            }
            contacto.NombreApellido = txtNombreYApellido.Text;
            contacto.Email = txtCorreo.Text;
            contacto.Telefono = txtTelefono.Text;
            contacto.Rol = ddTipoContacto.SelectedValue;
            contacto.Fuente = ddFuente.SelectedValue;
            contacto.DeseaRecibirCorreos = chkDeseaRecibirCorreos.Checked;
            contacto.DeseaRecibirWhatsapp = chkDeseaRecibirWhatsapps.Checked;
            contacto.InformacionPersonal = txtContactoExtra.Text;
            if (hayError)
            {
                throw new Exception("Error al guardar el contacto");
            }

            return contacto;
        }
        
        public void validateJustificacion(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = !string.IsNullOrEmpty(txtJustificacion.Text) && txtJustificacion.Text.Length > 10;
            if (!e.IsValid)
            {
                ToastMessages.Add("La justificación debe tener al menos 10 caracteres");
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            try
            {
                ContactoModelo Ob = ObtenerModeloDesdeFormulario();
                if (id != Guid.Empty)
                {
                    Ob.Id = id;
                }

                ContactoModelo updatedContacto = negocio.GuardarContacto(Ob, idsDireccionesBorradas, idsEventosBorrados);
                
                contacto.Id = updatedContacto.Id;
                
                string justificacion = id == Guid.Empty ? "Se ha creado un nuevo contacto" : txtJustificacion.Text;
                Negocio.Servicios.HistoricoServicio historicoServicio = new Negocio.Servicios.HistoricoServicio();
                historicoServicio.GeneraryGuardarHistorico(contacto.Id, justificacion);
                Master?.FireToasts("success", "Contacto guardado correctamente", "");
                Session["FIRE_TOASTS"] = new LayoutTailwind.Toast
                {
                    type = "success",
                    title = "Contacto guardado correctamente",
                    html = ""
                };
                
                Response.Redirect(redirect_to, false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);

                Master?.FireToasts("error", "Error al guardar la orden", ToastMessages);
                ToastMessages.Clear();
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
        
        protected void btnAgregarEvento_Click(object sender, EventArgs e)
        {
            ViewState["updatingEvento"] = null;
            lblTitleModalEvento.Text = "Agregar Evento";
            txtEventoDesc.Text = "";
            FechaEventoSeleccionada = DateTime.Now.ToShortDateString();
            txtEventoDesc.Text = "";
            ddTipoEvento.SelectedValue = null;
            ScriptManager.RegisterStartupScript(this, GetType(), "text", "LoadEventoDesc();", true);
            AbrirModal("modalEvento");
        }
        
        protected void btnEliminarDireccion_Click(object sender, EventArgs e)
        {
            string idDireccion = ((Button)sender).CommandArgument;
            idsDireccionesBorradas.Add(Guid.Parse(idDireccion));
            ViewState["idsDireccionesBorradas"] = idsDireccionesBorradas;
            contacto.Direcciones.RemoveAll(x => x.IdDireccion == Guid.Parse(idDireccion));
            rptDirecciones.DataSource = contacto.Direcciones;
            rptDirecciones.DataBind();
        }
        
        protected void btnEliminarEvento_Click(object sender, EventArgs e)
        {
            string idEvento = ((Button)sender).CommandArgument;
            idsEventosBorrados.Add(Guid.Parse(idEvento));
            ViewState["idsEventosBorrados"] = idsEventosBorrados;
            contacto.Eventos.RemoveAll(x => x.IdEvento == Guid.Parse(idEvento));
            rptEventos.DataSource = contacto.Eventos;
            rptEventos.DataBind();
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
        
        protected void btnEditarEvento_Click(object sender, EventArgs e)
        {
            string idEvento = ((Button)sender).CommandArgument;
            EventoModelo evento = contacto.Eventos.FirstOrDefault(x => x.IdEvento == Guid.Parse(idEvento));
            FechaEventoSeleccionada = evento.Fecha.ToShortDateString();
            txtEventoDesc.Text = evento.Descripcion;
            ddTipoEvento.SelectedValue = evento.TipoEvento.IdTipoEvento.ToString();
            ViewState["updatingEvento"] = idEvento;
            lblTitleModalEvento.Text = "Editar Evento";
            AbrirModal("modalEvento");
        }
        
        protected void OnGuardarModalEvento(object sender, EventArgs e)
        {
            if (ViewState["updatingEvento"] != null)
            {
                EventoModelo evento = contacto.Eventos.FirstOrDefault(x => x.IdEvento == Guid.Parse((string)ViewState["updatingEvento"]));
                evento.Fecha = DateTime.Parse(FechaEventoSeleccionada);
                evento.Descripcion = txtEventoDesc.Text;
                evento.TipoEvento = ((List<TipoEventoModelo>)ViewState["tiposDeEvento"])
                    ?.FirstOrDefault(x => x.IdTipoEvento == Guid.Parse(ddTipoEvento.SelectedValue));
                ViewState["updatingEvento"] = null;
            }
            else
            {
                contacto.Eventos.Add(new EventoModelo
                {
                    Fecha = DateTime.Parse(FechaEventoSeleccionada),
                    Descripcion = txtEventoDesc.Text,
                    TipoEvento = ((List<TipoEventoModelo>)ViewState["tiposDeEvento"])
                        ?.FirstOrDefault(x => x.IdTipoEvento == Guid.Parse(ddTipoEvento.SelectedValue))
                });
            };
            ViewState["contacto"] = contacto;
            rptEventos.DataSource = contacto.Eventos;
            rptEventos.DataBind();
            HideModal("modalEvento");
        }
        protected void onGuardarModalDireccion(object sender, EventArgs e)
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
        
        private void CargarCalendario()
        {
            Components.Calendario calendario = (Backoffice.Components.Calendario)LoadControl("~/Backoffice/Components/Calendario.ascx");
            phCalendarioEvento.Controls.Add(calendario);
            calendario.InicializarCalendario((object sender, EventArgs e) =>
            {
                FechaEventoSeleccionada = calendario.FechaCalendario.ToShortDateString();
            });
            calendario.CellSizeTWClass = "h-8";
            FechaEventoSeleccionada = calendario.FechaCalendario.ToShortDateString();
        }
        
        
        
        private void AbrirModal(string id)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", $"ShowModal({id});", true);
        }
        
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        } 
        
        protected void OnDescripcionEventoLoad(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "text", "LoadEventoDesc();", true);
        }       
        protected void OnContactoExtraLoad(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "text", "LoadContactoExtra();", true);
        }   
        
        public void HideModalDireccion(object sender, EventArgs e)
        {
            HideModal("modalDireccion");
        }
        
        public void HideModalEvento(object sender, EventArgs e)
        {
            HideModal("modalEvento");
        }

        public void HideModal(string id)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", $"HideModal({id});", true);
        }
        
        

    }
}