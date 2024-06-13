namespace Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class saco_direccion_google : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Direcciones", "calle_numero", c => c.String());
            AddColumn("dbo.Direcciones", "localidad", c => c.String());
            AddColumn("dbo.Direcciones", "codigo_postal", c => c.String());
            AddColumn("dbo.Direcciones", "piso", c => c.String());
            AddColumn("dbo.Direcciones", "departamento", c => c.String());
            AddColumn("dbo.Direcciones", "provincia", c => c.String());
            DropColumn("dbo.Direcciones", "descripcion");
            DropColumn("dbo.Direcciones", "google_name");
            DropColumn("dbo.Direcciones", "google_lat");
            DropColumn("dbo.Direcciones", "google_lng");
            DropColumn("dbo.Direcciones", "google_place_id");
            DropColumn("dbo.Direcciones", "google_formatted_address");
            DropColumn("dbo.Direcciones", "google_url");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Direcciones", "google_url", c => c.String());
            AddColumn("dbo.Direcciones", "google_formatted_address", c => c.String());
            AddColumn("dbo.Direcciones", "google_place_id", c => c.String());
            AddColumn("dbo.Direcciones", "google_lng", c => c.String());
            AddColumn("dbo.Direcciones", "google_lat", c => c.String());
            AddColumn("dbo.Direcciones", "google_name", c => c.String());
            AddColumn("dbo.Direcciones", "descripcion", c => c.String());
            DropColumn("dbo.Direcciones", "provincia");
            DropColumn("dbo.Direcciones", "departamento");
            DropColumn("dbo.Direcciones", "piso");
            DropColumn("dbo.Direcciones", "codigo_postal");
            DropColumn("dbo.Direcciones", "localidad");
            DropColumn("dbo.Direcciones", "calle_numero");
        }
    }
}
