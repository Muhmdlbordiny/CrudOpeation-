using Assighment.Models;
using Microsoft.EntityFrameworkCore;

namespace Assighment.Repositry
{
    public class CourseRepo : ICourseRepo
    {
        AppDbcontext context;
        public CourseRepo(AppDbcontext _context)
        {
            context = _context;
        }
        public void Add(Course course)
        {
           context.Courses.Add(course);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Course old =GetById(id);
            context.Courses.Remove(old);
            context.SaveChanges();
        }

        public void Edit(int id, Course course)
        {
            Course old = GetById(id);
            old.Id= course.Id;
            old.Name= course.Name;
            old.degre = course.degre;
            old.mindegree = course.mindegree;
            old.Dept_Id = course.Dept_Id;
            old.Inst_Id = course.Inst_Id;
            context.SaveChanges();
        }

        public List<Course> GetAll()
        {
            return context.Courses.ToList();
        }

        public List<Course> GetAllWithDepartment()
        {
            return context.Courses.Include(x => x.Department).ToList();
        }

        public Course GetById(int id)
        {
            return context.Courses.FirstOrDefault(x => x.Id == id);
        }
    }
}
