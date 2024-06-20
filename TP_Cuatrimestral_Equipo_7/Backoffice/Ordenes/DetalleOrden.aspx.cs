using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Dominio.Modelos;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Ordenes
{
    public partial class DetalleOrden : System.Web.UI.Page
    {
        public Dominio.Modelos.OrdenModelo orden;
        public List<Dominio.Modelos.OrdenEstadoModelo> estados;
        public string redirect_to = "/Backoffice/Ordenes";
        
        private Negocio.Servicios.OrdenServicio servicioOrden;
        private string OrdenActual = "dtl_orden_actual";
        private string Estados = "dtl_estados";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["redirect_to"] != null)
            {
                redirect_to = Request.QueryString["redirect_to"];
            }

            if (Session[OrdenActual] != null)
            {
                orden = (Dominio.Modelos.OrdenModelo)Session[OrdenActual];
            }

            if (Session[Estados] != null)
            {
                estados = (List<Dominio.Modelos.OrdenEstadoModelo>)Session[Estados];
            }
            

            btnGenerateShoppingList.Text = "Generar lista de compras";
            
        if (!IsPostBack)
            {
                Guid id = Guid.TryParse(Request.QueryString["id"], out id) ? id : Guid.Empty;
                servicioOrden = new Negocio.Servicios.OrdenServicio();
                if (id == Guid.Empty) Response.Redirect(redirect_to, false);
                try
                {
                    if (id != Guid.Empty)
                    {
                        orden = servicioOrden.ObtenerPorId(id);
                        estados = servicioOrden.ListarEstadosDeOrden();
                        if (orden == null) throw new Exception();
                        Session[OrdenActual] = orden;
                        Session[Estados] = estados;
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect(redirect_to, false);
                }

            }
            
            litOrdenExtra.Text = orden?.Descripcion ?? "";
            
            phEstados.Controls.Clear();
                
            foreach (OrdenEstadoModelo estado in estados)
            {     
                Button btn = new Button
                {
                    Text = estado.Nombre,
                    CssClass = $"w-full justify-center {estado.PillClass} cursor-pointer",
                    CommandName = "CambiarEstado",
                    CommandArgument = estado.IdOrdenEstado.ToString(),
                };
                btn.Click += Btn_Click;
                phEstados.Controls.Add(btn);
            }
        }
        
        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int estado = int.Parse(btn.CommandArgument);
            servicioOrden = new Negocio.Servicios.OrdenServicio();
            orden = servicioOrden.CambiarEstado(orden.IdOrden, estado);
            Session[OrdenActual] = orden;
        }
        
        
        // generate printable pdf from order ListaCompra

        protected void GenerateShoppingList(object sender, EventArgs e)
        {
            try
            {
                List<string> lines = new List<string>();
                lines.Add("<tr class='subtitle-row'><td colspan=\"6\"><h4>Ingredientes</h4></td></tr>");
                foreach (IngredienteDetalleRecetaModelo detalleIngrediente in orden.ListaCompra.Ingredientes)
                {
                    StringBuilder row = new StringBuilder();
                    row.Append("<tr>");
                    row.Append("<td>" + detalleIngrediente.Ingrediente.Nombre + "</td>");
                    row.Append("<td>" + detalleIngrediente.Cantidad + "</td>");
                    row.Append("<td>" + detalleIngrediente.Ingrediente.Unidad.Nombre + "</td>");
                    row.Append("<td>$ " + detalleIngrediente.Ingrediente.CostoNormalizado + "</td>");
                    row.Append("<td>" + detalleIngrediente.Ingrediente.Proveedor + "</td>");
                    row.Append("<td>$ " + detalleIngrediente.Subtotal + "</td>");
                    row.Append("</tr>");
                    
                    lines.Add(row.ToString());
                }
                // add subtotal ingredientes
                lines.Add("<tr class='subtotal-row'><td colspan=\"5\"></td><td>$ " + orden.ListaCompra.TotalIngredientes + "</td></tr>");
                
                // add separator row for suministros
               lines.Add("<tr class='subtitle-row'><td colspan=\"6\"><h4>Suministros</h4></td></tr>");
                
                foreach (ItemDetalleProductoModelo detalleSuministro in orden.ListaCompra.Suministros)
                {
                    StringBuilder row = new StringBuilder();
                    row.Append("<tr>");
                    row.Append("<td>" + detalleSuministro.Suministro.Nombre + "</td>");
                    row.Append("<td></td>");
                    row.Append("<td>" + detalleSuministro.Cantidad + "</td>");
                    row.Append("<td>$ " + detalleSuministro.Suministro.Costo + "</td>");
                    row.Append("<td>" + detalleSuministro.Suministro.Proveedor + "</td>");
                    row.Append("<td>$ " + detalleSuministro.Cantidad * (detalleSuministro.Suministro.Costo / detalleSuministro.Suministro.Cantidad) + "</td>");
                    row.Append("</tr>");
                    
                    lines.Add(row.ToString());
                }
                // add subtotal suministros
                lines.Add("<tr class='subtotal-row'><td colspan=\"5\"></td><td>$ " + orden.ListaCompra.TotalSuministros + "</td></tr>");
                
                StringBuilder sbTotal = new StringBuilder();
                // tr for total row with total in last td
                sbTotal.Append("<tr class='total-row'><td colspan=\"5\"><h4>Total</h4></td><td>$ " + orden.ListaCompra.Total + "</td></tr>");
                
                //group lines in chunks of 20 rows
                List<string> chunks = new List<string>();
                StringBuilder sb = new StringBuilder();
                int chunkSize = 17;
                for (int i = 0; i < lines.Count; i++)
                {
                    sb.Append(lines[i]);
                    if (i % chunkSize == 0 && i != 0)
                    {
                        chunks.Add(sb.ToString());
                        sb.Clear();
                    }
                }
                chunks.Add(sb.ToString());
                // add total row to last chunk
                chunks[chunks.Count - 1] += sbTotal.ToString();
                
                
                
                
                

                string style = @"
.page {
      margin: auto;
      margin-top: 10mm;
      border: 1px solid black;
      width: 18cm;
      height: 27cm;
  }

@page {
      margin: 0;
      size: A4 portrait;
  }

table {
      width: 100%;
      border-collapse: collapse;
  }

th, td {
      border: 1px solid black;
      padding: 8px;
      text-align: left;
  }

th {
      background-color: #f2f2f2;
  }

tr:nth-child(even) {
      background-color: #f2f2f2;
  }

tr:hover {
      background-color: #f5f5f5;
  }

h2{
        font-size: 1.5em;
        text-align: center;
        padding: 10px;
}

.subtitle-row{
        background-color: #f2f2f2;
        font-weight: bold;
}
h4{
        padding-block: 5px;
        margin: 0px;
}

.subtotal-row {
      background-color: #f2f2f2;
      font-weight: bold;
  }

.total-row {
      background-color: #f2f2f2;
      font-weight: bold;
      font-size: 1.2em;
  }
";

                if (chunks.Count > 0)
                {
                    for (int i = 0; i < chunks.Count; i++)
                    {
                        string title = i == 0 ? "<h2>Lista de compras</h2>" : "";
                        chunks[i] = $@"
<div class=""page"">
{title}
  <table>
    <thead>
        <tr>
            <th>Item</th>
            <th>Cantidad</th>
            <th>Unidad</th>
            <th>Costo unitario ≈</th>
            <th>Proveedor</th>
            <th>Subtotal ≈</th>       
        </tr>
    </thead>
    <tbody>
        {chunks[i]}
    </tbody>
    </table>
</div>
<div style=""page-break-after: always""></div>";
                    }
                }

                string shoppinglist = $@"
<!DOCTYPE html>
<html lang=""en"">
    <head>
      <meta charset=""UTF-8"">
      <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
      <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
      <title>Document</title>
      <style type=""text/css"">
        {style}
      </style>
    </head>
    <body>
       {string.Join("", chunks)}
   </body>
</html>
";
                
            // <div class=""page"">
            // <h1>Page 2</h1>
            // <img src=""/pdf.jpg"" style=""width: 100%; height: auto;"" />
            // </div>
            // <div style=""page-break-after: always""></div>
                
                // Negocio.Servicios.PdfServicio.GeneratePdfAttachment(shoppinglist, "ListaCompra.pdf");
                Negocio.Servicios.PdfServicio.GeneratePdfInline(shoppinglist);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                ((LayoutTailwind)Master)?.FireToasts();
            }
        }
    }
}