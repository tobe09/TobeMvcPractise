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
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Unit = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "Id", "dbo.Employees");
            DropIndex("dbo.Courses", new[] { "Id" });
            DropTable("dbo.Courses");
        }
    }
}
