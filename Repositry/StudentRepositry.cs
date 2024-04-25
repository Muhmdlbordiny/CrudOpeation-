using Assighment.Models;
using Microsoft.EntityFrameworkCore;

namespace Assighment.Repositry
{
    public class StudentRepositry: IStudentRepositry
    {
        //DpI
        AppDbcontext context;//= new AppDbcontext();

        public Guid Id { get ; set ; }
        public StudentRepositry(AppDbcontext _context)
        {
            Id = Guid.NewGuid();
            context = _context;
        }

        //CRUD
        public List<Student> GetAll()
        {
            return context.students.ToList();
        }
        public List<Student>GetAllStudentWithDepartmentDat()  
        {
            return context.students.Include(s => s.Department).ToList();
        }
        public Student GetbyId(int id) 
        {
            return context.students.FirstOrDefault(x => x.Id == id);
        }
        public void Add(Student item)
        {
             context.students.Add(item);
            context.SaveChanges();
        }
        public void Edit(int id ,Student item)
        {
            Student oldstd = GetbyId(id);
            oldstd.Name = item.Name;
            oldstd.Address = item.Address;
            oldstd.Age = item.Age;
            oldstd.Dept_Id = item.Dept_Id;
            oldstd.Image = item.Image;
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            Student oldstd = GetbyId(id);
            context.students.Remove(oldstd);
            context.SaveChanges();


        }
    }
}
