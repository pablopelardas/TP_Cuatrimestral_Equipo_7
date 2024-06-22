using System;
using Datos.EF;
using System.Collections.Generic;
using System.Linq;

public static class IngredientesSeed
{
    public static Random random = new Random();
    public static List<INGREDIENTE> getIngredientes(Datos.EF.Entities context)
    {

        List<UNIDAD_MEDIDA> unidades = context.UNIDADES_MEDIDA.ToList();

        return new List<INGREDIENTE>
        {
            new INGREDIENTE
            {
                nombre = "Harina",
                id_unidad = unidades.Find(u => u.abreviatura == "GR").id_unidad,
                cantidad = 1000,
                costo = 590,
            },
            new INGREDIENTE
            {
                nombre = "Almidon de maiz",
                id_unidad = unidades.Find(u => u.abreviatura == "GR").id_unidad,
                cantidad = 550,
                costo = 1900,
            },
            new INGREDIENTE
            {
                nombre = "Bicarbonato de sodio",
                id_unidad = unidades.Find(u => u.abreviatura == "GR").id_unidad,
                cantidad = 25,
                costo = 440.89M,
            },
            new INGREDIENTE
            {
                nombre = "Manteca",
                id_unidad = unidades.Find(u => u.abreviatura == "GR").id_unidad,
                cantidad = 2500,
                costo = 25200,
            },
            new INGREDIENTE
            {
                nombre = "Azucar impalpable",
                id_unidad = unidades.Find(u => u.abreviatura == "GR").id_unidad,
                cantidad = 500,
                costo = 1600.70M,
            },
            new INGREDIENTE
            {
                nombre = "Yemas",
                id_unidad = unidades.Find(u => u.abreviatura == "UN").id_unidad,
                cantidad = 1,
                costo = 56,
            },
            new INGREDIENTE
            {
                nombre = "Esencia de vainilla",
                id_unidad = unidades.Find(u => u.abreviatura == "CC").id_unidad,
                cantidad = 2000,
                costo = 3699,
            },
            new INGREDIENTE
            {
                nombre = "Rayadura de limon",
                id_unidad = unidades.Find(u => u.abreviatura == "UN").id_unidad,
                cantidad = 1,
                costo = 0,
            },
            new INGREDIENTE
            {
                nombre = "Dulce de leche",
                id_unidad = unidades.Find(u => u.abreviatura == "GR").id_unidad,
                cantidad = 3000,
                costo = 10400,
            },
            new INGREDIENTE
            {
                nombre = "Coco rallado",
                id_unidad = unidades.Find(u => u.abreviatura == "GR").id_unidad,
                cantidad = 250,
                costo = 1600,
            },
            new INGREDIENTE()
            {
                nombre = "Chocolinas",
                id_unidad = unidades.Find(u => u.abreviatura == "GR").id_unidad,
                cantidad = 500,
                costo = 500,
            },
            new INGREDIENTE()
            {
                nombre = "Casancrem Clasico",
                id_unidad = unidades.Find(u => u.abreviatura == "GR").id_unidad,
                cantidad = 480,
                costo = 2738,
            },
            new INGREDIENTE()
            {
                nombre = "Dulce de leche Repostero",
                id_unidad = unidades.Find(u => u.abreviatura == "GR").id_unidad,
                cantidad = 400,
                costo = 1970.85M,
            },
            new INGREDIENTE()
            {
                nombre = "Leche Clasica",
                id_unidad = unidades.Find(u => u.abreviatura == "ML").id_unidad,
                cantidad = 1000,
                costo = 1335,
            },
        };
    }
}