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
    
    public partial class UNIDADES_MEDIDA
    {
        public UNIDADES_MEDIDA()
        {
            this.INGREDIENTES = new HashSet<INGREDIENTES>();
        }
    
        public int id_unidad { get; set; }
        public string nombre { get; set; }
        public string abreviatura { get; set; }
    
        public virtual ICollection<INGREDIENTES> INGREDIENTES { get; set; }
    }
}
