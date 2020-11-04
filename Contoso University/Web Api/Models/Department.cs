namespace Web_Api.Models
{
    public class Department
    {
        private int id;
        private string title;
        private string description;

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value;}
    }
}