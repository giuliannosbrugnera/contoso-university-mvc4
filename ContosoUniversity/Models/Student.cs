using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; }

        [Display(Name = "First Mid Name")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Column("FirstName")]
        public string FirstMidName { get; set; }

        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        public string FullName => LastName + ", " + FirstMidName;

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}