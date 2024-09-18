namespace Code.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _try : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Students", "StudentDetailsID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "StudentDetailsID", c => c.Int(nullable: false));
        }
    }
}
