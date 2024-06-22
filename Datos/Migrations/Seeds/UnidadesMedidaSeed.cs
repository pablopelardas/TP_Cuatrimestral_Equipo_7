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
                nombre = "Unidad",
                abreviatura = "UN",
            },
            new UNIDAD_MEDIDA
            {
                nombre = "Kilogramo",
                abreviatura = "KG"
            },
            new UNIDAD_MEDIDA()
            {
                nombre = "Gramo",
                abreviatura = "GR"
            },
            new UNIDAD_MEDIDA
            {
                nombre = "Litro",
                abreviatura = "LT"
            },
            new UNIDAD_MEDIDA
            {
                nombre = "Mililitro",
                abreviatura = "ML"
            },
            new UNIDAD_MEDIDA
            {
                nombre = "Centimetros cubicos",
                abreviatura = "CC"
            },
            
        };
    }

}