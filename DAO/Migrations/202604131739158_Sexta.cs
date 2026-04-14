namespace DAO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sexta : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Kardex", "IdEspacioAnterior", "dbo.Espacio");
            DropForeignKey("dbo.Kardex", "IdEspacioNuevo", "dbo.Espacio");
            DropIndex("dbo.Kardex", new[] { "IdEspacioAnterior" });
            DropIndex("dbo.Kardex", new[] { "IdEspacioNuevo" });
            AddColumn("dbo.Kardex", "Monto", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Kardex", "Descripcion", c => c.String(maxLength: 200));
            AlterColumn("dbo.Kardex", "TipoMovimiento", c => c.String(nullable: false, maxLength: 20));
            DropColumn("dbo.Kardex", "IdEspacioAnterior");
            DropColumn("dbo.Kardex", "IdEspacioNuevo");
            DropColumn("dbo.Kardex", "Observacion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Kardex", "Observacion", c => c.String(maxLength: 150));
            AddColumn("dbo.Kardex", "IdEspacioNuevo", c => c.Int());
            AddColumn("dbo.Kardex", "IdEspacioAnterior", c => c.Int());
            AlterColumn("dbo.Kardex", "TipoMovimiento", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Kardex", "Descripcion");
            DropColumn("dbo.Kardex", "Monto");
            CreateIndex("dbo.Kardex", "IdEspacioNuevo");
            CreateIndex("dbo.Kardex", "IdEspacioAnterior");
            AddForeignKey("dbo.Kardex", "IdEspacioNuevo", "dbo.Espacio", "Id");
            AddForeignKey("dbo.Kardex", "IdEspacioAnterior", "dbo.Espacio", "Id");
        }
    }
}
