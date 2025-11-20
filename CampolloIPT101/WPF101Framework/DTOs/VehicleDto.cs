using System.ComponentModel.DataAnnotations;

namespace WPF101.WPF101Framework.DTOs
{
    public class VehicleDto
    {
        [Key]
        public int Id { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Price { get; set; }
    }
}
