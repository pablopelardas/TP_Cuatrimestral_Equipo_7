namespace Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Unifico_Decimales : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Detalle_Recetas", "cantidad", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Ingredientes", "cantidad", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Suministros", "cantidad", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Suministros", "cantidad", c => c.Double(nullable: false));
            AlterColumn("dbo.Ingredientes", "cantidad", c => c.Double(nullable: false));
            AlterColumn("dbo.Detalle_Recetas", "cantidad", c => c.Double(nullable: false));
        }
    }
}
