using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Web_Api.Models;

namespace Web_Api.DAL
{
    public class ContosoUniversityContext : DbContext
    {
        public ContosoUniversityContext(): base("ContosoUniversity")
        {
        }
        public virtual DbSet<Course> Courses { get; set; }
        //public virtual DbSet<CourseStudent> CourseStudents { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<User> Users { get; set; } 
        public virtual DbSet<Admin> Admins { get; set; }

        /*
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasOptional(i => i.Instructor)
                .WithOptionalDependent()
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Course>()
                .HasOptional(d => d.Department)
                .WithOptionalDependent()
                .WillCascadeOnDelete(true);
        }
        */
    }
}