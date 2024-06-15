using System.Collections.Generic;
using System.Linq;

namespace Dominio.Modelos
{
    public class ListaCompra
    {
        public List<IngredienteDetalleRecetaModelo> Ingredientes { get; set; }
        public List<ItemDetalleProductoModelo> Suministros { get; set; }
        
        public ListaCompra(List<ItemDetalleProductoModelo> items)
        {
            Suministros = items.Where(x => x.Suministro != null).ToList();
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
            Ingredientes = new List<IngredienteDetalleRecetaModelo>();
            Suministros = new List<ItemDetalleProductoModelo>();
            
            foreach (var lista in listas)
            {
                // Sumar ingredientes
                foreach (var ingrediente in lista.Ingredientes)
                {
                    IngredienteDetalleRecetaModelo ingredienteActual = Ingredientes.FirstOrDefault(x => x.Ingrediente.IdIngrediente == ingrediente.Ingrediente.IdIngrediente);
                    if (ingredienteActual == null)
                    {
                        Ingredientes.Add(ingrediente);
                    }
                    else
                    {
                        ingredienteActual.Cantidad += ingrediente.Cantidad;
                    }
                }
                
                // Sumar suministros
                foreach (var suministro in lista.Suministros)
                {
                    ItemDetalleProductoModelo suministroActual = Suministros.FirstOrDefault(x => x.Suministro.IdSuministro == suministro.Suministro.IdSuministro);
                    if (suministroActual == null)
                    {
                        Suministros.Add(suministro);
                    }
                    else
                    {
                        suministroActual.Cantidad += suministro.Cantidad;
                    }
                }
            }
        }
        
        
        
    }
}