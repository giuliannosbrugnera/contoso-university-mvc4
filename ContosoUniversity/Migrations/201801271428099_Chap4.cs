namespace ContosoUniversity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Chap4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Budget = c.Decimal(nullable: false, storeType: "money"),
                        StartDate = c.DateTime(nullable: false),
                        InstructorId = c.Int(),
                    })
                .PrimaryKey(t => t.DepartmentId)
                .ForeignKey("dbo.Instructor", t => t.InstructorId)
                .Index(t => t.InstructorId);

            CreateTable(
                "dbo.Instructor",
                c => new
                    {
                        InstructorId = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        HireDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.InstructorId);

            CreateTable(
                "dbo.OfficeAssignment",
                c => new
                    {
                        InstructorId = c.Int(nullable: false),
                        Location = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.InstructorId)
                .ForeignKey("dbo.Instructor", t => t.InstructorId)
                .Index(t => t.InstructorId);

            CreateTable(
                "dbo.CourseInstructor",
                c => new
                    {
                        CourseId = c.Int(nullable: false),
                        InstructorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseId, t.InstructorId })
                .ForeignKey("dbo.Course", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Instructor", t => t.InstructorId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.InstructorId);

            // Create  a department for course to point to.
            Sql("INSERT INTO dbo.Department (Name, Budget, StartDate) VALUES ('Temp', 0.00, GETDATE())");
            //  default value for FK points to department created above.
            AddColumn("dbo.Course", "DepartmentId", c => c.Int(nullable: false, defaultValue: 1));
            //AddColumn("dbo.Course", "DepartmentId", c => c.Int(nullable: false));

            AlterColumn("dbo.Course", "Title", c => c.String(maxLength: 50));
            CreateIndex("dbo.Course", "DepartmentId");
            AddForeignKey("dbo.Course", "DepartmentId", "dbo.Department", "DepartmentId", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.CourseInstructor", "InstructorId", "dbo.Instructor");
            DropForeignKey("dbo.CourseInstructor", "CourseId", "dbo.Course");
            DropForeignKey("dbo.Course", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.Department", "InstructorId", "dbo.Instructor");
            DropForeignKey("dbo.OfficeAssignment", "InstructorId", "dbo.Instructor");
            DropIndex("dbo.CourseInstructor", new[] { "InstructorId" });
            DropIndex("dbo.CourseInstructor", new[] { "CourseId" });
            DropIndex("dbo.OfficeAssignment", new[] { "InstructorId" });
            DropIndex("dbo.Department", new[] { "InstructorId" });
            DropIndex("dbo.Course", new[] { "DepartmentId" });
            AlterColumn("dbo.Course", "Title", c => c.String());
            DropColumn("dbo.Course", "DepartmentId");
            DropTable("dbo.CourseInstructor");
            DropTable("dbo.OfficeAssignment");
            DropTable("dbo.Instructor");
            DropTable("dbo.Department");
        }
    }
}
