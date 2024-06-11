using System.Data.Entity;

namespace Datos.EF
{
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }

        public virtual DbSet<CATEGORIA> CATEGORIAS { get; set; }
        public virtual DbSet<CONTACTO> CONTACTOS { get; set; }
        public virtual DbSet<DETALLEORDEN> DETALLE_ORDENES { get; set; }
        public virtual DbSet<DETALLERECETA> DETALLE_RECETAS { get; set; }
        public virtual DbSet<EVENTO> EVENTOS { get; set; }
        public virtual DbSet<IMAGENPRODUCTO> IMAGENES_PRODUCTOS { get; set; }
        public virtual DbSet<INGREDIENTE> INGREDIENTES { get; set; }
        public virtual DbSet<ORDEN> ORDENES { get; set; }
        public virtual DbSet<ORDENESTADO> ORDENES_ESTADOS { get; set; }
        public virtual DbSet<ORDENPAGOESTADO> ORDENES_PAGO_ESTADOS { get; set; }
        public virtual DbSet<PRODUCTO> PRODUCTOS { get; set; }
        public virtual DbSet<RECETA> RECETAS { get; set; }
        public virtual DbSet<SUMINISTRO> SUMINISTROS { get; set; }
        public virtual DbSet<TIPO_EVENTO> TIPOS_EVENTOS { get; set; }
        public virtual DbSet<UNIDAD_MEDIDA> UNIDADES_MEDIDA { get; set; }
        public virtual DbSet<DETALLEPRODUCTO> DETALLE_PRODUCTOS { get; set; }

        public virtual DbSet<PAGO> PAGOS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ORDEN>().ToTable("Ordenes");
            modelBuilder.Entity<CONTACTO>().ToTable("Contactos");
            modelBuilder.Entity<PRODUCTO>().ToTable("Productos");
            modelBuilder.Entity<RECETA>().ToTable("Recetas");
            modelBuilder.Entity<DETALLEORDEN>().ToTable("Detalle_Ordenes");
            modelBuilder.Entity<DETALLEPRODUCTO>().ToTable("Detalle_Productos");
            modelBuilder.Entity<DETALLERECETA>().ToTable("Detalle_Recetas");
            modelBuilder.Entity<ORDENESTADO>().ToTable("Ordenes_Estados");
            modelBuilder.Entity<ORDENPAGOESTADO>().ToTable("Ordenes_Pago_Estados");
            modelBuilder.Entity<CATEGORIA>().ToTable("Categorias");
            modelBuilder.Entity<IMAGENPRODUCTO>().ToTable("Imagenes_Productos");
            modelBuilder.Entity<INGREDIENTE>().ToTable("Ingredientes");
            modelBuilder.Entity<SUMINISTRO>().ToTable("Suministros");
            modelBuilder.Entity<TIPO_EVENTO>().ToTable("Tipos_Eventos");
            modelBuilder.Entity<UNIDAD_MEDIDA>().ToTable("Unidades_Medida");
            modelBuilder.Entity<EVENTO>().ToTable("Eventos");
            modelBuilder.Entity<PAGO>().ToTable("Pagos");


            modelBuilder.Entity<PAGO>()
                .HasRequired(e => e.ORDEN)
                .WithMany(e => e.PAGOS)
                .WillCascadeOnDelete(false);
        }
    }
}
