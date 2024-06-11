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



            modelBuilder.Entity<CATEGORIA>()
                .Property(e => e.tipo)
                .IsUnicode(false);

            modelBuilder.Entity<CATEGORIA>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<CATEGORIA>()
                .HasMany(e => e.PRODUCTOS)
                .WithRequired(e => e.CATEGORIA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CATEGORIA>()
                .HasMany(e => e.RECETAS)
                .WithRequired(e => e.CATEGORIA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CATEGORIA>()
                .HasMany(e => e.SUMINISTROS)
                .WithRequired(e => e.CATEGORIA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CONTACTO>()
                .Property(e => e.nombre_apellido)
                .IsUnicode(false);

            modelBuilder.Entity<CONTACTO>()
                .Property(e => e.tipo)
                .IsUnicode(false);

            modelBuilder.Entity<CONTACTO>()
                .Property(e => e.correo)
                .IsUnicode(false);

            modelBuilder.Entity<CONTACTO>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<CONTACTO>()
                .Property(e => e.fuente)
                .IsUnicode(false);

            modelBuilder.Entity<CONTACTO>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<CONTACTO>()
                .Property(e => e.producto_que_provee)
                .IsUnicode(false);

            modelBuilder.Entity<CONTACTO>()
                .Property(e => e.informacion_personal)
                .IsUnicode(false);

            modelBuilder.Entity<CONTACTO>()
                .HasMany(e => e.EVENTOS)
                .WithRequired(e => e.CLIENTE)
                .HasForeignKey(e => e.id_cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CONTACTO>()
                .HasMany(e => e.ORDENES)
                .WithRequired(e => e.CLIENTE)
                .HasForeignKey(e => e.id_cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DETALLEORDEN>()
                .Property(e => e.producto_costo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DETALLEORDEN>()
                .Property(e => e.producto_precio)
                .HasPrecision(19, 4);

            modelBuilder.Entity<IMAGENPRODUCTO>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<INGREDIENTE>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<INGREDIENTE>()
                .Property(e => e.costo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<INGREDIENTE>()
                .Property(e => e.proveedor)
                .IsUnicode(false);

            modelBuilder.Entity<INGREDIENTE>()
                .HasMany(e => e.DETALLE_RECETAS)
                .WithRequired(e => e.INGREDIENTE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ORDEN>()
                .Property(e => e.tipo_entrega)
                .IsUnicode(false);

            modelBuilder.Entity<ORDEN>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<ORDEN>()
                .Property(e => e.descuento_porcentaje)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ORDEN>()
                .Property(e => e.costo_envio)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ORDEN>()
                .Property(e => e.direccion_entrega)
                .IsUnicode(false);

            modelBuilder.Entity<ORDEN>()
                .HasMany(e => e.DETALLE_ORDENES)
                .WithRequired(e => e.ORDEN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ORDENESTADO>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<ORDENPAGOESTADO>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCTO>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCTO>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCTO>()
                .Property(e => e.horas_trabajo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<PRODUCTO>()
                .Property(e => e.tipo_precio)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCTO>()
                .Property(e => e.valor_precio)
                .HasPrecision(18, 0);

            modelBuilder.Entity<PRODUCTO>()
                .HasMany(e => e.DETALLE_ORDENES)
                .WithRequired(e => e.PRODUCTO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRODUCTO>()
                .HasMany(e => e.IMAGENES)
                .WithRequired(e => e.PRODUCTO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRODUCTO>()
                .HasMany(e => e.DETALLE_PRODUCTOS)
                .WithRequired(e => e.PRODUCTO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RECETA>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<RECETA>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<RECETA>()
                .Property(e => e.precio_personalizado)
                .HasPrecision(19, 4);

            modelBuilder.Entity<RECETA>()
                .HasMany(e => e.DETALLE_RECETAS)
                .WithRequired(e => e.RECETA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SUMINISTRO>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<SUMINISTRO>()
                .Property(e => e.proveedor)
                .IsUnicode(false);

            modelBuilder.Entity<SUMINISTRO>()
                .Property(e => e.costo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<TIPO_EVENTO>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<TIPO_EVENTO>()
                .HasMany(e => e.EVENTOS)
                .WithRequired(e => e.TIPO_EVENTO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UNIDAD_MEDIDA>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<UNIDAD_MEDIDA>()
                .Property(e => e.abreviatura)
                .IsUnicode(false);

            modelBuilder.Entity<UNIDAD_MEDIDA>()
                .HasMany(e => e.INGREDIENTES)
                .WithRequired(e => e.UNIDAD_MEDIDA)
                .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<PAGO>()
                .Property(e => e.monto)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PAGO>()
                .Property(e => e.tipo_pago)
                .IsUnicode(false);

            modelBuilder.Entity<PAGO>()
                .HasRequired(e => e.CLIENTE)
                .WithMany()
                .HasForeignKey(e => e.id_cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PAGO>()
                .HasRequired(e => e.ORDEN)
                .WithMany()
                .HasForeignKey(e => e.id_orden)
                .WillCascadeOnDelete(false);

        }
    }
}
