namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
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
            
            CreateTable(
                "dbo.course",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClassName = c.String(),
                        Description = c.String(),
                        TeacherID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Teacher", t => t.TeacherID, cascadeDelete: true)
                .Index(t => t.TeacherID);
            
            CreateTable(
                "dbo.Teacher",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TeacherName = c.String(nullable: false),
                        age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.studentDetails",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        FatherName = c.String(),
                        MotherName = c.String(),
                        NumOfSiblings = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Students", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentName = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                        StudentDetailsID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.studentDetails", "ID", "dbo.Students");
            DropForeignKey("dbo.course", "TeacherID", "dbo.Teacher");
            DropIndex("dbo.studentDetails", new[] { "ID" });
            DropIndex("dbo.course", new[] { "TeacherID" });
            DropTable("dbo.Students");
            DropTable("dbo.studentDetails");
            DropTable("dbo.Teacher");
            DropTable("dbo.course");
            DropTable("dbo.Asign");
        }
    }
}
