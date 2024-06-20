using System;
using System.Collections;
using Datos.EF;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using Datos.Migrations;
using static System.Net.Mime.MediaTypeNames;

public static class DetalleRecetasSeed
{
    public static List<DETALLERECETA> getDetalleRecetas(Datos.EF.Entities context)
    {
        List<INGREDIENTE> ingredientesContext = context.INGREDIENTES.ToList();
        List<RECETA> recetasContext = context.RECETAS.ToList();

        DETALLERECETA getRandomDetalleReceta(Guid id_receta)
        {
            INGREDIENTE ingrediente = ingredientesContext[Configuration.GlobalRandom.Next(0, ingredientesContext.Count)];
            return new DETALLERECETA
            {
                cantidad = Configuration.GlobalRandom.Next(1, 10),
                id_ingrediente = ingrediente.id_ingrediente,
                id_receta = id_receta
            };
        }

        List<DETALLERECETA> detalleRecetas = new List<DETALLERECETA>();

        foreach (RECETA receta in recetasContext )
        {
            List<Guid> ingredientesEnReceta = new List<Guid>();
            int cantidadDetalleRecetas = Configuration.GlobalRandom.Next(1, 2);
            for (int i = 0; i < cantidadDetalleRecetas; i++)
            {
                DETALLERECETA detalleReceta = getRandomDetalleReceta(receta.id_receta);
                if (ingredientesEnReceta.Contains(detalleReceta.id_ingrediente))
                {
                    continue;
                }
                ingredientesEnReceta.Add(detalleReceta.id_ingrediente);
                detalleRecetas.Add(detalleReceta);
            }
        }

        return detalleRecetas;
    }

}