using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Api.DTO
{
    public class CourseStudentDTO
    {
        private int courseId;
        private String courseTitle;
        private int studentId;
        private String studentFullName;

        public int CourseId { get => courseId; set => courseId = value; }
        public String CourseTitle { get => courseTitle; set => courseTitle = value; }
        public int StudentId { get => studentId; set => studentId = value; }
        public String StudentFullName { get => studentFullName; set => studentFullName = value; }
    }
}