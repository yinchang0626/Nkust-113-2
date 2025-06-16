using System;
using System.Collections.Generic;

namespace CourseSelectionSystem.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Instructor { get; set; }
        public decimal? Price { get; set; }
        public DateTime? StartDate { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
