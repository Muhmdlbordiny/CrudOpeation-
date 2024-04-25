using System.ComponentModel.DataAnnotations;

namespace Assighment.Models
{
    public  class Department
    {
        [Key]
        public int Id { get; set; }
       
        [Length(5,20)]
        public string Name { get; set; }

        [Length(5, 20)]
        public string Manger { get; set; }
        public virtual List< Course> Course { get; set; }
        public virtual List<Instructor> Instructor { get; set; }
        public virtual List<Student> Student { get; set; }
        


    }
}
