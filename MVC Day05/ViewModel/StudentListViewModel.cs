using MVC_Day05.Models;

namespace MVC_Day05.ViewModel
{
    public class StudentListViewModel
    {
        public List<Student> Students { get; set; }
        public string SearchString { get; set; }
        public int? DepartmentId { get; set; }
    }
}
