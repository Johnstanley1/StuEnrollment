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
using JAjagu_Assignment2.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JAjagu_Assignment2.Controllers
{
    public class CourseRegistrationController : BaseController
    {
        private ICourseRegistrationService _courseRegistrationService;

        public CourseRegistrationController(ICourseRegistrationService courseRegistrationService)
        {
            _courseRegistrationService = courseRegistrationService;
        }

        // Retrieve and render all courses information to the view
        [HttpGet("/courses/all-courses")]
		public IActionResult AllCourses()
        {
            var allCoursesWithRegisteredStudents = _courseRegistrationService.TotalRegisteredStudents();

            string cookiesValue = GetPageVisitDate("Home_Cookies_Date");
            if (string.IsNullOrEmpty(cookiesValue))
            {
                SetPageVisitDate("CourseRegistration_Cookies_Date", DateTime.Now.ToString());
            }
            ViewBag.CookiesPageVisitDate = cookiesValue;

            return View("AllCourses", allCoursesWithRegisteredStudents);
        }

        // Get method to render the add courses view for the web app
        [HttpGet()]
        public IActionResult AddCourse()
        {
            var students = _courseRegistrationService.GetAllStudents();

            EnrollmentViewModel enrollmentViewModel = new EnrollmentViewModel
            {
                ActiveCourse = new Course(),
                NewStudent = new Student(),
                AllStudents = students
            };

            string cookiesValue = GetPageVisitDate("CourseRegistration_Cookies_Date");

            if (string.IsNullOrEmpty(cookiesValue))
            {
                SetPageVisitDate("CourseRegistration_Cookies_Date", DateTime.Now.ToString());
            }
            ViewBag.CookiesPageVisitDate = cookiesValue;

            return View(enrollmentViewModel);
        }


        // post method to add courses to the db 
        [HttpPost()]
         public IActionResult AddCourse(EnrollmentViewModel enrollmentViewModel)
         {

            if (ModelState.IsValid)
            {
                _courseRegistrationService?.AddCourse(enrollmentViewModel);

                TempData["LastActionMessage"] = $"The course '{enrollmentViewModel.ActiveCourse.CourseName}' was added successfully";

                return RedirectToAction("ManageCourses", "CourseRegistration", new {id = enrollmentViewModel.ActiveCourse.CourseId});
            }
            else
            {
                var student = _courseRegistrationService?.GetStudentsByCourse(enrollmentViewModel.ActiveCourse.CourseId)?.OrderBy(g => g.StudentName).ToList();
                enrollmentViewModel.AllStudents = student;

                return View(enrollmentViewModel);
            }

         }


         // Get method to render the edit course view 
         [HttpGet()]
         public IActionResult EditCourse(int id)
         {
            var course = _courseRegistrationService.GetCourseById(id);
            var student = _courseRegistrationService.GetStudentsByCourse(id);

            string cookiesValue = GetPageVisitDate("CourseRegistration_Cookies_Date");

            if (string.IsNullOrEmpty(cookiesValue))
            {
                SetPageVisitDate("CourseRegistration_Cookies_Date", DateTime.Now.ToString());
            }
            ViewBag.CookiesPageVisitDate = cookiesValue;

            EnrollmentViewModel enrollmentViewModel = new EnrollmentViewModel()
            {
				ActiveCourse = course,
                AllStudents = student,
            };
            return View(enrollmentViewModel);
         }
         

         // Post method to edit course information 
         [HttpPost()]
         public IActionResult EditCourse(EnrollmentViewModel enrollmentViewModel)
         {
             if (ModelState.IsValid)
             {
                 _courseRegistrationService.UpdateCourse(enrollmentViewModel);

                TempData["LastActionMessage"] = $"The course '{enrollmentViewModel.ActiveCourse.CourseName}' was updated successfully";

                return RedirectToAction("ManageCourses", "CourseRegistration", new {id = enrollmentViewModel?.ActiveCourse?.CourseId});

             }
             else
             {
                 var students = _courseRegistrationService?.GetStudentsByCourse(enrollmentViewModel.ActiveCourse.CourseId)?.OrderBy(s => s.StudentName).ToList();
                 enrollmentViewModel.AllStudents = students;
             }
             return View(enrollmentViewModel);
         }


        // Get method to render the manage course view with courses and associated student in the course
        [HttpGet()]
        public IActionResult ManageCourses(int id)
        {
            Course courses = _courseRegistrationService.GetCourseById(id);
            List<Student> students = _courseRegistrationService?.GetStudentsByCourse(id);

            string cookiesValue = GetPageVisitDate("CourseRegistration_Cookies_Date");

			if (string.IsNullOrEmpty(cookiesValue))
            {
                SetPageVisitDate("CourseRegistration_Cookies_Date", DateTime.Now.ToString());
            }
            ViewBag.CookiesPageVisitDate = cookiesValue;

            EnrollmentViewModel enrollmentViewModel = new EnrollmentViewModel
            {
                ActiveCourse = courses,
                AllStudents = students,
                NewStudent = new Student(),
                InvitationNotSentCount = courses?.Students?.Where(s => s.Status == Student.StudentStatus.ConfirmationMessageNotSent).Count() ?? 0,
                InvitationSentCount = courses?.Students?.Where(s => s.Status == Student.StudentStatus.ConfirmationMessageSent).Count() ?? 0,
                ConfirmedCount = courses?.Students?.Where(s => s.Status == Student.StudentStatus.EnrollmentConfirmed).Count() ?? 0,
                DeclinedCount = courses?.Students?.Where(s => s.Status == Student.StudentStatus.EnrollmentDeclined).Count() ?? 0,
			};
            return View(enrollmentViewModel);
        }


        // Post method to add edit/add and delete courses, add students to course, and send enrollment emails to students
        [HttpPost()]
        public IActionResult ManageCourses(int id, EnrollmentViewModel enrollmentViewModel)
        {
			if (ModelState.IsValid)
            {
                _courseRegistrationService?.AddStudentToCourse(id, enrollmentViewModel);
                var student = _courseRegistrationService.GetStudentById(id);
                if (student != null)
                {
					int newlyCreatedStudentId = student.StudentId;
					TempData["NewlyCreatedStudentId"] = newlyCreatedStudentId;
					TempData["LastActionMessage"] = $"The student '{enrollmentViewModel?.NewStudent?.StudentName}' was added successfully";
					enrollmentViewModel.InvitationNotSentCount++;
				}
				
				return RedirectToAction("ManageCourses", new { id });
            }
            else
            {
                var courses = _courseRegistrationService.GetCourseById(id);
                var students = _courseRegistrationService?.GetStudentsByCourse(id)?.Where(s => s.CourseId == id).OrderBy(p => p.StudentName).ToList();

                enrollmentViewModel.ActiveCourse = courses;
                enrollmentViewModel.AllStudents = students;

            }
			return View(enrollmentViewModel);

        }

        // Get method to render the delete course view
		[HttpGet()]
        public IActionResult DeleteCourse(int id)
        {
            var course = _courseRegistrationService.GetCourseById(id);
            var student = _courseRegistrationService.GetStudentsByCourse(id);

            string cookiesValue = GetPageVisitDate("CourseRegistration_Cookies_Date");

            if (string.IsNullOrEmpty(cookiesValue))
            {
                SetPageVisitDate("CourseRegistration_Cookies_Date", DateTime.Now.ToString());
            }
            ViewBag.CookiesPageVisitDate = cookiesValue;

            EnrollmentViewModel enrollmentViewModel = new EnrollmentViewModel()
            {
				ActiveCourse = course,
                AllStudents = student,
            };
            return View(enrollmentViewModel);
        }


        // Post method to delete course along with its assiociated students
        [HttpPost()]
        public IActionResult Deletion(EnrollmentViewModel enrollmentViewModel)
        {
            _courseRegistrationService.DeleteCourse(enrollmentViewModel.ActiveCourse.CourseId);

            TempData["LastActionMessage"] = $"The course {enrollmentViewModel.ActiveCourse.CourseName} and its associated students were deleted successfully";

            return RedirectToAction("AllCourses");
        }


        // Post method to delete students from the course
        [HttpPost()]
        public IActionResult DeleteStudent(int courseId, int studentId, EnrollmentViewModel enrollmentViewModel)
        {
            _courseRegistrationService.DeleteStudentFromCourse(courseId, studentId);

            TempData["LastActionMessage"] = $"The student {enrollmentViewModel?.NewStudent?.StudentName} was deleted successfully";

            return RedirectToAction("ManageCourses", new {id = courseId});

        }


        // Post method to send emails to students
		[HttpPost()]
		public IActionResult SendEmails(int id, int studentId, EnrollmentViewModel enrollmentViewModel)
		{
            Debug.WriteLine($"Received values - CourseId: {id}, StudentId: {studentId}");

            if (enrollmentViewModel.SendEmail = true)
            {
                string confirmationLink = Url.Action("EnrollmentResponse", "CourseRegistration", new { id }, Request.Scheme);

                _courseRegistrationService?.SendConfirmationEmailForCourse(id, studentId, confirmationLink);

            }
            else
            {
                TempData["LastActionMessage"] = "Sending email is not enabled.";
            }
            TempData["LastActionMessage"] = "Confirmation email sent successfully";

            enrollmentViewModel.InvitationSentCount++;

            return RedirectToAction("ManageCourses", new { id });
        }

        
        // Get method to render the enrollment confirmation view
        [HttpGet()]
        public IActionResult EnrollmentResponse(int id, int studentId)
        {
            var courses = _courseRegistrationService.GetCourseById(id);

            string cookiesValue = GetPageVisitDate("CourseRegistration_Cookies_Date");

            if (string.IsNullOrEmpty(cookiesValue))
            {
                SetPageVisitDate("CourseRegistration_Cookies_Date", DateTime.Now.ToString());
            }
            ViewBag.CookiesPageVisitDate = cookiesValue;

            if (courses != null)
            {
                var targetStudent = _courseRegistrationService.GetStudentById(studentId);
				EnrollmentViewModel enrollmentViewModel = new EnrollmentViewModel()
                {
                    
                    ActiveCourse = courses,
                    AllStudents = new List<Student> { targetStudent }
                };
                return View("EnrollmentResponse", enrollmentViewModel);

            }
            return View("AllCourses");

        }


        // Post method to confirm/decline enrollment
        [HttpPost()]
        public IActionResult EnrollmentResponse(int id, int studentId, EnrollmentViewModel enrollmentViewModel)
        {
			_courseRegistrationService.UpdateStudent(id, studentId, enrollmentViewModel);

			if (enrollmentViewModel.ResponsesOption == EnrollmentViewModel.EnrollmentResponse.No)
			{
				return RedirectToAction("ManageCourses", new { id });
			}
			else
			{
				return RedirectToAction("ThankYou", new { id });
			}			
        }


        // Get method to render the thank you view
        [HttpGet("/courses/{id}/thank-you")]
		public IActionResult ThankYou(int id)
		{
			var courses = _courseRegistrationService.GetCourseById(id);
			var students = _courseRegistrationService.GetStudentsByCourse(id)?.Where(s => s.CourseId == id).OrderBy(p => p.StudentName).ToList();
			EnrollmentViewModel enrollmentViewModel = new EnrollmentViewModel()
			{
				ActiveCourse = courses,
				AllStudents = students,
			};

            string cookiesValue = GetPageVisitDate("CourseRegistration_Cookies_Date");

            if (string.IsNullOrEmpty(cookiesValue))
            {
                SetPageVisitDate("CourseRegistration_Cookies_Date", DateTime.Now.ToString());
            }
            ViewBag.CookiesPageVisitDate = cookiesValue;

            return View("ThankYou", enrollmentViewModel);
		}
	}
}
