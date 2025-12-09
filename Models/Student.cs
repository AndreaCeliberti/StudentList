using System;
using System.ComponentModel.DataAnnotations;

namespace Student.Models
{
    public class Student
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cognome { get; set; }

        public DateOnly DataDiNascita { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
