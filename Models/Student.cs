using System.ComponentModel.DataAnnotations;

namespace Utlanssystem.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string StudentNumber { get; set; }
        public bool HasActiveLoan { get; set; }
    }
}