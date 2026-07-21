using System.ComponentModel.DataAnnotations;

namespace Utlanssystem.Models
{
    public class Device
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Navn må fylles ut")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enhetstype må fylles ut")]
        public string DeviceType { get; set; }
        [Required(ErrorMessage = "Modell må fylles ut")]
        public string ModelName { get; set; }
        public string Specifications { get; set; }
        public bool IsAvailable { get; set; }
    }
}