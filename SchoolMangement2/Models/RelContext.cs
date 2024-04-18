using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SchoolMangement2.Models
{
    public class RelContext:IdentityDbContext
    {
        public RelContext(DbContextOptions<RelContext> options):base(options)
        {
            
        }
        

       public DbSet<Employee> Employees { get; set; }
       public DbSet<Address> Addresss { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().
                HasKey(e => e.EmployeeId);

            modelBuilder.Entity<Address>().
                HasKey(e => e.Id);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.AddressList) // Assuming 'Address' is a navigation property in 'Employee' 
                .WithOne(a => a.Employee)   
                .HasForeignKey(a => a.EmployeeId); 
        }
        

    }
}
