using System.ComponentModel.DataAnnotations;

namespace Assighment.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        public string RoleName {  get; set; }
    }
}
