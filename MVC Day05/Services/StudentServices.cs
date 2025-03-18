using Microsoft.EntityFrameworkCore;
using MVC_Day05.DbContexts;
using MVC_Day05.Models;

namespace MVC_Day05.Service
{
    public class StudentServices
    {
        AcademyDbContext db = new AcademyDbContext();

        public List<Student> GetAll()
        {
            return db.Students.Include(s => s.Department).ToList();
        }
        public Student GetById(int id)
        {
            return db.Students.Include(s => s.Department).FirstOrDefault(s => s.Id == id);
        }
        public void Add(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges();
        }

        public Student Update(Student student)
        {
            db.Students.Update(student);
            db.SaveChanges();
            return student;
        }

        public void Delete(int id)
        {
            var student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
        }
        public List<Department> GetDepartments()
        {
            return db.Departments.ToList();
        }

        public List<Student> GetFilteredStudents(string searchString, int? departmentId)
        {
            var query = db.Students.Include(s => s.Department).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Name.Contains(searchString));
            }

            if (departmentId.HasValue)
            {
                query = query.Where(s => s.DepartmentId == departmentId.Value);
            }

            return query.ToList();
        }
    }
}
