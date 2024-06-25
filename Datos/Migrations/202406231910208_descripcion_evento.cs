namespace Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class descripcion_evento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Eventos", "descripcion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Eventos", "descripcion");
        }
    }
}
