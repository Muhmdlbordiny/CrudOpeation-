using System.ComponentModel.DataAnnotations;

namespace Assighment.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username {  get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password {  get; set; }
        public bool RemmeberMe {  get; set; }
    }
}
