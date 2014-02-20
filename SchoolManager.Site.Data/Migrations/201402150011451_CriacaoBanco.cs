namespace SchoolManager.Site.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoBanco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classroom",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CollegeID = c.Int(nullable: false),
                        GraduationYear = c.Int(),
                        Students = c.Int(),
                        Coordinator = c.String(maxLength: 50),
                        Email = c.String(maxLength: 100),
                        Period = c.String(maxLength: 1),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.College", t => t.CollegeID, cascadeDelete: true)
                .Index(t => t.CollegeID);
            
            CreateTable(
                "dbo.College",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Adress = c.String(maxLength: 80),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classroom", "CollegeID", "dbo.College");
            DropIndex("dbo.Classroom", new[] { "CollegeID" });
            DropTable("dbo.College");
            DropTable("dbo.Classroom");
        }
    }
}
