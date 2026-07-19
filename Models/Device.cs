using System.ComponentModel.DataAnnotations;

namespace Utlanssystem.Models
{
    public class Device
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string DeviceType { get; set; }
        public string ModelName { get; set; }
        public string Specifications { get; set; }
        public bool IsAvailable { get; set; }
    }
}