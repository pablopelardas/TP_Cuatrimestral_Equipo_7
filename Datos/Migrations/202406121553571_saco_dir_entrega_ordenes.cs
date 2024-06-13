namespace Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class saco_dir_entrega_ordenes : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ordenes", "direccion_entrega");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ordenes", "direccion_entrega", c => c.String(maxLength: 200));
        }
    }
}
