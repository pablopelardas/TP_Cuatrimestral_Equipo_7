using System.Collections.Generic;
using System.Linq;

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
            
            // Ingredientes = items.Where(x => x.Receta != null)
            //     .SelectMany(item => item.Receta.DetalleRecetas)
            //     .GroupBy(detalle => detalle.Ingrediente.IdIngrediente)
            //     .Select(group => new IngredienteDetalleRecetaModelo
            //     {
            //         Ingrediente = group.First().Ingrediente,
            //         Cantidad = group.Sum(detalle => detalle.Cantidad)
            //     })
            //     .ToList();
            
            List<ItemDetalleProductoModelo> itemsRecetas = items.Where(x => x.Receta != null).ToList();
            Ingredientes = new List<IngredienteDetalleRecetaModelo>();
            
            foreach (var item in itemsRecetas)
            {
                foreach (var detalle in item.Receta.DetalleRecetas)
                {
                    IngredienteDetalleRecetaModelo ingrediente = Ingredientes.FirstOrDefault(x => x.Ingrediente.IdIngrediente == detalle.Ingrediente.IdIngrediente);
                    if (ingrediente == null)
                    {
                        Ingredientes.Add(detalle);
                    }
                    else
                    {
                        ingrediente.Cantidad += item.Cantidad;
                    }
                }
            }
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


        public string GenerateHTML()
        {
            throw new System.NotImplementedException();
        }


    }
}