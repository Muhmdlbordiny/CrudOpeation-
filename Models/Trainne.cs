using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assighment.Models
{
    public class Trainne
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public int Grade { get; set; }
        [Required]
        [ForeignKey("Department")]
        public int Dep_Id { get; set; }
        public virtual Department Department{ get; set; }
    }
}
