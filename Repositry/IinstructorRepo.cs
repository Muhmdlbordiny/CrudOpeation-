using Assighment.Models;

namespace Assighment.Repositry
{
    public interface IinstructorRepo
    {
         Guid Id { get; set; }
        List<Instructor> GetAll();
        List<Instructor> GetAllWithDepartment();
        Instructor GetById(int id);
        void Add(Instructor instructor);
        void Edit (int id,Instructor instructor);
        void Delete(int id);
    }
}
