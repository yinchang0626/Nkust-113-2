using System.Collections.Generic;

namespace CourseSelectionSystem.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
