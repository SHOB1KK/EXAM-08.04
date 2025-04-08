using System.Net;
using Domain.DTOs;
using Domain.DTOs.Booking;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class BookingService(DataContext context) : IBookingService
{

    public async Task<Response<List<GetBookingDto>>> GetAllAsync()
    {
        var bookings = await context.Bookings
            .Include(b => b.User)
            .Include(b => b.Car)
            .ToListAsync();

        var data = bookings.Select(b => new GetBookingDto
        {
            Id = b.Id,
            UserId = b.UserId,
            UserName = b.User.UserName,
            CarId = b.CarId,
            CarModel = b.Car.Model,
            StartDate = b.StartDate,
            EndDate = b.EndDate,
            TotalPrice = b.TotalPrice,
        }).ToList();

        return new Response<List<GetBookingDto>>(data);
    }

    public async Task<Response<GetBookingDto>> GetByIdAsync(int id)
    {
        var booking = await context.Bookings
            .Include(b => b.User)
            .Include(b => b.Car)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (booking == null)
        {
            return new Response<GetBookingDto>(HttpStatusCode.BadRequest, $"Booking with id {id} not found");
        }

        var bookingDto = new GetBookingDto
        {
            Id = booking.Id,
            UserId = booking.UserId,
            UserName = booking.User.UserName,
            CarId = booking.CarId,
            CarModel = booking.Car.Model,
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            TotalPrice = booking.TotalPrice,
        };

        return new Response<GetBookingDto>(bookingDto);
    }

    public async Task<Response<CreateBookingDto>> CreateAsync(CreateBookingDto createBookingDto)
    {
        var booking = new Booking
        {
            UserId = createBookingDto.UserId,
            CarId = createBookingDto.CarId,
            StartDate = createBookingDto.StartDate,
            EndDate = createBookingDto.EndDate,
        };

        await context.Bookings.AddAsync(booking);
        var result = await context.SaveChangesAsync();

        if (result == 0)
        {
            return new Response<CreateBookingDto>(HttpStatusCode.BadRequest, "Booking not created");
        }

        return new Response<CreateBookingDto>(createBookingDto);
    }

    public async Task<Response<UpdateBookingDto>> UpdateAsync(int id, UpdateBookingDto updateBookingDto)
    {
        var existingBooking = await context.Bookings.FindAsync(id);
        if (existingBooking == null)
        {
            return new Response<UpdateBookingDto>(HttpStatusCode.BadRequest, $"Booking with id {id} not found");
        }

        existingBooking.UserId = updateBookingDto.UserId;
        existingBooking.CarId = updateBookingDto.CarId;
        existingBooking.StartDate = updateBookingDto.StartDate;
        existingBooking.EndDate = updateBookingDto.EndDate;
        existingBooking.TotalPrice = updateBookingDto.TotalPrice;

        var result = await context.SaveChangesAsync();

        if (result == 0)
        {
            return new Response<UpdateBookingDto>(HttpStatusCode.BadRequest, "Booking not updated");
        }

        var updatedBookingDto = new UpdateBookingDto
        {
            Id = existingBooking.Id,
            UserId = existingBooking.UserId,
            CarId = existingBooking.CarId,
            StartDate = existingBooking.StartDate,
            EndDate = existingBooking.EndDate,
            TotalPrice = existingBooking.TotalPrice,
        };

        return new Response<UpdateBookingDto>(updatedBookingDto);
    }

    public async Task<Response<string>> DeleteAsync(int id)
    {
        var booking = await context.Bookings.FindAsync(id);
        if (booking == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Booking does not exist");
        }

        context.Remove(booking);
        var result = await context.SaveChangesAsync();

        if (result == 0)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "Booking not deleted");
        }

        return new Response<string>(HttpStatusCode.OK, "Booking deleted");
    }

    public async Task<Response<List<User>>> FrequentRenterDto()
    {
        var users = await context.Users.ToListAsync();

        var data = users.Select(u => new User
        {
            Id = u.Id,
            UserName = u.UserName,
            Email = u.Email,
            Phone = u.Phone,
        }).ToList();

        return new Response<List<User>>(data);
    }
    public async Task<Response<List<ActiveBookingDto>>> GetActiveBookingsAsync()
    {
        var activeBookings = await context.Bookings
            .Include(b => b.User)
            .Include(b => b.Car)
            .Where(b => b.StartDate <= DateTime.Now && b.EndDate >= DateTime.Now)
            .ToListAsync();

        var data = activeBookings.Select(b => new ActiveBookingDto
        {
            CarModel = b.Car.Model, 
            UserName = b.User.UserName,
            EndDate = b.EndDate
        }).ToList();

        return new Response<List<ActiveBookingDto>>(data);
    }
}