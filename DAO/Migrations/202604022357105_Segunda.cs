namespace DAO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Segunda : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cliente", "RTN", c => c.String(maxLength: 30));
            DropColumn("dbo.Cliente", "RFC");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cliente", "RFC", c => c.String(maxLength: 30));
            DropColumn("dbo.Cliente", "RTN");
        }
    }
}
