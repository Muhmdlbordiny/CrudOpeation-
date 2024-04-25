using Assighment.Models;
using Assighment.Repositry;
using Assighment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Assighment.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        //DIP
        IStudentRepositry studentRepositry;//Tigh Couple ==>Lossly Couple
        IDepartmentRepositry departmentrepositry;
        //DI
        public StudentController(IStudentRepositry _stdRepo,IDepartmentRepositry _DeptRepo)
        {
            // who send _stdrepo,deptrepo coming implement Ioc Container
            //studentRepositry = new StudentRepositry();
            studentRepositry = _stdRepo; //Ask
            //departmentrepositry = new Departmentrepositry();
            departmentrepositry = _DeptRepo;
        }
        public IActionResult TestService()
        {
            // id repo and view in view

            ViewBag.serviceID = studentRepositry.Id;
            return View();
        }
        public IActionResult DetailsUsingPartial(int id)
        {
            Student str = studentRepositry.GetbyId(id); //context.students.FirstOrDefault(c=>c.Id == id);
            return PartialView("_StudentCardPartial",str); //content view without Layout
        }
        public IActionResult Details(int id)
        {
            return View (studentRepositry.GetbyId(id) /*context.students.FirstOrDefault(x => x.Id == id)*/);
        }
        //remote Attribute using Ajax call
        public IActionResult CheckName(string Name,string Address)
        {
            
            if (Name.Contains("ITI"))
                return Json(true);
            return Json(false);
        }
        [HttpGet] //anchor ,form mthod get
        public IActionResult Action1()
        {
            return Content("Action1 Get");
        }
        [HttpPost]//<form method="post">
        public IActionResult Action1(int id, string name)
        {
            return Content("Action1 post");
        }
       // AppDbcontext context = new AppDbcontext();
        public IActionResult New()
        {
            ViewData["DeptList"] = departmentrepositry.GetAll();//context.Departments.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//intrenal calling only "FRom the same domain
        public IActionResult New(Student  newstd)
        {
            //if(newstd.Name != null) 
            if(ModelState.IsValid == true)
            {
                //custom validation dept_Id ! = 0
                if (newstd.Dept_Id != 0)
                {
                    try
                    {


                        //save
                        //context.students.Add(newstd);
                        //context.SaveChanges();
                        studentRepositry.Add(newstd);
                        return RedirectToAction("Index");
                    }catch(Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                }
                else
                {
                    //error message send view 
                    ModelState.AddModelError("Dept_Id", "Select Department");// display on  div if key not found
                }
            }
            // else
            //{
            ViewData["DeptList"] = departmentrepositry.GetAll();//context.Departments.ToList();// send viewbag because not send foreach on null throw exception 
                return View(newstd);
           // }
        }
        public IActionResult Delete(int id)
        {
           // Student std = studentRepositry.GetbyId(id); //context.students.FirstOrDefault(x=> x.Id == id);
            //context.students.Remove(std);
            studentRepositry.Delete(id);
            //context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult getstudent()
        {
            List<string> branches = new List<string>();
            branches.Add("Alex");
            branches.Add("Assiut");
            branches.Add("Smart");
            branches.Add("Menia");

            ViewData["brc"] = branches;
            // ViewData["temp"] = 44;
            ViewBag.temp = 44;

            ViewBag.color = "red";

            ViewData["temp"] = 55;
            ViewData["msg"] = "Hello FRom Action";


            Student stdModel = studentRepositry.GetbyId(1);//context.students.FirstOrDefault();
            return View(stdModel);//view="GetStudent" Model ="student"
        }
        public IActionResult Edit(int id)
        {
            Student student = studentRepositry.GetbyId(id); //context.students.FirstOrDefault(s => s.Id == id);//Model
            ViewData["DeptList"] = departmentrepositry.GetAll();//context.Departments.ToList();
            return View(student);
        }
        [HttpPost]
        public IActionResult SaveEdit(int id, Student newStd)
        {
           // Student oldStudent = context.students.FirstOrDefault(s => s.Id == id);
            //if (newStd.Name != null && newStd.Age > 10)
            if(ModelState.IsValid == true)
            {
                //get old object
               /* oldStudent.Name = newStd.Name;
                oldStudent.Address = newStd.Address;
                oldStudent.Age = newStd.Age;
                oldStudent.Image = newStd.Image;
                oldStudent.Dept_Id = newStd.Dept_Id;
                context.SaveChanges();*/
               studentRepositry.Edit(id, newStd);
                return RedirectToAction("Index");
                //save
            }
            //model 
            ViewData["DeptList"] = departmentrepositry.GetAll();//context.Departments.ToList();

            return View("Edit", newStd);
        }
        public IActionResult Index()
        {
            string name = User.Identity.Name;
            string id =  User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value; // arrive to Id from cookie
            return View(studentRepositry.GetAllStudentWithDepartmentDat()/*context.students.Include(s=>s.Department).ToList()*/);
        }
        public IActionResult GetStudentUsingViewModel(int id, string Salary)
        {
            //get Model
            Student stdModel = studentRepositry.GetbyId(id); //context.students.FirstOrDefault(s => s.Id == id);

            List<string> branches = new List<string>();
            branches.Add("Alex");
            branches.Add("Assiut");
            branches.Add("Smart");
            branches.Add("Menia");

            StudentBranchesTempMSgViewModel stdViewModel =
                new StudentBranchesTempMSgViewModel();
            stdViewModel.StdName = stdModel.Name;
            stdViewModel.StdId = stdModel.Id;
            stdViewModel.Msg = "Hello";
            stdViewModel.Color = "blue";
            stdViewModel.Temp = 20;
            stdViewModel.Branches = branches;

            return View(stdViewModel);

        }
    }
}
