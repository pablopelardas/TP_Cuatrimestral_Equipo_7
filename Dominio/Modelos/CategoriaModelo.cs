using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    [Serializable()]
    public class CategoriaModelo
    {
        
        private string image_path = "/Assets/Categorias";
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string Tipo { get; set; }
        
        public string Imagen { get; set; }
        public string ImagenPath { get { return image_path + "/" + Imagen; } }

        public CategoriaModelo()
        {
        }
    }
}
