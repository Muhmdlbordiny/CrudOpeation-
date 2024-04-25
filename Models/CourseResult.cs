using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assighment.Models
{
    public class CourseResult
    {
        [Key]
        public int Id { get; set; }
        public int degree { get; set; }

        [ForeignKey("Course")]
        public int Crs_id { get; set; }

        [ForeignKey("Trainne")]
        public int Trainee_id { get; set; }
        public  virtual Course Course { get; set; }
        public virtual Trainne Trainne { get; set; }

    }
}
