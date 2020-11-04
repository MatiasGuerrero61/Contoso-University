using System;

namespace Web_Api.Models
{
    public class Instructor
    {
        private int id;
        private string name;
        private string lastName;
        private DateTime hireDate;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public DateTime HireDate { get => hireDate; set => hireDate = value; }
    }
}