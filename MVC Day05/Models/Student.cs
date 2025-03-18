namespace MVC_Day05.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        // foreign key
        public int DepartmentId { get; set; }

        // navigation property

        public Department Department { get; set; }
        public List<StuCrsRes> StuCrsRes { get; set; }
    }
}
