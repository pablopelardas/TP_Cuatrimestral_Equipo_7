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
        
        public EventHandler OnSelectedIndexChanged { get; set; }
        
        public bool Enabled
        {
            get => ddAutocomplete.Enabled;
            set
            {
                if (ddAutocomplete != null)
                    ddAutocomplete.Enabled = value;
            }
        }

        public object SelectedValue
        {
            get => ddAutocomplete.SelectedValue;
            set => ddAutocomplete.SelectedValue = value.ToString();
        }
        
        public string CssClass
        {
            get => ddAutocomplete.CssClass;
            set => ddAutocomplete.CssClass = value;
        }


        public void InicializarComboBox(DataSourceControl dataSource, Action<DropDownList> initDd, EventHandler _OnSelectedIndexChanged = null, bool AutoPostBack = false)
        {
            OnSelectedIndexChanged = _OnSelectedIndexChanged;
            if (!IsPostBack)
            {
                ddAutocomplete.DataSourceID = dataSource.ID;
                ddAutocomplete.AutoPostBack = AutoPostBack;
                initDd(ddAutocomplete);
            }
                ddAutocomplete.SelectedIndexChanged += OnSelectedIndexChanged;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "InitChosen", "InitChosen();", true);
                

                
        }
    }
}