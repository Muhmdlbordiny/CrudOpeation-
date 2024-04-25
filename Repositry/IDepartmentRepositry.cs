using Assighment.Models;

namespace Assighment.Repositry
{
    public interface IDepartmentRepositry
    {
        List<Department> GetAll();
        Department GetbyId(int id);
        void Add(Department dept);
        void Edit(int id, Department dept);
        void Delete(int id);
    }
}
