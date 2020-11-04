namespace Web_Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "Department_Id", "dbo.Departments");
            DropForeignKey("dbo.Courses", "Instructor_Id", "dbo.Instructors");
            DropIndex("dbo.Courses", new[] { "Department_Id" });
            DropIndex("dbo.Courses", new[] { "Instructor_Id" });
            DropColumn("dbo.Courses", "DepartmentId");
            DropColumn("dbo.Courses", "InstructorId");
            RenameColumn(table: "dbo.Courses", name: "Department_Id", newName: "DepartmentId");
            RenameColumn(table: "dbo.Courses", name: "Instructor_Id", newName: "InstructorId");
            AlterColumn("dbo.Courses", "InstructorId", c => c.Int());
            AlterColumn("dbo.Courses", "DepartmentId", c => c.Int());
            CreateIndex("dbo.Courses", "InstructorId");
            CreateIndex("dbo.Courses", "DepartmentId");
            AddForeignKey("dbo.Courses", "DepartmentId", "dbo.Departments", "Id");
            AddForeignKey("dbo.Courses", "InstructorId", "dbo.Instructors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "InstructorId", "dbo.Instructors");
            DropForeignKey("dbo.Courses", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Courses", new[] { "DepartmentId" });
            DropIndex("dbo.Courses", new[] { "InstructorId" });
            AlterColumn("dbo.Courses", "DepartmentId", c => c.Int(nullable: false));
            AlterColumn("dbo.Courses", "InstructorId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Courses", name: "InstructorId", newName: "Instructor_Id");
            RenameColumn(table: "dbo.Courses", name: "DepartmentId", newName: "Department_Id");
            AddColumn("dbo.Courses", "InstructorId", c => c.Int(nullable: false));
            AddColumn("dbo.Courses", "DepartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "Instructor_Id");
            CreateIndex("dbo.Courses", "Department_Id");
            AddForeignKey("dbo.Courses", "Instructor_Id", "dbo.Instructors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Courses", "Department_Id", "dbo.Departments", "Id", cascadeDelete: true);
        }
    }
}
