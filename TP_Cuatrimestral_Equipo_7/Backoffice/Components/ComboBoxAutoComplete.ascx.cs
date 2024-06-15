using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Components
{
    public partial class ComboBoxAutoComplete : System.Web.UI.UserControl
    {

        public string ComboID { get; set; }
        
        public bool Enabled
        {
            get
            {
                return cboAutoComplete.Enabled;
            }
            set
            {
                if (cboAutoComplete != null)
                    cboAutoComplete.Enabled = value;
            }
        }

        public object SelectedValue
        {
            get
            {
                return cboAutoComplete.SelectedValue;
            }
            set
            {
               cboAutoComplete.SelectedValue = value.ToString();
            }
        }
        
        public string CssClass
        {
            get
            {
                return cboAutoComplete.CssClass;
            }
            set
            {
                cboAutoComplete.CssClass = value;
            }
        }

        private DropDownList cboAutoComplete;

        public void InicializarComboBox(Action<DropDownList> initComboBox, EventHandler OnSelectedIndexChanged = null, bool AutoPostBack = false)
        {
            if (ComboID == null)
            {
                ComboID = "cboAutoComplete" + Guid.NewGuid().ToString().Replace("-", "");
            }

            if (!IsPostBack)
            {
                cboAutoComplete = new DropDownList();
                cboAutoComplete.Attributes.Add("id", ComboID);
                cboAutoComplete.CssClass = "chzn-select";
                cboAutoComplete.AutoPostBack = AutoPostBack;
                initComboBox(cboAutoComplete);
                Session[ComboID] = cboAutoComplete;
            }
            else
            {
                if (Session[ComboID] != null)
                {
                    cboAutoComplete = (DropDownList)Session[ComboID];
                }
            }

            if (cboAutoComplete != null)
            {
                phComboBox.Controls.Add(cboAutoComplete);
                if (OnSelectedIndexChanged != null)
                    cboAutoComplete.SelectedIndexChanged += OnSelectedIndexChanged;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "InitChosen();", true);
        }
    }
}