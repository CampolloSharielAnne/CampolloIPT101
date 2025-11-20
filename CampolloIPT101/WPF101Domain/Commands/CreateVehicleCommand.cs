namespace WPF101.WPF101Domain.Commands
{
    public class CreateVehicleCommand
    {
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Price { get; set; }
    }
}
