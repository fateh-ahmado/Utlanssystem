using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Utlanssystem.Models
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }

        public DateTime LoanDate { get; set; } // Datoen lånet ble opprettet

        [ForeignKey("Device")]
        public int DeviceId { get; set; }
        public Device? Device { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student? Student { get; set; }
    }
}