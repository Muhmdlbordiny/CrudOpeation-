using Assighment.Models;

namespace Assighment.Repositry
{
    public class Departmentrepositry: IDepartmentRepositry
    {
        AppDbcontext context;//= new AppDbcontext();
        public Departmentrepositry(AppDbcontext _context)
        {
            context = _context;
        }
        public List<Department> GetAll()
        {
            return context.Departments.ToList();
        }
        public Department GetbyId(int id)
        {
            return context.Departments.FirstOrDefault(d => d.Id == id);
        }
        public void Add(Department dept)
        {
            context.Departments.Add(dept);
            context.SaveChanges();

        }
        public void Edit(int id, Department dept)
        {
           Department oldDept= GetbyId(id);
            oldDept.Name = dept.Name;
            oldDept.Manger = dept.Manger;
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            Department oldDept = GetbyId(id);
            context.Departments.Remove(oldDept);
            context.SaveChanges();

        }
    }
}
