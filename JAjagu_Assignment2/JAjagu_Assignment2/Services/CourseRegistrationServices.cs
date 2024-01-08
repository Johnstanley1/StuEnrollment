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
using JAjagu_Assignment2.Entities;
using JAjagu_Assignment2.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;

namespace JAjagu_Assignment2.Services
{
	public class CourseRegistrationServices : ICourseRegistrationService
    {
        private CourseRegistrationDbContext _courseRegistrationDbContext;

        public CourseRegistrationServices(CourseRegistrationDbContext courseRegistrationDbContext)
        {
            _courseRegistrationDbContext = courseRegistrationDbContext;
        }

        // Retrieve all courses from db
        public List<Course> GetAllCourses()
        {
            List<Course> course = _courseRegistrationDbContext.Courses
                .Include(c => c.Students)
                .ToList();
            return course;
        }


        // Retrieve all students from db
        public List<Student> GetAllStudents()
        {
            var students = _courseRegistrationDbContext.Students
                .ToList();
			return students;    
        }


        // Retrieve all courses from db by CourseId
        public Course GetCourseById(int courseId)
        {
            Course? course = _courseRegistrationDbContext.Courses
                .Include(c => c.Students)
                .FirstOrDefault(c=> c.CourseId == courseId);
            return course;
        }


        // Retrieve all students from db by studentId
		public Student GetStudentById(int studentId)
		{
            Student? student = _courseRegistrationDbContext?.Students
                .FirstOrDefault(s => s.StudentId == studentId);
            return student;
		}

        // Retrieve all students in a course
		public List<Student> GetStudentsByCourse(int courseId)
        {
            var students = _courseRegistrationDbContext?.Students?
                .Where(s => s.CourseId == courseId)
                .ToList();
            return students;
        }


        // Add students to course
        public void AddStudentToCourse(int courseId, EnrollmentViewModel enrollmentViewModel)
        {
            var courses = _courseRegistrationDbContext.Courses
                .Include(c => c.Students)
                .FirstOrDefault(c => c.CourseId == courseId);

            if (courses != null && enrollmentViewModel.NewStudent != null)
            {
                courses?.Students?.Add(enrollmentViewModel.NewStudent);

                enrollmentViewModel.NewStudent.CourseId = courses.CourseId;

                _courseRegistrationDbContext.SaveChanges();
            }

        }
        

        // Add course to db
        public void AddCourse(EnrollmentViewModel enrollmentViewModel)
        {
            _courseRegistrationDbContext.Courses.Add(enrollmentViewModel.ActiveCourse);
            _courseRegistrationDbContext?.SaveChanges();
        }


        // Delete course from db
        public void DeleteCourse(int courseId)
        {
            var courseToDelete = _courseRegistrationDbContext.Courses
                .FirstOrDefault(s => s.CourseId == courseId);

            if (courseToDelete != null)
            {
                _courseRegistrationDbContext.Courses.Remove(courseToDelete);
                _courseRegistrationDbContext.SaveChanges();
            }
        }


        // Update existing course
        public void UpdateCourse(EnrollmentViewModel enrollmentViewModel)
        {
            _courseRegistrationDbContext.Courses.Update(enrollmentViewModel.ActiveCourse);
            _courseRegistrationDbContext.SaveChanges();
        }


        // Delete students from course
        public void DeleteStudentFromCourse(int courseId, int studentId)
        {
            var course = _courseRegistrationDbContext.Courses
                .Include(c => c.Students)
                .FirstOrDefault(c => c.CourseId == courseId);

            if (course != null)
            {
                var student = course.Students.FirstOrDefault(s => s.StudentId == studentId);

                if (student != null)
                {
                    course.Students.Remove(student);
                    _courseRegistrationDbContext.SaveChanges();
                }
            }
        }
        

        // Calculate total number of registered students in a course
        public List<Course> TotalRegisteredStudents()
        {
            var courses = _courseRegistrationDbContext.Courses.Include(c => c.Students).ToList();

            foreach (var course in courses)
            {
                course.NumberOfStudent = course.Students.Count();
            }
            _courseRegistrationDbContext.SaveChanges();

            return courses;
        }


        // Send confirmation email for course
        public void SendConfirmationEmailForCourse(int courseId, int studentId, string confirmationLink)
        {
            // Retrieve all students in the course with the given courseId
            var students = _courseRegistrationDbContext.Students.Where(c => c.CourseId == courseId);

            foreach (var student in students)
            {
				Debug.WriteLine($"Sending confirmation email for CourseId: {courseId}, StudentId: {student.StudentId}");

				// Send a confirmation email to each student
				SendConfirmationEmailToStudent(student.StudentId, courseId, confirmationLink);

				student.Status = Student.StudentStatus.ConfirmationMessageSent;
				_courseRegistrationDbContext.SaveChanges();

			}

		}


        // Send confirmation email to students in a course
		public void SendConfirmationEmailToStudent(int studentId, int courseId, string confirmationLink)
		{
            Course? course = _courseRegistrationDbContext.Courses
                .Include(s => s.Students)  
                .FirstOrDefault(s => s.CourseId == courseId);

            Student? student = _courseRegistrationDbContext.Students
                .Include(c => c.Course)
               .FirstOrDefault(c => c.StudentId == studentId && c.CourseId == courseId);

            if (student == null)
            {
                return;
            }

            string studentEmail = student.StudentEmail;
            string fromAddress = "memwatv@gmail.com";
            string password = "trka utlf wqcg sobu";

             var smtpClient = new SmtpClient("smtp.gmail.com")
             {
                 Port = 587,
                 Credentials = new NetworkCredential(fromAddress, password),
                 EnableSsl = true,
             };

             var mailMessage = new MailMessage()
             {
                 From = new MailAddress(fromAddress),
                 Subject = $"Enrollment confirmation for ''{course.CourseName}'' required ",
                 Body = $"<h2>Hello {student.StudentName}:</h2><p>You requested to enroll in the course ''{course.CourseName}'', holding in room ''{course.RoomNumber}'', and starting on ''{course.StartDate?.ToString("d")}'' with instructor ''{course.CourseInstructor}''." +
				 $"<br><br>We are pleased to have you in this course, so if you could <a href=\"{confirmationLink}?studentId={student.StudentId}\">Confirm your enrollment</a> as soon as possible that would be appreciated. " +
                 $"<br><br>Sincerely," +
                 $"<br><br>The Course Manager App<br><br>",
                 IsBodyHtml = true
             };
             mailMessage.To.Add(studentEmail);
             smtpClient.Send(mailMessage);
        }


        // Update enrolled student status and invitation counts
        public void UpdateStudent(int courseId, int studentId, EnrollmentViewModel enrollmentViewModel)
        {
			Course? course = _courseRegistrationDbContext.Courses
	                    .Include(s => s.Students)
	                    .FirstOrDefault(s => s.CourseId == courseId);
			if (course != null)
			{
				Debug.WriteLine($"StudentId is : {studentId}, CourseId is: {courseId}");
                var student = course.Students.FirstOrDefault(s => s.StudentId == studentId);
				if (student != null)
				{
					Debug.WriteLine($"StudentId is : {studentId}");

					if (enrollmentViewModel.ResponsesOption == EnrollmentViewModel.EnrollmentResponse.No)
					{
						student.Status = Student.StudentStatus.EnrollmentDeclined;
						enrollmentViewModel.DeclinedCount++;
					}
					else
					{
						student.Status = Student.StudentStatus.EnrollmentConfirmed;
						enrollmentViewModel.ConfirmedCount++;
					}

					_courseRegistrationDbContext.SaveChanges();
				}
				else
				{
					Debug.WriteLine($"No student found with ID {studentId} in the course.");
					Debug.WriteLine($"Students in the course: {string.Join(", ", course.Students.Select(s => s.StudentId))}");
				}
			}
			else
			{
				Debug.WriteLine($"Course with ID {courseId} not found.");
			}
		}

    }
}

