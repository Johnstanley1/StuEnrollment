using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JAjagu_Assignment2.Entities
{
    public class Course
    {
        // PK
        public int CourseId { get; set; }


        [Required(ErrorMessage = "Please enter course name")]
        public string? CourseName { get; set;}


        [Required(ErrorMessage = "Please enter course instructor")]
        public string? CourseInstructor { get; set; }


        [Required(ErrorMessage = "Please enter course start date")]
        public DateTime? StartDate { get; set; }


        [Required(ErrorMessage = "Please enter course room number")]
        [RegularExpression("^[0-9][A-Z]\\d{2}$", ErrorMessage = "Room number must be in the format: 4G15")]
        public string? RoomNumber { get; set; }

        public int NumberOfStudent { get; set; }

        public ICollection<Student>? Students { get; set; }
    }
}
