namespace Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class id_direccion_opcional : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ordenes", "id_direccion", "dbo.Direcciones");
            DropIndex("dbo.Ordenes", new[] { "id_direccion" });
            AlterColumn("dbo.Ordenes", "id_direccion", c => c.Guid());
            CreateIndex("dbo.Ordenes", "id_direccion");
            AddForeignKey("dbo.Ordenes", "id_direccion", "dbo.Direcciones", "id_direccion");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ordenes", "id_direccion", "dbo.Direcciones");
            DropIndex("dbo.Ordenes", new[] { "id_direccion" });
            AlterColumn("dbo.Ordenes", "id_direccion", c => c.Guid(nullable: false));
            CreateIndex("dbo.Ordenes", "id_direccion");
            AddForeignKey("dbo.Ordenes", "id_direccion", "dbo.Direcciones", "id_direccion", cascadeDelete: true);
        }
    }
}
