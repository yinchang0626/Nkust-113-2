using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace C110152310.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<CourseSelection> Selections { get; set; } = new List<CourseSelection>();
    }

    public class CourseSelection
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string? UserId { get; set; }
        public Course? Course { get; set; }
    }
}
