using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Modelos
{
    public class ListaCompra
    {
        public List<IngredienteDetalleRecetaModelo> Ingredientes { get; set; }
        public List<ItemDetalleProductoModelo> Suministros { get; set; }
        
        public decimal TotalSuministros
        {
            get
            {
                return decimal.Round(Suministros.Sum(x => x.SubTotal), 2);
            }
        }
        
        public decimal TotalIngredientes
        {
            get
            {
                return decimal.Round(Ingredientes.Sum(x => x.Subtotal), 2);
            }
        }
        
        public decimal Total
        {
            get
            {
                return decimal.Round(TotalIngredientes + TotalSuministros, 2);
            }
        }
        
        public ListaCompra(List<ItemDetalleProductoModelo> items)
        {
            Suministros = items.Where(x => x.Suministro != null).ToList();
            
            Ingredientes = items.Where(x => x.Receta != null)
                .SelectMany(item => item.Receta.DetalleRecetas)
                .GroupBy(detalle => detalle.Ingrediente.IdIngrediente)
                .Select(group => new IngredienteDetalleRecetaModelo
                {
                    Ingrediente = group.First().Ingrediente,
                    Cantidad = group.Sum(detalle => detalle.Cantidad)
                })
                .ToList();
        }
        
        public ListaCompra(List<ListaCompra> listas)
        {
            Ingredientes = listas.SelectMany(lista => lista.Ingredientes)
                .GroupBy(ingrediente => ingrediente.Ingrediente.IdIngrediente)
                .Select(group => new IngredienteDetalleRecetaModelo
                {
                    Ingrediente = group.First().Ingrediente,
                    Cantidad = group.Sum(ingrediente => ingrediente.Cantidad)
                })
                .ToList();
            
            Suministros = listas.SelectMany(lista => lista.Suministros)
                .GroupBy(suministro => suministro.Suministro.IdSuministro)
                .Select(group => new ItemDetalleProductoModelo
                {
                    Suministro = group.First().Suministro,
                    Cantidad = group.Sum(suministro => suministro.Cantidad)
                })
                .ToList();
        }
        
        private string table_Style = @"
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




.title{
        font-size: 1.5em;
        text-align: center;
        margin-block-end: 10px;
}
.subtitle{
        font-size: 1.1em;
        text-align: center;
        padding: 5px;
}


.subtitle-row{
        background-color: #f2f2f2;
        font-weight: bold;
}

.subtotal-row {
      background-color: #f2f2f2;
      font-weight: bold;
  }

.subtotal-row span{
      display: inline-block;
      padding-block: 2px;
      text-transform: uppercase;
  }

.total-row {
      background-color: #f2f2f2;
      font-weight: bold;
  }
.total-row span{
      display: inline-block;
      padding-block: 2px;
      text-transform: uppercase;
}
";
        private string GenerateTable()
        {
            string table = "<table>";
            table += "<tr class='subtitle-row'><th>Ingrediente</th><th>Cantidad</th><th>Unidad</th><th>Costo</th><th>Proveedor</th><th>Subtotal</th></tr>";
            foreach (var ingrediente in Ingredientes)
            {
                StringBuilder row = new StringBuilder();
                row.Append("<tr>");
                row.Append("<td>" + ingrediente.Ingrediente.Nombre + "</td>");
                row.Append("<td>" + ingrediente.Cantidad + "</td>");
                row.Append("<td>" + ingrediente.Ingrediente.Unidad.Nombre + "</td>");
                row.Append("<td>$ " + ingrediente.Ingrediente.CostoNormalizado + "</td>");
                row.Append("<td>" + ingrediente.Ingrediente.Proveedor + "</td>");
                row.Append("<td>$ " + ingrediente.Subtotal + "</td>");
                row.Append("</tr>");
                table += row.ToString();
            }
            table += $"<tr class='subtotal-row'><td colspan=\"5\"><span>Total Ingredientes</span></td><td>$ {TotalIngredientes}</td></tr>";
            
            
            table += "<tr class='subtitle-row'><th>Suministro</th><th></th><th>Cantidad</th><th>Costo</th><th>Proveedor</th><th>Subtotal</th></tr>";
            foreach (var suministro in Suministros)
            {
                StringBuilder row = new StringBuilder();
                row.Append("<tr>");
                row.Append("<td>" + suministro.Suministro.Nombre + "</td>");
                row.Append("<td></td>");
                row.Append("<td>" + suministro.Cantidad + "</td>");
                row.Append("<td>$ " + suministro.Suministro.Costo + "</td>");
                row.Append("<td>" + suministro.Suministro.Proveedor + "</td>");
                // row.Append("<td>$ " + suministro.Cantidad * (suministro.Suministro.Costo / suministro.Suministro.Cantidad) + "</td>");
                row.Append("<td>$ " + suministro.SubTotal + "</td>");
                row.Append("</tr>");
                // table += $"<tr><td>{suministro.Suministro.Nombre}</td><td>{suministro.Cantidad}</td></tr>";
                table += row.ToString();
            }
            // table += $"<tr class='subtotal-row'><td>Total Suministros</td><td>{TotalSuministros}</td></tr>";
            table += $"<tr class='subtotal-row'><td colspan=\"5\"><span>Total Suministros</span></td><td>$ {TotalSuministros}</td></tr>";
            // table += $"<tr class='total-row'><td>Total</td><td>{Total}</td></tr>";
            table += $"<tr class='total-row'><td colspan=\"5\"><span>Total</span></td><td>$ {Total}</td></tr>";
            table += "</table>";
            return table;
        }

    
        public string GenerateHTML(string subtitle = "")
        {
            string _subtitle = string.IsNullOrEmpty(subtitle) ? "" : $"<h2 class='subtitle'>{subtitle}</h2>";
            string html = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <style type=""text/css"">
                    {table_Style}
                </style>
            </head>
            <body>
                <h1 class='title'>Lista de Compra</h1>
                {_subtitle}
                {GenerateTable()}
            </body>
            </html>
            ";
            return html;
        }

    }
}