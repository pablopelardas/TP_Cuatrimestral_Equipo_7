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

        private DropDownList cboAutoComplete;

        public void InicializarComboBox(Action<DropDownList> initComboBox)
        {
            if (ComboID == null)
            {
                ComboID = "cboAutoComplete" + Guid.NewGuid().ToString().Replace("-", "");
            }
            cboAutoComplete = new DropDownList();
            cboAutoComplete.Attributes.Add("id", ComboID);
            cboAutoComplete.Attributes.Add("class", "chzn-select");
            phComboBox.Controls.Add(cboAutoComplete);
            if (!IsPostBack)
            {
                initComboBox(cboAutoComplete);
            }
        }
    }
}