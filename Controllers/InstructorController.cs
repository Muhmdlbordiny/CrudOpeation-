using Assighment.Models;
using Assighment.Repositry;
using Assighment.ViewModels;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Assighment.Controllers
{
    public class InstructorController : Controller
    {
        IinstructorRepo instrucRepositru;
        IDepartmentRepositry departmentRepositry;
        public InstructorController(IinstructorRepo _inRepo,IDepartmentRepositry _department)
        {
            instrucRepositru = _inRepo;
            departmentRepositry = _department;
        }

        //AppDbcontext context = new AppDbcontext();

        public IActionResult Index()
        {
            return View(instrucRepositru.GetAllWithDepartment());    //(context.Instructors.Include(x=>x.Department).ToList());
            //return View();
        }
        public IActionResult NewInstructor()
        {
            // return View(new Instructor());
            ViewData["DeptList"] = departmentRepositry.GetAll();//context.Departments.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNew(Instructor inst)
        {
            if (ModelState.IsValid == true)
            {
                if (inst.Dept_id != null)
                {
                    try
                    {
                       // context.Instructors.Add(inst);
                        //context.SaveChanges();
                        instrucRepositru.Add(inst);
                        return RedirectToAction("Index");
                    }
                    catch(Exception ex) 
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                }
                else
                {
                    ModelState.AddModelError("Dept_Id", "Select Department");
                }
            }
            ViewData["DeptList"] = departmentRepositry.GetAll();    //context.Departments.ToList();
            return View("NewInstructor", inst);
        }
        public IActionResult Edit(int id) 
        {
            Instructor ins = instrucRepositru.GetById(id); //context.Instructors.FirstOrDefault(x=>x.Id== id);
            ViewData["DeptList"] = departmentRepositry.GetAll();         //context.Departments.ToList();
            return View(ins);
        }
        public IActionResult Delete(int id)
        {
            //Instructor ins = context.Instructors.FirstOrDefault(c=>c.Id == id);
            //context.Remove(ins);
            //context.SaveChanges();
            instrucRepositru.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult GetInstructorUsingViewModel(int id, string Salary)
        {
            Instructor instructor = instrucRepositru.GetById(id); //context.Instructors.FirstOrDefault(x=>x.Id==id);
            List<string> branches = new List<string>();
            branches.Add("Alex");
            branches.Add("Assiut");
            branches.Add("Smart");
            branches.Add("Menia");
            branches.Add("ElMahmodia");
            InstructorUsingVM insVM =
                new InstructorUsingVM();
            insVM.instName = instructor.Name;
            insVM.instId = instructor.Id;
            insVM.Branches = branches;
            return View(insVM);
        }
        [HttpPost]
        public IActionResult SaveEdit(int id,  Instructor ins) 
        {
            Instructor oldins = instrucRepositru.GetById(id); //context.Instructors.FirstOrDefault(x => x.Id == id);
            if (ModelState.IsValid == true)
            {
                //oldins.Name = ins.Name;
                //oldins.Address= ins.Address;
                //oldins.Image = ins.Image;
                //oldins.Salary= ins.Salary;
                //oldins.Dept_id =ins.Dept_id;
                //context.SaveChanges();
                instrucRepositru.Edit(id, ins);
                return RedirectToAction("Index");
            }
            ViewData["DeptList"] = departmentRepositry.GetAll();//context.Departments.ToList();
            return View("Edit", ins);
            

            
        }

    }
}
