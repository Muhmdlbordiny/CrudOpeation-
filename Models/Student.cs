using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Assighment.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Student Name")]
        [Required]
        [MaxLength(30,ErrorMessage ="Name must be less than 30 letter")]
        [MinLength(5,ErrorMessage ="Name must be greater than 4 letter")]
        //[UniqueName(Message ="Done Name")]
        //custom vaildator ITI
        [Remote(action:"CheckName",controller:"Student",
            AdditionalFields = "Address", ErrorMessage ="Name must contain ITI")]
        public string Name { get; set; }
        [Required]
        //[RegularExpression(@"[a-zA-Z 0-9] \d{3,25}")]
        [RegularExpression(@"(Alex|Assuit)",ErrorMessage ="Address must be Alex or Assuit")]

        public string Address { get; set; } 
        [Display(Name = "Student Age")]
        [Required]
        [Range(20,50,ErrorMessage ="Age must be between 20 &50")]
        public int Age { get; set; }
        [Required]
        [RegularExpression(@"\w+\.(jpg|png)",ErrorMessage ="Image must be jpg or png")]
        public string Image { get; set; }
        [ForeignKey("Department")]
        [Display(Name = "Department Name")]
        public int Dept_Id { get; set; }

        public virtual Department? Department { get; set; }
    }
}
