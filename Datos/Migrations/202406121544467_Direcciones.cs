namespace Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Direcciones : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Direcciones",
                c => new
                    {
                        id_direccion = c.Guid(nullable: false, identity: true),
                        descripcion = c.String(),
                        google_name = c.String(),
                        google_lat = c.String(),
                        google_lng = c.String(),
                        google_place_id = c.String(),
                        google_formatted_address = c.String(),
                        google_url = c.String(),
                    })
                .PrimaryKey(t => t.id_direccion);
            
            AddColumn("dbo.Ordenes", "id_direccion", c => c.Guid(nullable: false));
            CreateIndex("dbo.Ordenes", "id_direccion");
            AddForeignKey("dbo.Ordenes", "id_direccion", "dbo.Direcciones", "id_direccion", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ordenes", "id_direccion", "dbo.Direcciones");
            DropIndex("dbo.Ordenes", new[] { "id_direccion" });
            DropColumn("dbo.Ordenes", "id_direccion");
            DropTable("dbo.Direcciones");
        }
    }
}
