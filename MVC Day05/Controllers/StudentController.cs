using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Day05.Models;
using MVC_Day05.Service;
using MVC_Day05.ViewModel;

namespace MVC_Day05.Controllers
{
    public class StudentController : Controller
    {
        private StudentServices _services;

        public StudentController(StudentServices services)
        {
            _services = services;
        }
        public IActionResult GetAll(string searchString, int? departmentId)
        {
            var students = _services.GetFilteredStudents(searchString, departmentId);

            var viewModel = new StudentListViewModel
            {
                Students = students,
                SearchString = searchString,
                DepartmentId = departmentId,
            };
            ViewData["DeptList"] = new SelectList(_services.GetDepartments(), "Id", "Name");
            return View(viewModel);
        }
        public IActionResult GetById(int id)
        {
            var student = _services.GetById(id);
            if (student == null)
            {
                return View("404");
            }
            ViewBag.StudentId = id;
            return View(student);
        }
        /// <summary>
        /// Add student
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            ViewData["DeptList"] = new SelectList(_services.GetDepartments(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Add(AddStudentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["DeptList"] = new SelectList(_services.GetDepartments(), "Id", "Name");
                return View(viewModel);
            }

            var student = new Student
            {
                Name = viewModel.Name,
                Age = viewModel.Age,
                DepartmentId = viewModel.DepartmentId
            };
            _services.Add(student);
            return RedirectToAction("GetAll");
        }

        /// <summary>
        /// Edit student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _services.GetById(id);
            if (student == null)
            {
                return View("404");
            }
            var viewModel = new AddStudentViewModel
            {
                Name = student.Name,
                Age = student.Age,
                DepartmentId = student.DepartmentId
            };

            ViewData["DeptList"] = new SelectList(_services.GetDepartments(), "Id", "Name");
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Edit(int id, AddStudentViewModel viewModel)
        {
            var student = _services.GetById(id);
            if (student == null)
            {
                return View("404");
            }
            if (!ModelState.IsValid)
            {
                ViewData["DeptList"] = new SelectList(_services.GetDepartments(), "Id", "Name");
                return View(viewModel);
            }
            student.Name = viewModel.Name;
            student.Age = viewModel.Age;
            student.DepartmentId = viewModel.DepartmentId;
            var status = _services.Update(student);
            if (status == null)
            {
                return BadRequest();
            }
            return RedirectToAction("GetAll");
        }
        /// <summary>
        /// Delete student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var student = _services.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View("Warning", student);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            _services.Delete(id);
            return RedirectToAction("GetAll");
        }

    }
}
