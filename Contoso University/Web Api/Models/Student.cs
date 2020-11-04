using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web_Api.Models
{
    public class Student
    {
        public Student()
        {
            this.Courses = new List<Course>();
        }

        private string name;
        private string lastName;
        private string dni;
        private int userId;


        public int Id { get; set; }
        public string Name { get => name; set => name = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Dni { get => dni; set => dni = value; }
        public int UserId { get => userId; set => userId = value; }
        public ICollection<Course> Courses { get; set; }
    }
}