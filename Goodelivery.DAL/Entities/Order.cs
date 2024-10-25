using System.ComponentModel.DataAnnotations;

namespace Goodelivery.DAL.Entities;

public class Order
{
    public Guid Id { get; init; }
    public double Weight { get; set; }
    [StringLength(30)]
    public required string Region { get; set; }
    public DateTime Time { get; set; }
    public override string ToString() => $"{Id}\tВес: {Weight}кг\tРайон: {Region}\tДата и время: {Time}";
}