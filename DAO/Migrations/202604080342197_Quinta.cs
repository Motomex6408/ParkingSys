namespace DAO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Quinta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "Phone", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "Phone");
        }
    }
}
