using Domain.Entities;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface ICarService
{
    Task<Response<List<Car>>> GetAllCarsAsync();
    Task<Response<Car>> GetCarByIdAsync(int id);
    Task<Response<Car>> CreateCarAsync(Car car);
    Task<Response<Car>> UpdateCarAsync(int id, Car car);
    Task<Response<string>> DeleteCarAsync(int id);
    Task<Response<List<AvailableCarDto>>> GetAvailableCarsAsync();
    Task<Response<List<PopularCarDto>>> GetPopularCarsAsync();
}
