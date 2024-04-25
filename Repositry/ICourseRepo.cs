using Assighment.Models;

namespace Assighment.Repositry
{
    public interface ICourseRepo
    {
        List<Course> GetAll();
        List<Course> GetAllWithDepartment();
        Course GetById(int id);
        void Add(Course course);
        void Edit(int id , Course course);
        void Delete(int id);
    }
}
