//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Datos
{
    using System;
    using System.Collections.Generic;
    
    public partial class DETALLE_RECETAS
    {
        public int id_receta { get; set; }
        public int id_ingrediente { get; set; }
        public double cantidad { get; set; }
    
        public virtual INGREDIENTES INGREDIENTES { get; set; }
        public virtual RECETAS RECETAS { get; set; }
    }
}