using Datos.EF;
using System.Collections.Generic;

public static class IngredientesSeed
{
    public static List<INGREDIENTE> getIngredientes()
    {
        return new List<INGREDIENTE>
        {
            new INGREDIENTE
            {
                id_ingrediente = 1,
                nombre = "Harina",
                id_unidad = 2,
                cantidad = 1
            },
            new INGREDIENTE
            {
                id_ingrediente = 2,
                nombre = "Azucar",
                id_unidad = 2,
                cantidad = 1
            },
            new INGREDIENTE
            {
                id_ingrediente = 3,
                nombre = "Huevos",
                id_unidad = 1,
                cantidad = 2
            },
            new INGREDIENTE
            {
                id_ingrediente = 4,
                nombre = "Leche",
                id_unidad = 3,
                cantidad = 1
            },
            new INGREDIENTE
            {
                id_ingrediente = 5,
                nombre = "Manteca",
                id_unidad = 2,
                cantidad = 1
            },
            new INGREDIENTE
            {
                id_ingrediente = 6,
                nombre = "Polvo de hornear",
                id_unidad = 2,
                cantidad = 1
            },
            new INGREDIENTE
            {
                id_ingrediente = 7,
                nombre = "Esencia de vainilla",
                id_unidad = 1,
                cantidad = 1
            },
            new INGREDIENTE
            {
                id_ingrediente = 8,
                nombre = "Cacao",
                id_unidad = 2,
                cantidad = 1
            },
            new INGREDIENTE
            {
                id_ingrediente = 9,
                nombre = "Crema",
                id_unidad = 3,
                cantidad = 1
            },
            new INGREDIENTE
            {
                id_ingrediente = 10,
                nombre = "Frutillas",
                id_unidad = 2,
                cantidad = 1
            },
            new INGREDIENTE
            {
                id_ingrediente = 11,
                nombre = "Azucar impalpable",
                id_unidad = 2,
                cantidad = 1
            },
            new INGREDIENTE
            {
                id_ingrediente = 12,
                nombre = "Chocolate",
                id_unidad = 2,
                cantidad = 1
            },
            new INGREDIENTE
            {
                id_ingrediente = 13,
                nombre = "Dulce de leche",
                id_unidad = 2,
                cantidad = 1
            }
        };
    }
}