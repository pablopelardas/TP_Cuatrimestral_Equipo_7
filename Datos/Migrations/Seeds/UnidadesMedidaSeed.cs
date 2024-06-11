using Datos.EF;
using System.Collections.Generic;

public static class UnidadesMedidaSeed
{
    public static List<UNIDAD_MEDIDA> getUnidadMedidas()
    {
        return new List<UNIDAD_MEDIDA>
        {
            new UNIDAD_MEDIDA
            {
                id_unidad = 1,
                nombre = "Unidad",
                abreviatura = "UN",
            },
            new UNIDAD_MEDIDA
            {
                id_unidad = 2,
                nombre = "Kilogramo",
                abreviatura = "KG"
            },
            new UNIDAD_MEDIDA
            {
                id_unidad = 3,
                nombre = "Litro",
                abreviatura = "LT"
            }
        };
    }

}