using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Components
{
    public partial class DetailList : System.Web.UI.UserControl
    {
        public string CustomID { get; set; }
        public string ModalTitle { get; set; }

        private GridView gvData;
        Action<GridView> _init;

        public void InicializarListaDetalle(Action<GridView> initGridData, Control phHeader = null, Control phFooter = null, Action<GridView> registerEvents = null)
        {
            if (CustomID == null)
            {
                CustomID = "gvData" + Guid.NewGuid().ToString().Replace("-", "");
            }
            gvData = new GridView();
            gvData.Attributes.Add("id", CustomID);
            phList.Controls.Add(gvData);
            _init = initGridData;
            if (phHeader != null)
            {
                phListHeader.Controls.Add(phHeader);
            }
            if (phFooter != null)
            {
                phListFooter.Controls.Add(phFooter);
            }
            // if (!IsPostBack)
            // {
            // }
                _init(gvData);
            if (registerEvents != null) {
                registerEvents(gvData);
            }
        }

        public void CargarListaDetalle()
        {
            _init(gvData);
        }

        public void CargarModal(string ModalTitle = "", Control controlModalBody = null, EventHandler onGuardarModal = null, EventHandler onCancelarModal = null)
        {
            if (ModalTitle != "")
            {
                this.ModalTitle = ModalTitle;
            } else             {
                this.ModalTitle = "Título";
            }
            if (controlModalBody != null)
            {
                pnlModalBody.Controls.Add(controlModalBody);
            }
            if (onGuardarModal != null)
            {
                btnGuardarModal.Click += onGuardarModal;
            }
            if (onCancelarModal != null)
            {
                btnCancelarModal.Click += onCancelarModal;
            }

        }

        public void MostrarModal()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowModal();", true);
        }

    }
}