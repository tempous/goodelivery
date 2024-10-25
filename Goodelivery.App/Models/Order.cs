namespace Goodelivery.App.Models;

public class Order
{
    public Guid Id { get; set; }
    public double Weight { get; set; }
    public required string Region { get; set; }
    public DateTime Time { get; set; }
    public override string ToString() => $"{Id}\tВес: {Weight}кг\tРайон: {Region}\tДата и время: {Time}";
}