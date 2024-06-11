using System;
using Datos.EF;
using System.Collections.Generic;
using System.Linq;

public static class IngredientesSeed
{
    private static Random random = new Random();
    public static List<INGREDIENTE> getIngredientes(Datos.EF.Entities context)
    {

        List<UNIDAD_MEDIDA> unidades = context.UNIDADES_MEDIDA.ToList();

        Guid randomUnitId()
        {
            int index = random.Next(0, unidades.Count);
            return unidades[index].id_unidad;
        }

        return new List<INGREDIENTE>
        {
            new INGREDIENTE
            {
                nombre = "Harina",
                // pick random unit of measure with Random class
                id_unidad = randomUnitId(),
                cantidad = 1
            },
            new INGREDIENTE
            {
                nombre = "Azucar",
                id_unidad = randomUnitId(),
                cantidad = 1
            },
            new INGREDIENTE
            {
                nombre = "Huevos",
                id_unidad = randomUnitId(),
                cantidad = 2
            }
        };
    }
}