/*
 * Programmed by : Johnstanley Ajagu
 * Student Id: 8864315
 * Revision history:
 *      1-nov-2023: Project created
 *      1-nov-2023: Designed views
 *      4-nov-2023: updated student enrollment logic
 *      5-nov-2023: implemented cookies for the app's first visit date
 *      8-nov-2023: implemented push emails
 *      12-nov-2023: updated counts and status
 *      14-nov-2023: project completed
 */
using Microsoft.EntityFrameworkCore;

namespace JAjagu_Assignment2.Entities
{
    public class CourseRegistrationDbContext : DbContext
    {
        public CourseRegistrationDbContext(DbContextOptions<CourseRegistrationDbContext>options): base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().Property(u => u.Status).HasConversion<string>().HasMaxLength(255);

            modelBuilder.Entity<Student>().HasData(

               new Student()
               {
                   StudentId = 1, StudentName = "Bart Simpson", StudentEmail = "barts@gmail.com",
                   Status = Student.StudentStatus.ConfirmationMessageNotSent,
                   CourseId = 100
               },
               new Student()
               {
                   StudentId = 2, StudentName = "Lisa Bart", StudentEmail = "lbart@yahoo.com",
                   Status = Student.StudentStatus.ConfirmationMessageNotSent,
                   CourseId = 100
               },
               new Student()
               {
                   StudentId = 3, StudentName = "Ajagu", StudentEmail = "culjon@yahoo.com",
                   Status = Student.StudentStatus.ConfirmationMessageNotSent,
                   CourseId = 101
               },
               new Student()
               {
                   StudentId = 4, StudentName = "Amos Mars", StudentEmail = "culjon@yahoo.com",
                   Status = Student.StudentStatus.ConfirmationMessageNotSent,
                   CourseId = 101
               },
               new Student()
               {
                   StudentId = 5, StudentName = "James Cordon", StudentEmail = "culjon@yahoo.com",
                   Status = Student.StudentStatus.ConfirmationMessageNotSent,
                   CourseId = 102
               },
               new Student()
               {
                   StudentId = 6, StudentName = "Jordan Holmes", StudentEmail = "culjon@yahoo.com",
                   Status = Student.StudentStatus.ConfirmationMessageNotSent,
                   CourseId = 102
               });

            modelBuilder.Entity<Course>().HasData(
                new Course()
                {
                    CourseId = 100, CourseName = "Programing Concepts 1", CourseInstructor = "Jasveen Kaur",
                    StartDate = DateTime.Now.AddDays(-25),
                    RoomNumber = "1C09",
                    NumberOfStudent = 2,
                },
                new Course()
                {
					CourseId = 101, CourseName = "System Analysis", CourseInstructor = "Yash Shah",
					StartDate = DateTime.Now.AddDays(-10),
					RoomNumber = "4G25",
					NumberOfStudent = 2,
                },
				new Course()
				{
					CourseId = 102, CourseName = "UX/UI Experience", CourseInstructor = "Rick Guzik",
					StartDate = DateTime.Now.AddDays(-30),
					RoomNumber = "2B25",
					NumberOfStudent = 2,
                });

           
        }
    }
}
