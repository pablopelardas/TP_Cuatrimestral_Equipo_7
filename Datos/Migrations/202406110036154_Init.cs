namespace Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        id_categoria = c.Int(nullable: false, identity: true),
                        tipo = c.String(nullable: false, maxLength: 50),
                        nombre = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.id_categoria);
            
            CreateTable(
                "dbo.Productos",
                c => new
                    {
                        id_producto = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 50),
                        descripcion = c.String(nullable: false, maxLength: 200),
                        porciones = c.Int(nullable: false),
                        horas_trabajo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        tipo_precio = c.String(nullable: false, maxLength: 50),
                        valor_precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        id_categoria = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id_producto)
                .ForeignKey("dbo.Categorias", t => t.id_categoria, cascadeDelete: true)
                .Index(t => t.id_categoria);
            
            CreateTable(
                "dbo.Detalle_Ordenes",
                c => new
                    {
                        id_orden = c.Int(nullable: false),
                        id_producto = c.Int(nullable: false),
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
                "dbo.Ordenes",
                c => new
                    {
                        id_orden = c.Int(nullable: false, identity: true),
                        id_cliente = c.Int(nullable: false),
                        tipo_entrega = c.String(nullable: false, maxLength: 50),
                        descripcion = c.String(),
                        descuento_porcentaje = c.Decimal(precision: 18, scale: 2),
                        costo_envio = c.Decimal(precision: 18, scale: 2),
                        direccion_entrega = c.String(maxLength: 200),
                        hora_entrega = c.Time(nullable: false, precision: 7),
                        id_orden_estado = c.Int(),
                        id_orden_pago_estado = c.Int(),
                        id_evento = c.Int(),
                        CLIENTE_id_contacto = c.Int(),
                    })
                .PrimaryKey(t => t.id_orden)
                .ForeignKey("dbo.Eventos", t => t.id_evento)
                .ForeignKey("dbo.Contactos", t => t.CLIENTE_id_contacto)
                .ForeignKey("dbo.Ordenes_Estados", t => t.id_orden_estado)
                .ForeignKey("dbo.Ordenes_Pago_Estados", t => t.id_orden_pago_estado)
                .Index(t => t.id_orden_estado)
                .Index(t => t.id_orden_pago_estado)
                .Index(t => t.id_evento)
                .Index(t => t.CLIENTE_id_contacto);
            
            CreateTable(
                "dbo.Contactos",
                c => new
                    {
                        id_contacto = c.Int(nullable: false, identity: true),
                        nombre_apellido = c.String(nullable: false, maxLength: 100),
                        tipo = c.String(nullable: false, maxLength: 20),
                        correo = c.String(nullable: false, maxLength: 100),
                        telefono = c.String(nullable: false, maxLength: 20),
                        fuente = c.String(maxLength: 100),
                        direccion = c.String(nullable: false, maxLength: 200),
                        producto_que_provee = c.String(maxLength: 100),
                        desea_recibir_correos = c.Boolean(nullable: false),
                        desea_recibir_whatsapp = c.Boolean(nullable: false),
                        informacion_personal = c.String(),
                    })
                .PrimaryKey(t => t.id_contacto);
            
            CreateTable(
                "dbo.Eventos",
                c => new
                    {
                        id_evento = c.Int(nullable: false, identity: true),
                        fecha = c.DateTime(nullable: false, storeType: "date"),
                        id_cliente = c.Int(nullable: false),
                        id_tipo_evento = c.Int(nullable: false),
                        CLIENTE_id_contacto = c.Int(),
                    })
                .PrimaryKey(t => t.id_evento)
                .ForeignKey("dbo.Contactos", t => t.CLIENTE_id_contacto)
                .ForeignKey("dbo.Tipos_Eventos", t => t.id_tipo_evento, cascadeDelete: true)
                .Index(t => t.id_tipo_evento)
                .Index(t => t.CLIENTE_id_contacto);
            
            CreateTable(
                "dbo.Tipos_Eventos",
                c => new
                    {
                        id_tipo_evento = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.id_tipo_evento);
            
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
                "dbo.Detalle_Productos",
                c => new
                    {
                        id_detalle_producto = c.Int(nullable: false, identity: true),
                        id_producto = c.Int(nullable: false),
                        id_suministro = c.Int(),
                        id_receta = c.Int(),
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
                        id_receta = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 50),
                        descripcion = c.String(nullable: false, maxLength: 200),
                        id_categoria = c.Int(nullable: false),
                        precio_personalizado = c.Decimal(storeType: "money"),
                    })
                .PrimaryKey(t => t.id_receta)
                .ForeignKey("dbo.Categorias", t => t.id_categoria, cascadeDelete: true)
                .Index(t => t.id_categoria);
            
            CreateTable(
                "dbo.Detalle_Recetas",
                c => new
                    {
                        id_receta = c.Int(nullable: false),
                        id_ingrediente = c.Int(nullable: false),
                        cantidad = c.Double(nullable: false),
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
                        id_ingrediente = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 50),
                        cantidad = c.Double(nullable: false),
                        id_unidad = c.Int(nullable: false),
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
                        id_unidad = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 50),
                        abreviatura = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.id_unidad);
            
            CreateTable(
                "dbo.Suministros",
                c => new
                    {
                        id_suministro = c.Int(nullable: false, identity: true),
                        id_categoria = c.Int(nullable: false),
                        nombre = c.String(nullable: false, maxLength: 50),
                        proveedor = c.String(maxLength: 50),
                        cantidad = c.Double(nullable: false),
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
                        id_producto = c.Int(nullable: false),
                        url = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.id_imagen)
                .ForeignKey("dbo.Productos", t => t.id_producto, cascadeDelete: true)
                .Index(t => t.id_producto);
            
            CreateTable(
                "dbo.Pagos",
                c => new
                    {
                        id_pago = c.Int(nullable: false, identity: true),
                        id_cliente = c.Int(nullable: false),
                        id_orden = c.Int(nullable: false),
                        fecha = c.DateTime(nullable: false),
                        monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        tipo_pago = c.String(),
                        CLIENTE_id_contacto = c.Int(),
                    })
                .PrimaryKey(t => t.id_pago)
                .ForeignKey("dbo.Contactos", t => t.CLIENTE_id_contacto)
                .ForeignKey("dbo.Ordenes", t => t.id_orden, cascadeDelete: true)
                .Index(t => t.id_orden)
                .Index(t => t.CLIENTE_id_contacto);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pagos", "id_orden", "dbo.Ordenes");
            DropForeignKey("dbo.Pagos", "CLIENTE_id_contacto", "dbo.Contactos");
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
            DropForeignKey("dbo.Ordenes", "id_orden_pago_estado", "dbo.Ordenes_Pago_Estados");
            DropForeignKey("dbo.Ordenes", "id_orden_estado", "dbo.Ordenes_Estados");
            DropForeignKey("dbo.Detalle_Ordenes", "id_orden", "dbo.Ordenes");
            DropForeignKey("dbo.Ordenes", "CLIENTE_id_contacto", "dbo.Contactos");
            DropForeignKey("dbo.Eventos", "id_tipo_evento", "dbo.Tipos_Eventos");
            DropForeignKey("dbo.Ordenes", "id_evento", "dbo.Eventos");
            DropForeignKey("dbo.Eventos", "CLIENTE_id_contacto", "dbo.Contactos");
            DropForeignKey("dbo.Productos", "id_categoria", "dbo.Categorias");
            DropIndex("dbo.Pagos", new[] { "CLIENTE_id_contacto" });
            DropIndex("dbo.Pagos", new[] { "id_orden" });
            DropIndex("dbo.Imagenes_Productos", new[] { "id_producto" });
            DropIndex("dbo.Suministros", new[] { "id_categoria" });
            DropIndex("dbo.Ingredientes", new[] { "id_unidad" });
            DropIndex("dbo.Detalle_Recetas", new[] { "id_ingrediente" });
            DropIndex("dbo.Detalle_Recetas", new[] { "id_receta" });
            DropIndex("dbo.Recetas", new[] { "id_categoria" });
            DropIndex("dbo.Detalle_Productos", new[] { "id_receta" });
            DropIndex("dbo.Detalle_Productos", new[] { "id_suministro" });
            DropIndex("dbo.Detalle_Productos", new[] { "id_producto" });
            DropIndex("dbo.Eventos", new[] { "CLIENTE_id_contacto" });
            DropIndex("dbo.Eventos", new[] { "id_tipo_evento" });
            DropIndex("dbo.Ordenes", new[] { "CLIENTE_id_contacto" });
            DropIndex("dbo.Ordenes", new[] { "id_evento" });
            DropIndex("dbo.Ordenes", new[] { "id_orden_pago_estado" });
            DropIndex("dbo.Ordenes", new[] { "id_orden_estado" });
            DropIndex("dbo.Detalle_Ordenes", new[] { "id_producto" });
            DropIndex("dbo.Detalle_Ordenes", new[] { "id_orden" });
            DropIndex("dbo.Productos", new[] { "id_categoria" });
            DropTable("dbo.Pagos");
            DropTable("dbo.Imagenes_Productos");
            DropTable("dbo.Suministros");
            DropTable("dbo.Unidades_Medida");
            DropTable("dbo.Ingredientes");
            DropTable("dbo.Detalle_Recetas");
            DropTable("dbo.Recetas");
            DropTable("dbo.Detalle_Productos");
            DropTable("dbo.Ordenes_Pago_Estados");
            DropTable("dbo.Ordenes_Estados");
            DropTable("dbo.Tipos_Eventos");
            DropTable("dbo.Eventos");
            DropTable("dbo.Contactos");
            DropTable("dbo.Ordenes");
            DropTable("dbo.Detalle_Ordenes");
            DropTable("dbo.Productos");
            DropTable("dbo.Categorias");
        }
    }
}
