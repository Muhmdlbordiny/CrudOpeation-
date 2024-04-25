using System.ComponentModel.DataAnnotations;

namespace Assighment.Models
{
    public class UniqueNameAttribute:ValidationAttribute
    {
        public string Message { get; set; }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value == null)
                return null;
            string newname =  value.ToString();
            AppDbcontext context = new AppDbcontext();
           Student stdDb = context.students.FirstOrDefault(s => s.Name == newname);
           // Student stdForm = (Student)validationContext.ObjectInstance;
            if(stdDb != null)
            {
                return new ValidationResult("Name Must be Unique");
            }
            return ValidationResult.Success;
        }
    }
}
