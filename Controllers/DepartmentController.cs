using Assighment.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assighment.Controllers
{
    public class DepartmentController : Controller
    {
        public AppDbcontext context = new AppDbcontext();

        public  IActionResult ShowDepartmentStudent()
        {
            List<Department> deptlist = context.Departments.ToList();
            return View(deptlist);
        }
        //department/GetStudentPerDepartment?deptId=1
        public IActionResult GetStudentPerDepartment(int deptId)
        {
            List<Student> stds = context.students.Where(d => d.Dept_Id == deptId).ToList();
            return Json(stds);
        }
        public IActionResult Index()
        {
            List<Department> deptlist = context.Departments.ToList();
            return View(deptlist);
        }
        public IActionResult Details(int id)
        {
            Department dept = context.Departments.FirstOrDefault(x=>x.Id==id);
            return View("Details", dept);
        }
        public IActionResult NewDept()
        {
            return View(new Department());
        }
        [HttpPost]
        public IActionResult SaveNew(Department dept)
        {
            if(dept.Name != null && dept.Manger != null)
            {
                context.Departments.Add(dept);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("NewDept", dept);
           // return RedirectToAction("Details", new { id = dept.Id });
        }
    }
}
