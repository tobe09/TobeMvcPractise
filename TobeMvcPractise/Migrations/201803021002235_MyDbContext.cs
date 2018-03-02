namespace TobeMvcPractise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyDbContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Unit = c.Int(nullable: false),
                        Migrate = c.Int(),
                        Employee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id, cascadeDelete: true)
                .Index(t => t.Employee_Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 60),
                        JoiningDate = c.DateTime(nullable: false),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "Employee_Id", "dbo.Employees");
            DropIndex("dbo.Courses", new[] { "Employee_Id" });
            DropTable("dbo.Employees");
            DropTable("dbo.Courses");
        }
    }
}
