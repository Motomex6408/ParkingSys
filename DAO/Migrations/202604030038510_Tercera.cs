namespace DAO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tercera : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empresa", "RTN", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.Empresa", "RFC");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Empresa", "RFC", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.Empresa", "RTN");
        }
    }
}
