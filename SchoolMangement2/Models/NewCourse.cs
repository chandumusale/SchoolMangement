namespace SchoolMangement2.Models
{
    public class NewCourse
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public Course? Course { get; set; }



        public int StudentId { get; set; }
        public Student? Student { get; set; }

    }
}
