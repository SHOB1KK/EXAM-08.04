using System.Net;
using Domain.DTOs;
using Domain.DTOs.Booking;
using Domain.Entities;
using Domain.Entities.DTOs;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserService(DataContext context) : IUserService
{

    public async Task<Response<List<User>>> GetAllUsersAsync()
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

    public async Task<Response<User>> GetUserByIdAsync(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            return new Response<User>(HttpStatusCode.BadRequest, $"User with id {id} not found");
        }

        var userDto = new User
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Phone = user.Phone,
        };

        return new Response<User>(userDto);
    }

    public async Task<Response<CreateUserDto>> CreateUserAsync(CreateUserDto createUserDto)
    {
        var user = new User()
        {
            Phone = createUserDto.Phone,
            Email = createUserDto.Email,
            UserName = createUserDto.UserName,
        };
        await context.Users.AddAsync(user);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<CreateUserDto>(HttpStatusCode.BadRequest, "User not created")
            : new Response<CreateUserDto>(createUserDto);
    }

    public async Task<Response<UpdateUserDto>> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
    {
        var existingUser = await context.Users.FindAsync(id);
        if (existingUser == null)
        {
            return new Response<UpdateUserDto>(HttpStatusCode.BadRequest, $"User with id {id} not found");
        }

        existingUser.Phone = updateUserDto.Phone;
        existingUser.Email = updateUserDto.Email;
        existingUser.UserName = updateUserDto.UserName;
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<UpdateUserDto>(HttpStatusCode.BadRequest, "User not updated")
            : new Response<UpdateUserDto>(updateUserDto);
    }

    public async Task<Response<string>> DeleteUserAsync(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            return new Response<string>("User does not exist");
        }

        context.Remove(user);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "User not deleted")
            : new Response<string>(HttpStatusCode.OK, "User deleted");
    }

    public async Task<Response<List<User>>> FrequentRenterDto()
    {
        var users = await context.Users
            .Where(u => u.Bookings.Count > 3)
            .ToListAsync();

        var data = users.Select(u => new FrequentRenterDto
        {
            UserName = u.UserName,
            BookingCount = u.Bookings.Count,
        }).ToList();

        return new Response<List<User>>(data);
    }
}