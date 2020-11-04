using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_Api.Models
{
    public class Course
    {
        public Course()
        {
            this.Students = new List<Student>();
        }

        private int id;
        private string title;
        private Department department;
        private Instructor instructor;
        private Nullable<int> instructorId;
        private Nullable<int> departmentId;
        private int capacity;

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public Department Department { get => department; set => department = value; }
        public Instructor Instructor { get => instructor; set => instructor = value; }
        public int Capacity { get => capacity; set => capacity = value; }
        public int? InstructorId { get => instructorId; set => instructorId = value; }
        public int? DepartmentId { get => departmentId; set => departmentId = value; }

        public virtual ICollection<Student> Students { get; set; }
    }
}