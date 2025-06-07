using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }

        [Required]
        public required string CourseName { get; set; }

        public required string Description { get; set; }

        public required string Instructor { get; set; }

        public string Classroom { get; set; } = string.Empty;
        public string Schedule { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        public DateTime? StartDate { get; set; }
    }
}