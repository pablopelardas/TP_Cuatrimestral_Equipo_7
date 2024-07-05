namespace Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agrego_solicitudes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Solicitudes",
                c => new
                    {
                        id_solicitud = c.Guid(nullable: false, identity: true),
                        tipo_evento = c.Guid(nullable: false),
                        fecha_evento = c.DateTime(nullable: false),
                        tipo_entrega = c.String(),
                        hora_entrega = c.Time(nullable: false, precision: 7),
                        calle_numero = c.String(),
                        localidad = c.String(),
                        codigo_postal = c.String(),
                        piso = c.String(),
                        departamento = c.String(),
                        provincia = c.String(),
                        json_productos = c.String(),
                        nombre_apellido = c.String(maxLength: 100),
                        correo = c.String(maxLength: 100),
                        telefono = c.String(maxLength: 20),
                        fuente = c.String(maxLength: 100),
                        desea_recibir_correos = c.Boolean(nullable: false),
                        desea_recibir_whatsapp = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id_solicitud);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Solicitudes");
        }
    }
}
