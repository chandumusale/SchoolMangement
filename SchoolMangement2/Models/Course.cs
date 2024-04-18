using System.ComponentModel.DataAnnotations;

namespace SchoolMangement2.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Course Name is required")]

        [RegularExpression(@"^[A-Za-z\s.#\++#]+$", ErrorMessage = "CourseName should only contain letters, dots, plus signs, hashtags, and white spaces")]
        public string? CourseName { get; set; }

        [Required(ErrorMessage = "Course Duration is required")]

        public string? CourseDuration { get; set; }

        public List<NewCourse>? NewCourses { get; set; }

    }
}
/*   public class Course
    {
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Course Name is required")]

        [RegularExpression(@"^[A-Za-z\s.#\++#]+$", ErrorMessage = "CourseName should only contain letters, dots, plus signs, hashtags, and white spaces")]
        public string ?CourseName { get; set; }

        [Required(ErrorMessage = "Course Duration is required")]
        
        public string? CourseDuration { get; set; }

        public List<CourseStudent>? CourseStudents { get; set; }
    }
    public class Student
    {

        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? CourseName { get; set; }
        public int SelectedCourseId { get; set; }  // To store the selected course ID

        public List<SelectListItem>? CourseOptions { get; set; }  // List to hold course options


        public List<CourseStudent>? CourseStudents { get; set; }
    }
    public class CourseStudent
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public Course? Course { get; set; }



        public int StudentId { get; set; }
        public Student? Student { get; set; }
    }*/