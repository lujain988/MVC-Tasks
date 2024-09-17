namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inaital : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Asign",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AsignName = c.String(nullable: false),
                        date = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Asign");
        }
    }
}
