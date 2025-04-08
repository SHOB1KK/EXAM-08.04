namespace Domain.DTOs;

public class GetBookingDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public int CarId { get; set; }
    public string CarModel { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
}
