using Microsoft.AspNetCore.Identity;

namespace Assighment.Models
{
    public class ApplicationUser:IdentityUser
    {
        public  string Address { get; set; }
    }
}
