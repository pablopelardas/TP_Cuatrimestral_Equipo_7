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
    
    public partial class RECETAS
    {
        public RECETAS()
        {
            this.DETALLE_RECETAS = new HashSet<DETALLE_RECETAS>();
            this.DETALLE_PRODUCTOS = new HashSet<DETALLE_PRODUCTOS>();
        }
    
        public int id_receta { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int id_categoria { get; set; }
        public Nullable<decimal> precio_personalizado { get; set; }
    
        public virtual ICollection<DETALLE_RECETAS> DETALLE_RECETAS { get; set; }
        public virtual CATEGORIAS CATEGORIAS { get; set; }
        public virtual ICollection<DETALLE_PRODUCTOS> DETALLE_PRODUCTOS { get; set; }
    }
}
