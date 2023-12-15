using Microsoft.EntityFrameworkCore;

namespace StudentRestAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 

        }

        public DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //send Student Table
            modelBuilder.Entity<Student>().HasData(
                new Student 
                {
                    StudentID = 1,
                    FirstName = "Jubayed",
                    LastName = "Ahmed",
                    Email = "jubayedsr@gmail.com",
                    DateOfBirth = new DateTime(1996,12,10),
                    Gender = Gender.Male,
                    DepartmentID = 2,
                    PhotoPath = "Images/Jubayed.png"
                }
                );
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    StudentID = 2,
                    FirstName = "SumsUddin",
                    LastName = "Shajib",
                    Email = "shumsu@gmail.com",
                    DateOfBirth = new DateTime(1986, 10, 12),
                    Gender = Gender.Male,
                    DepartmentID = 1,
                    PhotoPath = "Images/Shumsu.png"
                }
                );
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    StudentID = 3,
                    FirstName = "Shahad",
                    LastName = "Ahmed",
                    Email = "shahad@gmail.com",
                    DateOfBirth = new DateTime(1992, 6, 5),
                    Gender = Gender.Male,
                    DepartmentID = 4,
                    PhotoPath = "Images/Shahad.png"
                }
                );
        }
    }
}
