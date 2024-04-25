using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assighment.Models
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Must be field Name")]
        [Required]
        [MaxLength(30,ErrorMessage =" name Must be  less than 30 letter")]
        [MinLength(3,ErrorMessage ="Name must be greter than 2 letter")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"\w+\.(jpg|png)", ErrorMessage = "Image must be jpg or png")]

        public string Image { get; set; }
        [RegularExpression(@"Alex|Elmahmodia|Cairo|",ErrorMessage ="Address must be field")]
        public string Address { get; set; }
        [Required]
        [Display(Name ="What is the Excepected Salary")]
        
        public int Salary { get; set; }
        [ForeignKey("Department")]
        [Display(Name = "Department Name")]

        public int Dept_id { get; set; }
        public virtual Department? Department{ get; set; }
        
        //[ForeignKey("Course")]
        //public int Crs_Id { get; set; }
        public virtual List <Course>? Course { get; set; }

    }
}
