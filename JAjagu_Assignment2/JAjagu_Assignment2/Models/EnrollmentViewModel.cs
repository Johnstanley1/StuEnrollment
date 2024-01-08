using JAjagu_Assignment2.Entities;
using System.ComponentModel.DataAnnotations;

namespace JAjagu_Assignment2.Models
{
	public class EnrollmentViewModel
    {
        public enum EnrollmentResponse
		{
            Yes,
            No
        }

        public Course? ActiveCourse { get; set; }
        public Student? NewStudent { get; set; } // New student data
        public List<Student>? AllStudents { get; set; }
        public int InvitationSentCount { get; set; }
        public int InvitationNotSentCount { get; set; }
        public int ConfirmedCount { get; set; }
        public int DeclinedCount { get; set; }
        public bool SendEmail { get; set; } = false;
		public EnrollmentResponse ResponsesOption { get; set; }

    }
}
