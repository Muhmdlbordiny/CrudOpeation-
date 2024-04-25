using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assighment.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="please Enter your Course")]
        public string Name { get; set; }
        [Range(50,100,ErrorMessage ="Must be degree between 50&100")]
        public int degre { get; set; }
        [Range(0,49,ErrorMessage ="Must be degree between 0,49")]
        public short mindegree { get; set; }

        [Display(Name ="Department Name")]
        [ForeignKey("Department")]
        public int Dept_Id { get; set; }
        public virtual  Department? Department { get; set; }
        [ForeignKey("Instructor")]
        [Display(Name ="Choose Instructor")]
        public int Inst_Id { get; set; }
        public virtual Instructor? Instructor { get; set; }

    }
}
