namespace MVC_Day05.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set;}
        public string Degree { get; set;}
        public string MinDegree { get; set; }
        // foreign key
        public int DepartmentId { get; set; }

        // navigation property

        public Department Department { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<StuCrsRes> StuCrsRes { get; set; }
    }
}
