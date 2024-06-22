using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Cuatrimestral_Equipo_7
{
    public partial class LayoutTailwind : System.Web.UI.MasterPage
    {
        
        public void FireToasts(string type, string title, string html = "")
        {
            string JSON = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                type = type,
                title = title,
                html = html
            });
            ScriptManager.RegisterStartupScript(this, this.GetType(), "toasts", "FireToast(" + JSON + ")", true);
        }
        
        public void FireToasts(string type, string title, List<string> messages)
        {
            string html = @"<ul class='list-disc list-inside'>";
            foreach (string message in messages)
            {
                html += @"<li class='text-sm'><span class='font-semibold'>" + message + "</span></li>";
            }
            html += "</ul>";
            FireToasts(type, title, html);
        }
    }
}