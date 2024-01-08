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

namespace JAjagu_Assignment2.Services
{
    public interface ICourseRegistrationService
    {
		List<Course> GetAllCourses();
		Course GetCourseById(int courseId);
        void AddCourse(EnrollmentViewModel enrollmentViewModel);
        void UpdateCourse(EnrollmentViewModel enrollmentViewModel);
        void DeleteCourse(int courseId);


        List<Student> GetStudentsByCourse(int courseId);
		Student GetStudentById(int studentId);
		List<Student> GetAllStudents();
        void AddStudentToCourse(int courseId, EnrollmentViewModel enrollmentViewModel);
        void DeleteStudentFromCourse(int courseId, int studentId);


        List<Course> TotalRegisteredStudents();
        void SendConfirmationEmailForCourse(int courseId, int studentId, string confirmationLink);
        void UpdateStudent(int courseId, int studentId, EnrollmentViewModel enrollmentViewModel);
    }
}
