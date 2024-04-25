using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace Assighment.Models
{
    public class AppDbcontext:IdentityDbContext<ApplicationUser>
    {
        public AppDbcontext():base()
        {
            
        }
        public AppDbcontext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Course> Courses { get; set; }


        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Trainne> Trainnes { get; set; }
        public DbSet<CourseResult> CourseResults { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.
                UseSqlServer("Server = DESKTOP-SMHGA8V\\SQL;Database=products;user Id = sa;password =1234;TrustServerCertificate =True");
        }
       

    }
}
