using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolMangement2.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? CourseName { get; set; }
       // public int SelectedCourseId { get; set; }  // To store the selected course ID

      //  public List<SelectListItem>? CourseOptions { get; set; }  // List to hold course options


       public List<NewCourse>? NewCourses { get; set; }


    }
}
