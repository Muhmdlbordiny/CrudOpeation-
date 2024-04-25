using Assighment.Models;
using Microsoft.EntityFrameworkCore;

namespace Assighment.Repositry
{
    public class InstructorRepo : IinstructorRepo
    {
        AppDbcontext context;
        public InstructorRepo(AppDbcontext _context)
        {
            context = _context;
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        public void Add(Instructor instructor)
        {
            context.Instructors.Add(instructor);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Instructor old = GetById(id);
            context.Instructors.Remove(old);
            context.SaveChanges();
        }

        public void Edit(int id, Instructor instructor)
        {
            Instructor old = GetById(id);
            old.Id=instructor.Id;
            old.Name=instructor.Name;
            old.Address=instructor.Address;
            old.Image=instructor.Image;
            old.Salary=instructor.Salary;
            old.Dept_id=instructor.Dept_id;
            context.SaveChanges();
        }

        public List<Instructor> GetAll()
        {
            return context.Instructors.ToList();
        }

        public List<Instructor> GetAllWithDepartment()
        {
            return context.Instructors.Include(x=>x.Department).ToList();
        }

        public Instructor GetById(int id)
        {
            return context.Instructors.FirstOrDefault(c => c.Id == id);
        }
    }
}
