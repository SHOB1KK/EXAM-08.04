using Domain.DTOs;
using Domain.DTOs.Booking;
using Domain.Entities;
using Domain.Entities.DTOs;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IBookingService
{
    Task<Response<List<Booking>>> GetAllAsync();
    Task<Response<Booking>> GetByIdAsync(int id);
    Task<Response<Booking>> CreateAsync(Booking booking);
    Task<Response<Booking>> UpdateAsync(int id, UpdateBookingDto updateBookingDto);
    Task<Response<string>> DeleteAsync(int id);
    Task<Response<List<ActiveBookingDto>>> GetActiveBookingsAsync();
}