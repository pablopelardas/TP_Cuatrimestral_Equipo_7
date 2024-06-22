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
        
        List<DETALLERECETA> detalleRecetas = new List<DETALLERECETA>();
        
        Guid idAlfajoresDeMaicena = recetasContext.Find(r => r.nombre == "Alfajores de maicena").id_receta;
        
        // RECETA ALFAJORES DE MAICENA
        detalleRecetas.AddRange(new List<DETALLERECETA>
        {
            new DETALLERECETA
            {
                cantidad = 200,
                id_receta = idAlfajoresDeMaicena,
                id_ingrediente = ingredientesContext.Find(i => i.nombre == "Harina").id_ingrediente
            },
            new DETALLERECETA
            {
                cantidad = 300,
                id_receta = idAlfajoresDeMaicena,
                id_ingrediente = ingredientesContext.Find(i => i.nombre == "Almidon de maiz").id_ingrediente
            },
            new DETALLERECETA
            {
                cantidad = 10,
                id_receta = idAlfajoresDeMaicena,
                id_ingrediente = ingredientesContext.Find(i => i.nombre == "Bicarbonato de sodio").id_ingrediente
            },
            new DETALLERECETA
            {
                cantidad = 200,
                id_receta = idAlfajoresDeMaicena,
                id_ingrediente = ingredientesContext.Find(i => i.nombre == "Manteca").id_ingrediente
            },
            new DETALLERECETA
            {
                cantidad = 150,
                id_receta = idAlfajoresDeMaicena,
                id_ingrediente = ingredientesContext.Find(i => i.nombre == "Azucar impalpable").id_ingrediente
            },
            new DETALLERECETA
            {
                cantidad = 60,
                id_receta = idAlfajoresDeMaicena,
                id_ingrediente = ingredientesContext.Find(i => i.nombre == "Yemas").id_ingrediente
            },
            new DETALLERECETA
            {
                cantidad = 16,
                id_receta = idAlfajoresDeMaicena,
                id_ingrediente = ingredientesContext.Find(i => i.nombre == "Esencia de vainilla").id_ingrediente
            },
            new DETALLERECETA
            {
                cantidad = 1,
                id_receta = idAlfajoresDeMaicena,
                id_ingrediente = ingredientesContext.Find(i => i.nombre == "Rayadura de limon").id_ingrediente
            },
            new DETALLERECETA
            {
                cantidad = 550,
                id_receta = idAlfajoresDeMaicena,
                id_ingrediente = ingredientesContext.Find(i => i.nombre == "Dulce de leche").id_ingrediente
            },
            new DETALLERECETA
            {
                cantidad = 100,
                id_receta = idAlfajoresDeMaicena,
                id_ingrediente = ingredientesContext.Find(i => i.nombre == "Coco rallado").id_ingrediente
            }
        });
        
        Guid idChocotorta = recetasContext.Find(r => r.nombre == "Chocotorta").id_receta;
        
        // RECETA CHOCOTORTA
        detalleRecetas.AddRange(new List<DETALLERECETA>
        {
            new DETALLERECETA
            {
                cantidad = 250,
                id_receta = idChocotorta,
                id_ingrediente = ingredientesContext.Find(i => i.nombre == "Chocolinas").id_ingrediente
            },
            new DETALLERECETA
            {
                cantidad = 400,
                id_receta = idChocotorta,
                id_ingrediente = ingredientesContext.Find(i => i.nombre == "Casancrem Clasico").id_ingrediente
            },
            new DETALLERECETA
            {
                cantidad = 400,
                id_receta = idChocotorta,
                id_ingrediente = ingredientesContext.Find(i => i.nombre == "Dulce de leche Repostero").id_ingrediente
            },
            new DETALLERECETA
            {
                cantidad = 200,
                id_receta = idChocotorta,
                id_ingrediente = ingredientesContext.Find(i => i.nombre == "Leche Clasica").id_ingrediente
            },
        });
        
        


        return detalleRecetas;
    }

}