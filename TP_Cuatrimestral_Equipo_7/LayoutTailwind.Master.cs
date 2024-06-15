using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7
{
    
    public class Toast
    {
        public string Message { get; set; }
        public string Type { get; set; }
    }
    public partial class LayoutTailwind : System.Web.UI.MasterPage
    {
        public List<Toast> Toasts;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Toasts = new List<Toast>();
                Session["Toasts"] = Toasts;
            }
            else
            {
                Toasts = (List<Toast>)Session["Toasts"];
            }
        }
        
        public void FireToasts()
        {
            string JSONList = Newtonsoft.Json.JsonConvert.SerializeObject(Toasts);
            JSONList = JSONList.Replace("\"", "'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "toasts", "FireToast(" + JSONList + ")", true);
            Toasts.Clear();
        }
    }
}