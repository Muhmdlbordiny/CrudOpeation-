using Assighment.Models;

namespace Assighment.Repositry
{
    public interface IStudentRepositry
    {
        Guid Id { get; set; }
         List<Student> GetAll();
        List<Student> GetAllStudentWithDepartmentDat();
        Student GetbyId(int id);
        void Add(Student item);
        void Edit(int id, Student item);
        void Delete(int id);



    }
}
