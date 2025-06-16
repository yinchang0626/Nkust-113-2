using System;

namespace CourseSelectionSystem.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        public Course? Course { get; set; }
        public User? User { get; set; }
    }
}
