using System.ComponentModel.DataAnnotations;

namespace Goodelivery.API.Requests;

public class OrderRequest
{
    [Range(0.01, double.MaxValue, ErrorMessage = "Вес должен быть больше 0")]
    public double Weight { get; set; }
    [StringLength(30)]
    public required string Region { get; set; }
    public DateTime Time { get; set; }
}