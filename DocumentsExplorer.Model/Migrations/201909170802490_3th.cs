namespace DocumentsExplorer.Model
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3th : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Round", "IsCurrent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Round", "IsCurrent");
        }
    }
}
