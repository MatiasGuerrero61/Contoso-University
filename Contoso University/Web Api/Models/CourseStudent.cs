using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Api.Models
{
    public class CourseStudent
    {
        private int id;
        private Course course;
        private Student student;
        private String state;

        public int Id { get => id; set => id = value; }
        public Course Course { get => course; set => course = value; }
        public Student Student { get => student; set => student = value; }
        public String State { get => state; set => state = value; }
    }
}