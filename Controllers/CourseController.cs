using Assighment.Models;
using Assighment.Repositry;
using Assighment.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assighment.Controllers
{
    public class CourseController : Controller
    {
       // AppDbcontext context = new AppDbcontext();
       ICourseRepo courseRepo;
        IDepartmentRepositry departmentRepo;
        IinstructorRepo instructorRepo;
        public CourseController(ICourseRepo _crs,IDepartmentRepositry _department,IinstructorRepo _inst)
        {
            courseRepo = _crs;
            departmentRepo = _department;
             instructorRepo = _inst;
        }
        public IActionResult Index()
        {
            //List <Course> course = context.Courses.ToList();
            return View(courseRepo.GetAllWithDepartment());//(context.Courses.Include(v=>v.Department).ToList());
        }
        [HttpGet]
        public IActionResult New()
        {
            //return View(new Course());
            ViewData["Deptitem"] = departmentRepo.GetAll(); //context.Departments.ToList();
            ViewData["Institem"] = instructorRepo.GetAll(); //context.Instructors.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Course course)
        {
            if (ModelState.IsValid == true)
            {
                
                
                if (course.Dept_Id != 0&&course.Inst_Id!=0)
                 {
                    try
                    {
                        //context.Courses.Add(course);
                        //context.SaveChanges();
                        courseRepo.Add(course);
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                }
                else 
                {
                   ModelState.AddModelError("Dept_Id", "Select Department");
                    ModelState.AddModelError("Inst_Id", "Select Instructor");


                }
                

            }
            ViewData["Deptitem"] = departmentRepo.GetAll(); //context.Departments.ToList();
            ViewData["Institem"] = instructorRepo.GetAll(); //context.Instructors.ToList();

            return View( course);
        }
        public IActionResult Edit(int id) 
        {
            // ViewBag.Depts = context.Departments.ToList<Department>();
            Course cs = courseRepo.GetById(id);//context.Courses.FirstOrDefault(c=>c.Id==id);
            ViewData["Deptitem"] = departmentRepo.GetAll(); //context.Departments.ToList();
            ViewData["Institem"] = instructorRepo.GetAll();//context.Instructors.ToList();

            return View(cs);
        }
        [HttpPost]
        public IActionResult SaveEdit(int id,Course newcs)
        {
            Course oldcs = courseRepo.GetById(id); //context.Courses.FirstOrDefault(x => x.Id == id);

            if (ModelState.IsValid == true)
            {
                //oldcs.Name = newcs.Name;
                //oldcs.degre = newcs.degre;
                //oldcs.mindegree = newcs.mindegree;
                //oldcs.Dept_Id = newcs.Dept_Id;
                //oldcs.Inst_Id = newcs.Inst_Id;
                //context.SaveChanges();
                courseRepo.Edit(id, newcs);
                // return RedirectToAction("Details", "Department", new {id =cs.Dept_Id});
                return RedirectToAction("Index");
            }
            ViewData["Deptitem"] = departmentRepo.GetAll();//context.Departments.ToList();
            ViewData["Institem"] = instructorRepo.GetAll();// context.Instructors.ToList();
            return View("Edit", newcs);

        }
        public IActionResult Delete(int id)
        {
            //Course cr = context.Courses.FirstOrDefault(c=>c.Id == id);
            //context.Remove(cr);
            //context.SaveChanges();
            courseRepo.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult GetCourseUsingVM(int id)
        {
            Course csModel = courseRepo.GetById(id);//context.Courses.FirstOrDefault(s=>s.Id==id);
            List<string> branches = new List<string>();
            branches.Add("Alex");
            branches.Add("Assiut");
            branches.Add("Smart");
            branches.Add("Menia");
            CourseUsingVM csviewModel =
                new CourseUsingVM();
            csviewModel.stdName = csModel.Name;
            csviewModel.stdId = csModel.Id;
            csviewModel.Branches = branches;
            csviewModel.stdDescription = "the content of course from sql server and C#";
            return View(csviewModel);


        }
    }
}
