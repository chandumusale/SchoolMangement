using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SchoolMangement2.Models
{
    public class EnrollContext:DbContext
    {
        public EnrollContext(DbContextOptions<EnrollContext>options):base(options)
        {
           
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<NewCourse> NewCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewCourse>()
                .HasKey(cs => new { cs.CourseId, cs.StudentId });

            modelBuilder.Entity<NewCourse>()
                .HasOne(cs => cs.Course)
                .WithMany(c => c.NewCourses)
                .HasForeignKey(cs => cs.CourseId);

            modelBuilder.Entity<NewCourse>()
                .HasOne(cs => cs.Student)
                .WithMany(s => s.NewCourses)
                .HasForeignKey(cs => cs.StudentId);


        }
    }
}
