namespace Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        id_categoria = c.Guid(nullable: false, identity: true),
                        tipo = c.String(nullable: false, maxLength: 50),
                        nombre = c.String(nullable: false, maxLength: 50),
                        imagen = c.String(),
                    })
                .PrimaryKey(t => t.id_categoria);
            
            CreateTable(
                "dbo.Contactos",
                c => new
                    {
                        id_contacto = c.Guid(nullable: false, identity: true),
                        nombre_apellido = c.String(nullable: false, maxLength: 100),
                        tipo = c.String(nullable: false, maxLength: 20),
                        correo = c.String(nullable: false, maxLength: 100),
                        telefono = c.String(nullable: false, maxLength: 20),
                        fuente = c.String(maxLength: 100),
                        producto_que_provee = c.String(maxLength: 100),
                        desea_recibir_correos = c.Boolean(nullable: false),
                        desea_recibir_whatsapp = c.Boolean(nullable: false),
                        informacion_personal = c.String(),
                    })
                .PrimaryKey(t => t.id_contacto);
            
            CreateTable(
                "dbo.Direcciones",
                c => new
                    {
                        id_direccion = c.Guid(nullable: false, identity: true),
                        id_cliente = c.Guid(nullable: false),
                        calle_numero = c.String(),
                        localidad = c.String(),
                        codigo_postal = c.String(),
                        piso = c.String(),
                        departamento = c.String(),
                        provincia = c.String(),
                    })
                .PrimaryKey(t => t.id_direccion)
                .ForeignKey("dbo.Contactos", t => t.id_cliente, cascadeDelete: true)
                .Index(t => t.id_cliente);
            
            CreateTable(
                "dbo.Eventos",
                c => new
                    {
                        id_evento = c.Guid(nullable: false, identity: true),
                        fecha = c.DateTime(nullable: false, storeType: "date"),
                        id_cliente = c.Guid(nullable: false),
                        id_tipo_evento = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id_evento)
                .ForeignKey("dbo.Contactos", t => t.id_cliente, cascadeDelete: true)
                .ForeignKey("dbo.Tipos_Eventos", t => t.id_tipo_evento, cascadeDelete: true)
                .Index(t => t.id_cliente)
                .Index(t => t.id_tipo_evento);
            
            CreateTable(
                "dbo.Ordenes",
                c => new
                    {
                        id_orden = c.Guid(nullable: false, identity: true),
                        id_cliente = c.Guid(nullable: false),
                        tipo_entrega = c.String(nullable: false, maxLength: 50),
                        descripcion = c.String(),
                        descuento_porcentaje = c.Decimal(precision: 18, scale: 2),
                        costo_envio = c.Decimal(precision: 18, scale: 2),
                        hora_entrega = c.Time(nullable: false, precision: 7),
                        id_orden_estado = c.Int(),
                        id_orden_pago_estado = c.Int(),
                        id_evento = c.Guid(),
                    })
                .PrimaryKey(t => t.id_orden)
                .ForeignKey("dbo.Contactos", t => t.id_cliente, cascadeDelete: true)
                .ForeignKey("dbo.Ordenes_Estados", t => t.id_orden_estado)
                .ForeignKey("dbo.Ordenes_Pago_Estados", t => t.id_orden_pago_estado)
                .ForeignKey("dbo.Eventos", t => t.id_evento)
                .Index(t => t.id_cliente)
                .Index(t => t.id_orden_estado)
                .Index(t => t.id_orden_pago_estado)
                .Index(t => t.id_evento);
            
            CreateTable(
                "dbo.Detalle_Ordenes",
                c => new
                    {
                        id_orden = c.Guid(nullable: false),
                        id_producto = c.Guid(nullable: false),
                        cantidad = c.Int(nullable: false),
                        producto_porciones = c.Int(nullable: false),
                        producto_costo = c.Decimal(nullable: false, storeType: "money"),
                        producto_precio = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => new { t.id_orden, t.id_producto })
                .ForeignKey("dbo.Ordenes", t => t.id_orden, cascadeDelete: true)
                .ForeignKey("dbo.Productos", t => t.id_producto, cascadeDelete: true)
                .Index(t => t.id_orden)
                .Index(t => t.id_producto);
            
            CreateTable(
                "dbo.Productos",
                c => new
                    {
                        id_producto = c.Guid(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 50),
                        descripcion = c.String(nullable: false, maxLength: 200),
                        porciones = c.Int(nullable: false),
                        horas_trabajo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        tipo_precio = c.String(nullable: false, maxLength: 50),
                        valor_precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        id_categoria = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id_producto)
                .ForeignKey("dbo.Categorias", t => t.id_categoria, cascadeDelete: true)
                .Index(t => t.id_categoria);
            
            CreateTable(
                "dbo.Detalle_Productos",
                c => new
                    {
                        id_detalle_producto = c.Guid(nullable: false, identity: true),
                        id_producto = c.Guid(nullable: false),
                        id_suministro = c.Guid(),
                        id_receta = c.Guid(),
                        cantidad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id_detalle_producto)
                .ForeignKey("dbo.Productos", t => t.id_producto, cascadeDelete: true)
                .ForeignKey("dbo.Recetas", t => t.id_receta)
                .ForeignKey("dbo.Suministros", t => t.id_suministro)
                .Index(t => t.id_producto)
                .Index(t => t.id_suministro)
                .Index(t => t.id_receta);
            
            CreateTable(
                "dbo.Recetas",
                c => new
                    {
                        id_receta = c.Guid(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 50),
                        descripcion = c.String(nullable: false, maxLength: 200),
                        id_categoria = c.Guid(nullable: false),
                        precio_personalizado = c.Decimal(storeType: "money"),
                    })
                .PrimaryKey(t => t.id_receta)
                .ForeignKey("dbo.Categorias", t => t.id_categoria, cascadeDelete: true)
                .Index(t => t.id_categoria);
            
            CreateTable(
                "dbo.Detalle_Recetas",
                c => new
                    {
                        id_receta = c.Guid(nullable: false),
                        id_ingrediente = c.Guid(nullable: false),
                        cantidad = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.id_receta, t.id_ingrediente })
                .ForeignKey("dbo.Ingredientes", t => t.id_ingrediente, cascadeDelete: true)
                .ForeignKey("dbo.Recetas", t => t.id_receta, cascadeDelete: true)
                .Index(t => t.id_receta)
                .Index(t => t.id_ingrediente);
            
            CreateTable(
                "dbo.Ingredientes",
                c => new
                    {
                        id_ingrediente = c.Guid(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 50),
                        cantidad = c.Decimal(nullable: false, precision: 18, scale: 2),
                        id_unidad = c.Guid(nullable: false),
                        costo = c.Decimal(nullable: false, storeType: "money"),
                        proveedor = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.id_ingrediente)
                .ForeignKey("dbo.Unidades_Medida", t => t.id_unidad, cascadeDelete: true)
                .Index(t => t.id_unidad);
            
            CreateTable(
                "dbo.Unidades_Medida",
                c => new
                    {
                        id_unidad = c.Guid(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 50),
                        abreviatura = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.id_unidad);
            
            CreateTable(
                "dbo.Suministros",
                c => new
                    {
                        id_suministro = c.Guid(nullable: false, identity: true),
                        id_categoria = c.Guid(nullable: false),
                        nombre = c.String(nullable: false, maxLength: 50),
                        proveedor = c.String(maxLength: 50),
                        cantidad = c.Decimal(nullable: false, precision: 18, scale: 2),
                        costo = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.id_suministro)
                .ForeignKey("dbo.Categorias", t => t.id_categoria, cascadeDelete: true)
                .Index(t => t.id_categoria);
            
            CreateTable(
                "dbo.Imagenes_Productos",
                c => new
                    {
                        id_imagen = c.Int(nullable: false, identity: true),
                        id_producto = c.Guid(nullable: false),
                        url = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.id_imagen)
                .ForeignKey("dbo.Productos", t => t.id_producto, cascadeDelete: true)
                .Index(t => t.id_producto);
            
            CreateTable(
                "dbo.Ordenes_Estados",
                c => new
                    {
                        id_orden_estado = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.id_orden_estado);
            
            CreateTable(
                "dbo.Ordenes_Pago_Estados",
                c => new
                    {
                        id_orden_pago_estado = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.id_orden_pago_estado);
            
            CreateTable(
                "dbo.Ordenes_Direcciones",
                c => new
                    {
                        id_orden_direccion = c.Guid(nullable: false),
                        calle_numero = c.String(),
                        localidad = c.String(),
                        codigo_postal = c.String(),
                        piso = c.String(),
                        departamento = c.String(),
                        provincia = c.String(),
                    })
                .PrimaryKey(t => t.id_orden_direccion)
                .ForeignKey("dbo.Ordenes", t => t.id_orden_direccion)
                .Index(t => t.id_orden_direccion);
            
            CreateTable(
                "dbo.Pagos",
                c => new
                    {
                        id_pago = c.Guid(nullable: false, identity: true),
                        id_cliente = c.Guid(nullable: false),
                        id_orden = c.Guid(nullable: false),
                        fecha = c.DateTime(nullable: false),
                        monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        tipo_pago = c.String(),
                    })
                .PrimaryKey(t => t.id_pago)
                .ForeignKey("dbo.Contactos", t => t.id_cliente, cascadeDelete: true)
                .ForeignKey("dbo.Ordenes", t => t.id_orden)
                .Index(t => t.id_cliente)
                .Index(t => t.id_orden);
            
            CreateTable(
                "dbo.Tipos_Eventos",
                c => new
                    {
                        id_tipo_evento = c.Guid(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.id_tipo_evento);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Eventos", "id_tipo_evento", "dbo.Tipos_Eventos");
            DropForeignKey("dbo.Pagos", "id_orden", "dbo.Ordenes");
            DropForeignKey("dbo.Pagos", "id_cliente", "dbo.Contactos");
            DropForeignKey("dbo.Ordenes_Direcciones", "id_orden_direccion", "dbo.Ordenes");
            DropForeignKey("dbo.Ordenes", "id_evento", "dbo.Eventos");
            DropForeignKey("dbo.Ordenes", "id_orden_pago_estado", "dbo.Ordenes_Pago_Estados");
            DropForeignKey("dbo.Ordenes", "id_orden_estado", "dbo.Ordenes_Estados");
            DropForeignKey("dbo.Imagenes_Productos", "id_producto", "dbo.Productos");
            DropForeignKey("dbo.Detalle_Productos", "id_suministro", "dbo.Suministros");
            DropForeignKey("dbo.Suministros", "id_categoria", "dbo.Categorias");
            DropForeignKey("dbo.Detalle_Recetas", "id_receta", "dbo.Recetas");
            DropForeignKey("dbo.Ingredientes", "id_unidad", "dbo.Unidades_Medida");
            DropForeignKey("dbo.Detalle_Recetas", "id_ingrediente", "dbo.Ingredientes");
            DropForeignKey("dbo.Detalle_Productos", "id_receta", "dbo.Recetas");
            DropForeignKey("dbo.Recetas", "id_categoria", "dbo.Categorias");
            DropForeignKey("dbo.Detalle_Productos", "id_producto", "dbo.Productos");
            DropForeignKey("dbo.Detalle_Ordenes", "id_producto", "dbo.Productos");
            DropForeignKey("dbo.Productos", "id_categoria", "dbo.Categorias");
            DropForeignKey("dbo.Detalle_Ordenes", "id_orden", "dbo.Ordenes");
            DropForeignKey("dbo.Ordenes", "id_cliente", "dbo.Contactos");
            DropForeignKey("dbo.Eventos", "id_cliente", "dbo.Contactos");
            DropForeignKey("dbo.Direcciones", "id_cliente", "dbo.Contactos");
            DropIndex("dbo.Pagos", new[] { "id_orden" });
            DropIndex("dbo.Pagos", new[] { "id_cliente" });
            DropIndex("dbo.Ordenes_Direcciones", new[] { "id_orden_direccion" });
            DropIndex("dbo.Imagenes_Productos", new[] { "id_producto" });
            DropIndex("dbo.Suministros", new[] { "id_categoria" });
            DropIndex("dbo.Ingredientes", new[] { "id_unidad" });
            DropIndex("dbo.Detalle_Recetas", new[] { "id_ingrediente" });
            DropIndex("dbo.Detalle_Recetas", new[] { "id_receta" });
            DropIndex("dbo.Recetas", new[] { "id_categoria" });
            DropIndex("dbo.Detalle_Productos", new[] { "id_receta" });
            DropIndex("dbo.Detalle_Productos", new[] { "id_suministro" });
            DropIndex("dbo.Detalle_Productos", new[] { "id_producto" });
            DropIndex("dbo.Productos", new[] { "id_categoria" });
            DropIndex("dbo.Detalle_Ordenes", new[] { "id_producto" });
            DropIndex("dbo.Detalle_Ordenes", new[] { "id_orden" });
            DropIndex("dbo.Ordenes", new[] { "id_evento" });
            DropIndex("dbo.Ordenes", new[] { "id_orden_pago_estado" });
            DropIndex("dbo.Ordenes", new[] { "id_orden_estado" });
            DropIndex("dbo.Ordenes", new[] { "id_cliente" });
            DropIndex("dbo.Eventos", new[] { "id_tipo_evento" });
            DropIndex("dbo.Eventos", new[] { "id_cliente" });
            DropIndex("dbo.Direcciones", new[] { "id_cliente" });
            DropTable("dbo.Tipos_Eventos");
            DropTable("dbo.Pagos");
            DropTable("dbo.Ordenes_Direcciones");
            DropTable("dbo.Ordenes_Pago_Estados");
            DropTable("dbo.Ordenes_Estados");
            DropTable("dbo.Imagenes_Productos");
            DropTable("dbo.Suministros");
            DropTable("dbo.Unidades_Medida");
            DropTable("dbo.Ingredientes");
            DropTable("dbo.Detalle_Recetas");
            DropTable("dbo.Recetas");
            DropTable("dbo.Detalle_Productos");
            DropTable("dbo.Productos");
            DropTable("dbo.Detalle_Ordenes");
            DropTable("dbo.Ordenes");
            DropTable("dbo.Eventos");
            DropTable("dbo.Direcciones");
            DropTable("dbo.Contactos");
            DropTable("dbo.Categorias");
        }
    }
}
