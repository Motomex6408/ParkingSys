namespace DAO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cuarta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "Foto", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "Foto");
        }
    }
}
