using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Api.DTO
{
    public class CourseDTO
    {
        private int courseId;
        private String courseName;
        private String departmentName;
        private String instructorName;

        public int CourseId { get => courseId; set => courseId = value; }
        public String CourseName { get => courseName; set => courseName = value; }
        public String DepartmentName { get => departmentName; set => departmentName = value; }
        public String InstructorName { get => instructorName; set => instructorName = value; }

    }
}