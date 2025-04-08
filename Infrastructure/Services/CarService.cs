using System.Net;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Services;

public class CarService(DataContext context) : ICarService
{

    public async Task<Response<List<Car>>> GetAllCarsAsync()
    {
        var cars = await context.Cars.ToListAsync();

        var data = cars.Select(c => new Car
        {
            Id = c.Id,
            Model = c.Model,
            PricePerDay = c.PricePerDay,
            IsAvailable = c.IsAvailable,
        }).ToList();

        return new Response<List<Car>>(data);
    }

    public async Task<Response<Car>> GetCarByIdAsync(int id)
    {
        var car = await context.Cars.FindAsync(id);
        if (car == null)
        {
            return new Response<Car>(HttpStatusCode.BadRequest, $"Car with id {id} not found");
        }

        return new Response<Car>(car);
    }

    public async Task<Response<Car>> CreateCarAsync(Car car)
    {
        await context.Cars.AddAsync(car);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<Car>(HttpStatusCode.BadRequest, "Car not created")
            : new Response<Car>(car);
    }

    public async Task<Response<Car>> UpdateCarAsync(int id, Car car)
    {
        var existingCar = await context.Cars.FindAsync(id);
        if (existingCar == null)
        {
            return new Response<Car>(HttpStatusCode.BadRequest, $"Car with id {id} not found");
        }

        existingCar.Model = car.Model;
        existingCar.PricePerDay = car.PricePerDay;
        existingCar.IsAvailable = car.IsAvailable;

        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<Car>(HttpStatusCode.BadRequest, "Car not updated")
            : new Response<Car>(existingCar);
    }

    public async Task<Response<string>> DeleteCarAsync(int id)
    {
        var car = await context.Cars.FindAsync(id);
        if (car == null)
        {
            return new Response<string>("Car does not exist");
        }

        context.Cars.Remove(car);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Car not deleted")
            : new Response<string>(HttpStatusCode.OK, "Car deleted");
    }

    public async Task<Response<List<AvailableCarDto>>> GetAvailableCarsAsync()
    {
        var cars = await context.Cars
            .Where(c => c.IsAvailable)
            .ToListAsync();

        var data = cars.Select(c => new AvailableCarDto
        {
            Model = c.Model,
            PricePerDay = c.PricePerDay,
        }).ToList();

        return new Response<List<AvailableCarDto>>(data);
    }

    public async Task<Response<List<PopularCarDto>>> GetPopularCarsAsync()
    {
        var cars = await context.Cars
            .OrderByDescending(c => c.BookingCount)
            .Take(3)
            .ToListAsync();

        var data = cars.Select(c => new PopularCarDto
        {
            Model = c.Model,
            BookingCount = c.BookingCount
        }).ToList();

        return new Response<List<PopularCarDto>>(data);
    }
}