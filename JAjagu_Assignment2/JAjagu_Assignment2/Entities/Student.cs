using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JAjagu_Assignment2.Entities
{
    public class Student
    {
        public enum StudentStatus
        {
            [Display(Name = "Invitation Not Sent")]
            ConfirmationMessageNotSent = 0,

            [Display(Name = "Invitation Sent")]
            ConfirmationMessageSent = 1,

            [Display(Name = "Enrollment Confirmed")]
            EnrollmentConfirmed = 2,

            [Display(Name = "Enrollment Declined")]
            EnrollmentDeclined = 3
        }

        // PK
        public int StudentId { get; set; }


        [Required(ErrorMessage = "Please enter student's name")]
        public string? StudentName { get; set; }


        [Required(ErrorMessage = "Please enter student's email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address in the format abc@domain.com")]
        public string? StudentEmail { get; set; }


        [Required(ErrorMessage = "Please enter student's status")]
        public StudentStatus Status { get; set; } = StudentStatus.ConfirmationMessageNotSent;

        public string StatusString
        {
            get
            {
                switch (Status)
                {
                    case StudentStatus.ConfirmationMessageNotSent:
                        return "Invitation Not Sent";
                    case StudentStatus.ConfirmationMessageSent:
                        return "Invitation Sent";
                    case StudentStatus.EnrollmentConfirmed:
                        return "Enrollment Confirmed";
                    case StudentStatus.EnrollmentDeclined:
                        return "Enrollment Declined";
                    default:
                        return Status.ToString();
                }
            }
        }

        public Course? Course { get; set; }

        public int CourseId { get; set; }
    }
}
